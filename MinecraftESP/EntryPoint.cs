using Core;
using Core.Abstracts;
using Core.Utils;
using NAOT;
using static Core.Globals;
using Log = Core.Utils.Logger;

namespace MinecraftESP;
unsafe class EntryPoint
{
    [EntryPoint]
    static void Load()
    {
        Log.SetFile(LogPath);
        Log.Clear();
        Log.WriteLine($"Injected at {DateTime.Now}");

        Config = ConfigFile.GetConfig();

        new Dictionary<MinecraftVersion, Func<AbstractRenderHook>>() {
            { MinecraftVersion.v1, () => new v1.RenderHook(new()) },
            { MinecraftVersion.v19, () => new v19.RenderHook(new()) },
            { MinecraftVersion.v115, () => new v115.RenderHook(new()) },
            { MinecraftVersion.Cristalix, () => new vCristalix.RenderHook(new()) }
        }[Config->TargetVersion]().Attach();

        BindManager.Add(Enumerable.Range(0, ConfigFile.Config.STATES).Select(i => new Bind(Config->Binds[i], Config->EnableState + i)));
    }
}