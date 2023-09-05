global using RU = ESP.Utils.RenderUtils;
global using Log = ESP.Utils.Logger;
global using Keys = ESP.Utils.Keys;
global using RH = ESP.RenderHook;

global using static ESP.Utils.Interop;
global using static OpenGL.Enums;
global using ESP.Utils;
global using OpenGL;
global using Memory;
global using Hook;
global using ESP;

global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Diagnostics;
global using System.Drawing;
global using System.Text;

namespace ESP;
public unsafe class EntryPoint
{
    static Render render;

    [UnmanagedCallersOnly(EntryPoint = "Load", CallConvs = new System.Type[] { typeof(CallConvCdecl) })]
    public static void Load()
    {
        Log.SetFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "cs-mc-esp-log.txt"));
        Log.Clear();
        Log.WriteLine($"Injected at {DateTime.Now}");

        HookApi.AltInit();
        RH.Attach(render = new());
        HookApi.Commit();
        InitBinds();

        SetupConsole();
    }

    [UnmanagedCallersOnly(EntryPoint = "Unload", CallConvs = new System.Type[] { typeof(CallConvCdecl) })]
    public static void Unload() { /* Not possible in NAOT */ }

    static void SetupConsole()
    {
        try
        {
            Console.Clear();

            StartThread(() =>
            {
                while (true)
                {
                    //var str = Console.ReadLine();
                }
            });

            Console.Beep(500, 500);
        }
        catch { }
    }

    static void InitBinds()
    {
        BindManager.Add(new List<Bind> {
            new(Keys.NumPad0, () => render.Settings.NoLight = !render.Settings.NoLight ),
            new(Keys.NumPad1, () => render.Settings.NoBackground = !render.Settings.NoBackground ),
            new(Keys.NumPad2, () => render.Settings.NoFog = !render.Settings.NoFog ),
            new(Keys.NumPad3, () => render.Settings.AntiCullFace = !render.Settings.AntiCullFace ),
            new(Keys.NumPad4, () => render.Settings.WorldChams = !render.Settings.WorldChams ),
            new(Keys.NumPad5, () => render.Settings.CaveViewer = !render.Settings.CaveViewer ),
            new(Keys.NumPad6, () => render.Settings.RainbowText = !render.Settings.RainbowText ),
            new(Keys.NumPad7, () => render.Settings.ESP = !render.Settings.ESP ),
        });
    }

}