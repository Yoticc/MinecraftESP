namespace Core.Utils;
public unsafe class ConfigFile
{
    public static Config* GetConfig()
    {
        try
        {
            if (!IsExists())
            {
                var defaultConfig = Config.NewConfig(new());
                var defaultConfigJson = defaultConfig->Serialize();
                File.WriteAllText(ConfigPath, defaultConfigJson);
                MessageBox("Config warning", $"Config file not found.\nIt will be created automatically, go to \"{ConfigPath}\" and change it to your preferences, then click OK in this window");
            }

            return Config.Deserialize(File.ReadAllText(ConfigPath)!);
        }
        catch (Exception e)
        {
            MessageBox($"Config exception {e.GetType()}", "An error was thrown when try to get config.\nWill be used default config");

            return Config.NewConfig(new());
        }
    }


    public static bool IsExists() => File.Exists(ConfigPath);

    [StructLayout(LayoutKind.Sequential)]
    public struct Config
    {
        public const int STATES = 8;

        public Config() { }

        public Keys* Binds;
        public bool* EnableState;

        public MinecraftVersion TargetVersion = MinecraftVersion.v115;
        public Keys NoLightBind = Keys.NumPad0, NoBackgroundBind = Keys.NumPad1, NoFogBind = Keys.NumPad2, CaveViewerBind = Keys.NumPad3, PlayerESPBind = Keys.NumPad4, ChestESPBind = Keys.NumPad5, SignESPBind = Keys.NumPad6, ItemESPBind = Keys.NumPad7;
        public bool NoLightEnabled, NoBackgroundEnabled, NoFogEnabled, CaveViewerEnabled, PlayerESPEnabled = true, ChestESPEnabled = true, SignESPEnabled = true, ItemESPEnabled = true;

        // Oh Allah, today I did big HARAM. I wrote this code, it's the worst code I have written in the last few years. Forgive me for my sins ✡:big_booty_latina_in_hijab:🙏🏼
        static string[] hackNames = { "NoLight", "NoBackground", "NoFog", "CaveViewer", "PlayerESP", "ChestESP", "SignESP", "ItemESP" };
        public string Serialize()
        {
            var binds = Binds;
            var enableState = EnableState;
            return
@$"===== README =====
Github - https://github.com/Yoticc/MinecraftESP
UnKnoWnCheaTs - https://www.unknowncheats.me/forum/minecraft/576534-esp-naot.html

Available versions:
 {string.Join("\n ", Enum.GetValues<MinecraftVersion>().Select(v => $"{v} - {MinecraftVersionInfo.Description[v]}"))}]
Available keys for binds - https://github.com/Yoticc/MinecraftESP/blob/master/Core/Utils/Interop.cs

===== Config =====
Target minecraft version: {TargetVersion}

---Keybinds---
{string.Join('\n', hackNames.Select((name, index) => $"{name}: {binds[index]}"))}

---Enable states----
{string.Join('\n', hackNames.Select((name, index) => $"{name}: {enableState[index]}"))}
";
        }

        static int MINECRAFT_VERSION_LINE = 8 + Enum.GetValues<MinecraftVersion>().Length;
        static int KEYBINDS_LINE = MINECRAFT_VERSION_LINE + 3;
        static int ENABLE_STATES_LINE = KEYBINDS_LINE + STATES + 2;
        public static Config* Deserialize(string data)
        {
            try
            {
                var lines = data.Replace(" ", "").Split('\n').Select(l => l.Split(':')).ToArray();
                var config = NewConfig(new());

                config->TargetVersion = Enum.Parse<MinecraftVersion>(lines[MINECRAFT_VERSION_LINE][1]);
                for (int i = 0; i < STATES; i++)
                    config->Binds[i] = Enum.Parse<Keys>(lines[i + KEYBINDS_LINE][1]);
                for (int i = 0; i < STATES; i++)
                    config->EnableState[i] = bool.Parse(lines[i + ENABLE_STATES_LINE][1]);

                return config;
            }
            catch
            {
                MessageBox("You're stupid shit, why the hell did you change config file incorrectly, as if it's difficult to do, this is the task of a 9-year-old boy, and you, a 21-year-old man, couldn't cope with it.\n" +
                           "Please delete config file and let program re-create it, AND BE VERY CAREFUL NEXT TIME.\n" +
                           "Otherwise...\n" +
                           "I, the program, will have to insult you again.");
            }

            return null; // 🤡
        }

        public static Config* NewConfig(Config from)
        {
            var ptr = New(from);
            ptr->Binds = (Keys*)((byte*)ptr + sizeof(Keys*) + sizeof(bool*) + sizeof(MinecraftVersion));
            ptr->EnableState = (bool*)((byte*)ptr->Binds + sizeof(Keys) * STATES);
            return ptr;
        }
    }
}