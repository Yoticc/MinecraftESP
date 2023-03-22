global using Log = ESP.Utils.LogManger;
using ESP.Utils;
using Hook;
using OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Enums;
using Keys = ESP.Utils.Interop.Keys;
using RH = ESP.RenderHook;
namespace ESP;
public unsafe class EntryPoint
{
    private static Render render; //CallConvCdecl

    [UnmanagedCallersOnly(EntryPoint = "Load", CallConvs = new System.Type[] { typeof(CallConvCdecl) })]
    public static void Load()
    {
        Log.SetFile(@"B:\log.txt");
        Log.Clear();
        Log.Write("Injected at");
        Log.WriteLine(DateTime.Now);

        Interop.LoadLibrary(@"D:\VS\repos\MinecraftESP\MinecraftESP\bin\Release\net7.0\win-x64\Hook.dll");
        Interop.LoadLibrary(@"D:\VS\repos\MinecraftESP\MinecraftESP\bin\Release\net7.0\win-x64\OpenGL.dll");

        HookApi.AltInit();
        GL.InitGL();

        RenderHook.Init(render = new Render());

        HookApi.Commit();

        BindManager.Add(InitBinds());
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

    public static void Unload()
    {
        Log.Write("Uninjected at");
        Log.WriteLine(DateTime.Now);
        RH.EnableHook.Detach();
        RH.TranslateFHook.Detach();
        RH.BeginHook.Detach();
        RH.ScaleFHook.Detach();
        RH.DisableHook.Detach();
        RH.OrthoHook.Detach();
    }
}