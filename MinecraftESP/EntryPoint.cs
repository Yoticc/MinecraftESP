unsafe class EntryPoint
{
    void Load()
    {
        Log.StartNewSession(LogPath, $"Injected at {DateTime.Now}");

        Config = ConfigFile.GetConfig();

        ((Func<AbstractRenderHook>[])[
            () => new v100.RenderHook(new()),
            () => new v109.RenderHook(new()),
            () => new v115.RenderHook(new()),
            () => new v117.RenderHook(new()),
            () => new vCristalix.RenderHook(new())
        ])[(byte)Config->TargetVersion]().Attach();
        
        BindManager.Add(Range(0, ConfigFile.Config.STATES).Select(i => new Bind(Config->Binds[i], Config->EnableState + i)));
    }
}