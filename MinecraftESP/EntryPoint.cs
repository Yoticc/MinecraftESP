using Hook;
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
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

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

        var render = new Dictionary<MinecraftVersion, Func<AbstractRenderHook>>() {
            { MinecraftVersion.v1, () => new v1.RenderHook(new()) },
            { MinecraftVersion.v19, () => new v19.RenderHook(new()) },
            { MinecraftVersion.v115, () => new v115.RenderHook(new()) },
            { MinecraftVersion.Cristalix, () => new vCristalix.RenderHook(new()) }
        }[Config->TargetVersion]();

        render.Attach();

        InitBinds();
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