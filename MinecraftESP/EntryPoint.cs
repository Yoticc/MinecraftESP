using Core.Abstracts;
using Core.Utils;
using Memory;
using System.Runtime.CompilerServices;
using static Core.Globals;
using static Core.Utils.Interop;

using Log = Core.Utils.Logger;
using Native = System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;

namespace Core;
public unsafe class EntryPoint
{
    [Native(EntryPoint = "Load", CallConvs = new[] { typeof(CallConvCdecl) })]
    public static void Load()
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