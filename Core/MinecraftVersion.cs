using static Core.MinecraftVersion;

namespace Core;

public enum MinecraftVersion
{
    v100,
    v109,
    v115,
    v117,
    Cristalix
}

public class MinecraftVersionInfo
{
    public static MinecraftVersion[] AvaibleVersions =
    [
        v100,
        v109,
        v115,
        v117,
        Cristalix
    ];

    public static Dictionary<MinecraftVersion, string> Descriptions = new()
    {
        { v100, "For 1.0 - 1.8.9" },
        { v109, "For 1.9 - 1.14.4" },
        { v115, "For 1.15 - 1.16.5" },
        { v117, "For 1.17 - ???" },
        { Cristalix, "For Cristalix (RU: cristalix.gg)" }
    };
}