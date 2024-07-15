unsafe static class ConfigFile
{
    static readonly string ConfigPath = Path.Combine(Path.GetDirectoryName(ModulesInfo.GetModuleByName("MinecraftESP")!.Path)!, "config.json");

    public static Config* LoadConfig()
    {
        try
        {
            if (!IsExists)
            {
                var defaultConfig = Config.NewConfig(new());
                var defaultConfigJson = defaultConfig->Serialize();
                File.WriteAllText(ConfigPath, defaultConfigJson);
                MessageBox("Config warning", $"Config file not found.\nIt will be created automatically, go to \"{ConfigPath}\" and change it to your preferences");
            }

            return Config.Deserialize(File.ReadAllText(ConfigPath)!);
        }
        catch (Exception e)
        {
            MessageBox($"Config exception {e.GetType()}", "An error was thrown when try to get config.\nWill be used default config");

            return Config.NewConfig(new());
        }
    }

    public static bool IsExists => File.Exists(ConfigPath);
}