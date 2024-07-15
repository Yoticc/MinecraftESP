unsafe class EntryPoint
{
    void Load()
    {
        Log.StartNewSession("cs-mc-esp-log.txt", $"Injected at {DateTime.Now}");

        Cfg = ConfigFile.LoadConfig();
            
        ((Func<AbstractRenderHook>[])[
            () => new v100.RenderHook(),
            () => new v109.RenderHook(),
            () => new v115.RenderHook(),
            () => new v117.RenderHook(),
            () => new vCristalix.RenderHook()
        ])[(int)Cfg->TargetVersion]().Attach();
        
        BindManager.Add(range(0, Config.STATES).select(i => new Bind(Cfg->Binds[i], Cfg->EnableState + i)));
    }
}