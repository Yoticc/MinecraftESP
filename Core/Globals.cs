global using Core.Utils;
global using Hook;
global using OpenGL;
global using System.Diagnostics;
global using System.Drawing;
global using System.Runtime.InteropServices;
global using System.Text;
global using static korn;
global using static Core.Globals;
global using static Core.Utils.Interop;
global using static Memory.MemEx;
global using static OpenGL.Enums;
global using RU = Core.Utils.RenderUtils;

namespace Core;
public static unsafe class Globals
{
    static Globals()
    {
        ExecModule = GetModuleHandle("MinecraftESP");
        ExecFile = ModulesInfo.GetModuleByName("MinecraftESP").Path;
        ExecDir = Path.GetDirectoryName(ExecFile)!;
        LocalAppdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        LogPath = Path.Combine(LocalAppdata, "cs-mc-esp-log.txt");
        ConfigPath = Path.Combine(ExecDir, "config.json");
    }

    public const int MAX_PATH = 260;

    public static nint ExecModule;
    public static string ExecFile;
    public static string ExecDir;
    public static string LocalAppdata;

    public static string LogPath;
    public static string ConfigPath;

    public static ConfigFile.Config* Config;
}