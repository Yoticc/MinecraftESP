using MinecraftESP;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sample;

public class Program
{
    public static void Main() { }

    private static EntryPoint entryPoint = new EntryPoint();

    private const uint DLL_PROCESS_DETACH = 0;
    private const uint DLL_PROCESS_ATTACH = 1;
    private const uint DLL_THREAD_ATTACH = 2;
    private const uint DLL_THREAD_DETACH = 3;

    private static bool loaded;
    private static bool unloaded;

    [UnmanagedCallersOnly(EntryPoint = "DllMain", CallConvs = new Type[] { typeof(CallConvStdcall) })]
    public static bool DllMain(IntPtr module, uint reason, IntPtr reserved)
    {
        try
        {
            switch (reason)
            {
                case DLL_PROCESS_DETACH:
                    if (!loaded || unloaded)
                        return false;

                    unloaded = true;
                    entryPoint.Unload();
                    break;
                case DLL_PROCESS_ATTACH:
                    if (loaded)
                        return false;

                    loaded = true;
                    entryPoint.Load();
                    break;
                case DLL_THREAD_ATTACH: break;
                case DLL_THREAD_DETACH: break;
            }
        } catch (Exception ex) { Interop.MessageBox(0, ex.ToString(), "C# Exception", 0); }

        return true;
    }
}
