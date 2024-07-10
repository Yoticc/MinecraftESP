using static Core.MinecraftVersion;

namespace Core;

public enum MinecraftVersion
{
    v1,
    v19,
    v115,
    Cristalix
}

public class MinecraftVersionInfo
{
    public static MinecraftVersion[] AvaibleVersions =
    [
        v1,
        v19,
        v115,
        Cristalix
    ];

    public static Dictionary<MinecraftVersion, string> Description = new()
    {
        { v1, "For 1.0 - 1.8.9" },
        { v19, "For 1.9 - 1.14.4" },
        { v115, "For 1.15 - ???" },
        { Cristalix, "For Cristalix (RU: cristalix.gg)" }
    };
}