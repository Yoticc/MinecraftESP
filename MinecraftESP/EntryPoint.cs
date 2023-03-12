using Hook;
using MinecraftESP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sample;
public unsafe class EntryPoint
{
    private static string logPath = @"C:\log.txt";
    public static void Log(object obj)
    {
        File.AppendAllText(logPath, $"{obj}\n");
    }

    private static int counter = 0;
    private static void Show(object message)
    {
        Interop.MessageBox(0, message.ToString(), counter.ToString(),0);
        counter++;
    }

    [UnmanagedCallersOnly(EntryPoint = "Sleep2")]
    public static void Sleep2(uint ms)
    {
        Show("sleep time: " + ms);
    }

    public void Load()
    {
        File.WriteAllText(logPath, "");
        Log($"Injected at {DateTime.Now}");

        HookApi.AltInit();

        Function origin = new Function("kernel32.Sleep");
        Function ripped = new Function((delegate* unmanaged<uint, void>)&Sleep2);
        HookFunction hook = new HookFunction(origin, ripped);

        hook.Attach();
        
        HookApi.Commit();

        Show("First");
        // Will be hooked
        Interop.Sleep(10000);

        Show("Second");
        // Will not be hooked
        ((delegate* unmanaged<uint, void>)hook)(10000); //can be used 'hook', 'origin', or 'origin.Ptr'
        Show("End");
    }

    public void Unload()
    {
        Log($"Uninjected at {DateTime.Now}");
    }
}