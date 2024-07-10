namespace Core;
public static unsafe class Globals
{
    static Globals()
    {
        ExecModule = GetModuleHandle("MinecraftESP");
        ExecFile = ModulesInfo.GetModuleByName("MinecraftESP")!.Path;
        ExecDir = Path.GetDirectoryName(ExecFile)!;
        LocalAppdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        LogPath = Path.Combine(LocalAppdata, "cs-mc-esp-log.txt");
        ConfigPath = Path.Combine(ExecDir, "config.json");
    }

    public static nint ExecModule;
    public static string ExecFile;
    public static string ExecDir;
    public static string LocalAppdata;

    public static string LogPath;
    public static string ConfigPath;

    public static ConfigFile.Config* Config;
}