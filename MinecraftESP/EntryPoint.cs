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

    Render render;
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

        StartConsoleHandler();
        InitBinds();

        WriteStartMessage();
    }

    private void InitBinds()
    {
        BindManager.Add(
            new Bind(Keys.N, () => { Console.WriteLine("Funny"); }),
            new Bind(Keys.M, () => { Console.WriteLine("Not Funny"); })
        );
        BindManager.StartReceive();
    }

    private void StartConsoleHandler()
    {
        Interop.StartThread(() =>
        {
            Console.WriteLine("Thread started");
            string input;
            string[] args;
            Cap cap;
            while (true)
            {
                try
                {
                    input = Console.ReadLine();
                    args = input.Split(' ');

                    if (args[0] == "reset")
                    {
                        render.blockedEnableCaps.Clear();
                        render.blockedDisableCaps.Clear();
                        render.extraEnableCaps.Clear();
                        render.extraDisableCaps.Clear();
                        continue;
                    }

                    cap = Enum.Parse<Cap>(args[3]);

                    if (args[0] == "enable")
                    {
                        if (args[1] == "block")
                        {
                            if (args[2] == "add")
                            {
                                render.blockedEnableCaps.Add(cap);
                            }
                            else if (args[2] == "remove")
                            {
                                render.blockedEnableCaps.Remove(cap);
                            }
                        }
                        else if (args[1] == "extra")
                        {
                            if (args[2] == "add")
                            {
                                render.extraEnableCaps.Add(cap);
                            }
                            else if (args[2] == "remove")
                            {
                                render.extraEnableCaps.Remove(cap);
                                ((delegate* unmanaged<Cap, void>)RenderHook.DisableHook)(cap);
                            }
                        }
                    }
                    else if (args[0] == "disable")
                    {
                        if (args[1] == "block")
                        {
                            if (args[2] == "add")
                            {
                                render.blockedDisableCaps.Add(cap);
                            }
                            else if (args[2] == "remove")
                            {
                                render.blockedDisableCaps.Remove(cap);
                            }
                        }
                        else if (args[1] == "extra")
                        {
                            if (args[2] == "add")
                            {
                                render.extraDisableCaps.Add(cap);
                            }
                            else if (args[2] == "remove")
                            {
                                render.extraDisableCaps.Remove(cap);
                            }
                        }
                    }
                }
                catch { Console.WriteLine("ex"); }
            }
        });
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