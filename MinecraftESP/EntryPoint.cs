#define CS

using Cetours;
using Core.Abstracts;
using Core.Utils;
using Hook;
using Microsoft.Win32.SafeHandles;
using OpenGL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using static Core.Globals;

using Log = Core.Utils.Logger;
using Native = System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;

namespace Core;
public unsafe class EntryPoint
{
    [Native(EntryPoint = "Load", CallConvs = new[] { typeof(CallConvCdecl) })]
    public static void Load()
    {
        Log.SetFile(LogPath);
        Log.Clear();
        Log.WriteLine($"Injected at {DateTime.Now}");
        Config = ConfigFile.GetConfig();

        //HookApi.AltInit();
        //Modules.DetourRestoreAfterWith();
        //HookApi.Restore();
        //HookApi.UpdateCurrentThread();

#if CS
        //Detours.DetourTransactionBegin();
#else
        HookApi.Load();
        HookApi.Begin();
#endif

        var render = new Dictionary<MinecraftVersion, Func<AbstractRenderHook>>() {
            { MinecraftVersion.v1, () => new v1.RenderHook(new()) },
            { MinecraftVersion.v19, () => new v19.RenderHook(new()) },
            { MinecraftVersion.v115, () => new v115.RenderHook(new()) },
            { MinecraftVersion.Cristalix, () => new vCristalix.RenderHook(new()) }
        }[Config->TargetVersion]();
        render.Attach();

#if CS
        //Detours.DetourTransactionCommit();
#else
        HookApi.Commit();
#endif
        
        var oSB = (byte*)Utils.Interop.GetProcAddress(GL.Interface->Module, "wglSwapBuffers");
        var mSB = (byte*)(delegate* unmanaged<nint, void>)&v19.RenderHook.wglSwapBuffers;

        _MEMORY_BASIC_INFORMATION ombi;
        int ooldProtection;

        {
            Interop.VirtualQuery(oSB, &ombi, sizeof(_MEMORY_BASIC_INFORMATION));
            //ooldProtection = (int)ombi.AllocationProtect;
            Interop.VirtualProtect(ombi.BaseAddress, 0xc1000, 0x40, &ooldProtection);
        }

        {
            _MEMORY_BASIC_INFORMATION _mbi;
            Interop.VirtualQuery(mSB, &_mbi, sizeof(_MEMORY_BASIC_INFORMATION));
            int oldProtect;// = (int)_mbi.AllocationProtect;
            Interop.VirtualProtect(_mbi.BaseAddress, 0x308000, 0x40, &oldProtect);
        }

        var newPtr = (byte*)Interop.VirtualAlloc(null, 0x1000, 0x1000, 0x40); //0x2000 // oSB

        {   
            var ptr = oSB;

            byte* jumpSrc = ptr + 5;
            *ptr++ = 0xe9;
            *((int*)ptr) = (int)(mSB - jumpSrc);
            ptr += sizeof(int);
        }

        {
            var ptr = mSB;

            for (int i = 0; i < 50; i++)
                *ptr++ = 0x90;
        }

        {
            //int newProtection = 0x40;
            //Interop.VirtualProtect(ombi.BaseAddress, 0xc1000, ooldProtection, &newProtection);
        }

        ((delegate* unmanaged<nint, void>)oSB)(0x123);

        //render.PostAttach();

        InitBinds();
    }

    static void* AllocRegion(void* addr, int size)
    {
        byte* ptr = (byte*)addr;
        ptr -= (nint)ptr % 0x1000;

        _MEMORY_BASIC_INFORMATION mbi;
        while (true)
        {
            if (Interop.VirtualQuery(ptr, &mbi, sizeof(_MEMORY_BASIC_INFORMATION)) == 0)
                break;

            if (mbi.State == 0x10000 && mbi.RegionSize >= size)
                return mbi.AllocationBase;

            ptr += mbi.RegionSize;
        }

        return null;
    }

    static void InitBinds()
    {
        for (int i = 0; i < ConfigFile.Config.STATES; i++)
        {
            int index = i;
            BindManager.Add(new Bind(Config->Binds[i], () => Config->EnableState[index] = !Config->EnableState[index]));
        }
    }
}