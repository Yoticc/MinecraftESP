using Hook;
using OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Enums;
using Keys = ESP.Interop.Keys;

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

        Interop.StartThread(StartConsoleHandler);
        BindManager.Add(InitBinds());

        WriteStartMessage();
    }


    private List<Bind> InitBinds()
    {
        return new List<Bind>()
        {
            new Bind(Keys.Z, () => render.NoLight = !render.NoLight ),
            new Bind(Keys.X, () => render.NoBackground = !render.NoBackground ),
            new Bind(Keys.C, () => render.NoFog = !render.NoFog ),
            new Bind(Keys.V, () => render.AntiCullFace = !render.AntiCullFace ),
            new Bind(Keys.B, () => render.WorldChams = !render.WorldChams ),
            new Bind(Keys.N, () => render.CaveViewer = !render.CaveViewer ),
            new Bind(Keys.M, () => render.RainbowText = !render.RainbowText ),
        };
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
        Console.Clear();
        ConsoleColor old = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("ESP was successfully initialized!");
        Console.ForegroundColor = old;
    }

    public void Unload()
    {
        Log($"Uninjected at {DateTime.Now}");
    }
}