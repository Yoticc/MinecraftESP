global using Log = ESP.Utils.LogManger;
using ESP.Utils;
using Hook;
using OpenGL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Keys = ESP.Utils.Interop.Keys;
using RH = ESP.RenderHook;
namespace ESP;
public unsafe class EntryPoint
{
    private static Render render;

    [UnmanagedCallersOnly(EntryPoint = "Load", CallConvs = new System.Type[] { typeof(CallConvCdecl) })]
    public static void Load()
    {
        Log.SetFile(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "cs-mc-esp-log.txt"));
        Log.Clear();
        Log.WriteLine($"Injected at {DateTime.Now}");

        GL.InitGL();        

        HookApi.AltInit();
        RH.Attach(render = new Render());
        HookApi.Commit();
        BindManager.Add(InitBinds());

        SetupConsole();
    }

    [UnmanagedCallersOnly(EntryPoint = "Unload", CallConvs = new System.Type[] { typeof(CallConvCdecl) })]
    public static void Unload() { }

    private static void SetupConsole()
    {
        try
        {
            Console.Clear();

            Interop.StartThread(() =>
            {
                while (true)
                {
                    try
                    {
                        //string str = Console.ReadLine();
                    }
                    catch (Exception ex) { Console.WriteLine("ex"); }
                }
            });

            Console.Beep(500, 500);
        }
        catch { }
    }

    private static List<Bind> InitBinds()
    {
        return new List<Bind>()
        {
            new Bind(Keys.NumPad0, () => render.Settings.NoLight = !render.Settings.NoLight ),
            new Bind(Keys.NumPad1, () => render.Settings.NoBackground = !render.Settings.NoBackground ),
            new Bind(Keys.NumPad2, () => render.Settings.NoFog = !render.Settings.NoFog ),
            new Bind(Keys.NumPad3, () => render.Settings.AntiCullFace = !render.Settings.AntiCullFace ),
            new Bind(Keys.NumPad4, () => render.Settings.WorldChams = !render.Settings.WorldChams ),
            new Bind(Keys.NumPad5, () => render.Settings.CaveViewer = !render.Settings.CaveViewer ),
            new Bind(Keys.NumPad6, () => render.Settings.RainbowText = !render.Settings.RainbowText ),
            new Bind(Keys.NumPad7, () => render.Settings.ESP = !render.Settings.ESP ),
        };
    }

}