using Hook;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Core.Globals;

using Log = Core.Utils.Logger;

namespace Core;
public unsafe class EntryPoint
{
    static Targets settings = new();    

    [UnmanagedCallersOnly(EntryPoint = "Load", CallConvs = new[] { typeof(CallConvCdecl) })]
    public static void Load()
    {
        Log.SetFile(LogPath);
        Log.Clear();
        Log.WriteLine($"Injected at {DateTime.Now}");
        Config = ConfigFile.GetConfig();

        HookApi.AltInit();
        switch (Config->TargetVersion)
        {
            case MinecraftVersion.v112:
                new v112.RenderHook(new(settings)).Attach();
                break;
            case MinecraftVersion.v119:
                break;
        }
        HookApi.Commit();

        InitBinds();

        if (ConsoleApp.IsOpen())
        {
            var consoleApp = new ConsoleApp();
            consoleApp.Beep();
        }
    }

    static void InitBinds()
    {
        var binds = new List<Bind>();
        for (int i = 0; i < ConfigFile.Config.STATES; i++)
        {
            int index = i;
            binds.Add(new(Config->Binds[i], () => Config->EnableState[index] = !Config->EnableState[index]));
        }
        BindManager.Add(binds);
    }

}