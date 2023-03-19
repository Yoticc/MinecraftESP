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
    private static string logPath = @"C:\log.txt";
    public static void Log(object obj)
    {
        File.AppendAllText(logPath, $"{obj}\n");
    }

    private Render render;
    public void Load()
    {
        File.WriteAllText(logPath, "");
        Log($"Injected at {DateTime.Now}");

        Interop.LoadLibrary(@"D:\VS\repos\MinecraftESP\MinecraftESP\bin\Release\net7.0\win-x64\Hook.dll");
        Interop.LoadLibrary(@"D:\VS\repos\MinecraftESP\MinecraftESP\bin\Release\net7.0\win-x64\OpenGL.dll");

        HookApi.AltInit();
        GL.InitGL();

        RenderHook.Init(render = new Render());

        HookApi.Commit();

        BindManager.Add(InitBinds());

        //Interop.StartThread(StartConsoleHandler);
        //SetupConsole();
        //WriteStartMessage();
    }


    private List<Bind> InitBinds()
    {
        return new List<Bind>()
        {
            new Bind(Keys.Z, () => render.Settings.NoLight = !render.Settings.NoLight ),
            new Bind(Keys.X, () => render.Settings.NoBackground = !render.Settings.NoBackground ),
            new Bind(Keys.C, () => render.Settings.NoFog = !render.Settings.NoFog ),
            new Bind(Keys.V, () => render.Settings.AntiCullFace = !render.Settings.AntiCullFace ),
            new Bind(Keys.B, () => render.Settings.WorldChams = !render.Settings.WorldChams ),
            new Bind(Keys.N, () => render.Settings.CaveViewer = !render.Settings.CaveViewer ),
            new Bind(Keys.M, () => render.Settings.RainbowText = !render.Settings.RainbowText ),
            new Bind(Keys.L, () => render.Settings.ESP = !render.Settings.ESP ),
        };
    }

    private bool isDefConsoleInvalid = false;
    private string diservePathToConsoleInpuFile = @"C:\mccon.txt";
    private void SetupConsole()
    {
        try
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.Unicode;
        } 
        catch (IOException ex) { isDefConsoleInvalid = true; }

        if (isDefConsoleInvalid)
        {
            File.Create(diservePathToConsoleInpuFile).Dispose();
            Console.SetIn(File.OpenText(diservePathToConsoleInpuFile));
        }
    }


    private void StartConsoleHandler()
    {
        string input;
        string[] args;
        Cap cap;
        while (true)
        {
            try
            {
                input = Console.ReadLine();
                args = input.Split(' ');

            }
            catch { Console.WriteLine("ex"); }
        }
    }

    private void WriteStartMessage()
    {
        ConsoleColor old = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("ESP was successfully initialized!");
        Console.ForegroundColor = old;
    }

    public void Unload()
    {
        Log($"Uninjected at {DateTime.Now}");
        RH.EnableHook.Detach();
        RH.TranslateFHook.Detach();
        RH.BeginHook.Detach();
        RH.ScaleFHook.Detach();
        RH.DisableHook.Detach();
        RH.OrthoHook.Detach();
    }
}