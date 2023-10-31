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
    {
        MinecraftVersion.v1,
        MinecraftVersion.v19,
        MinecraftVersion.Cristalix
    };

    public static Dictionary<MinecraftVersion, string> Description = new()
    {
        { MinecraftVersion.v1, "For 1.0 - 1.8.9" },
        { MinecraftVersion.v19, "For 1.9 - 1.14.4" },
        { MinecraftVersion.v115, "For 1.15 - ???" },
        { MinecraftVersion.Cristalix, "For Cristalix (RU: cristalix.gg)" }
    }; 
}