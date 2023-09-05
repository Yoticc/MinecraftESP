namespace ESP;
public unsafe class Render
{
    public Settings Settings = new Settings();

    bool nowInInventory;

    public bool Enable(ref Cap cap)
    {
        if (cap == Cap.Lighting && Settings.NoLight) { }
        else if (cap == Cap.Fog && Settings.NoFog) { }
        else if (cap == Cap.CullFace && Settings.AntiCullFace) { }
        else if (cap == Cap.DepthTest && Settings.CaveViewer) { }
        else return true;

        return false;
    }

    public bool Disable(ref Cap cap)
    {
        if (cap == Cap.Blend && Settings.WorldChams) { }
        else if (cap == Cap.Texture2D && Settings.NoBackground) { }
        else return true;

        return false;
    }

    public bool Begin(ref Mode mode)
    {
        if (mode == Mode.TriangleStript && Settings.RainbowText)
            RU.Color(ColorUtils.GetRGB());
        
        return true;
    }

    public bool TranslateF(ref float x, ref float y, ref float z)
    {
        if (x == .5 && y == .4375 && z == .9375)
            SetTarget(Settings.Chest, 0, .0625f, -.4375f);
        else if (x == 1 && y == 0.4375 && z == 0.9375)
            SetTarget(Settings.LargeChest, 0, .0625f, -.4375f);

        return true;
    }

    public bool ScaleF(ref float x, ref float y, ref float z)
    {
        if (x == .9375 && y == .9375 && z == .9375)
            SetTarget(Settings.Player, 0, -1, 0);
        else if (x == .25 && y == .25 && z == .25)
            SetTarget(Settings.Item);
        else if (x == .5 && y == .5 && z == .5)
            SetTarget(Settings.Item);
        else if (x.IsBetween(.666665f, .6666668f) && y.IsBetween(-.6666668f, -.666665f) && z.IsBetween(-.6666668f, -.666665f))
            SetTarget(Settings.Sign);
        else
        {
            SetTarget(Settings.Other);
        }

        return true;
    }

    public bool Ortho(ref double left, ref double right, ref double bottom, ref double top, ref double zNear, ref double zFar)
    {
        if (zNear != 1000 || zFar != 3000)
            return true;

        nowInInventory = true;

        if (!Settings.ESP)
            return true;

        GL.PushAttrib(0x000fffff);
        GL.PushMatrix();

        RH.Disable(Cap.Texture2D);
        RH.Disable(Cap.CullFace);
        RH.Disable(Cap.Lighting);
        RH.Disable(Cap.DepthTest);

        RH.Enable(Cap.LineSmooth);

        RH.Enable(Cap.Blend);
        GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

        foreach (TargetOpt targetOpt in Settings.AsList)
            if (targetOpt.Enabled)
                foreach (GLTarget target in targetOpt.Targets)
                {
                    target.DrawOver(targetOpt);
                }

        GL.PopAttrib();
        GL.PopMatrix();

        return true;
    }

    public void SwapBuffers(nint hdc)
    {
        nowInInventory = false;

        foreach (var target in Settings.AsList)
            target.Targets.Clear();
    }

    void SetTarget(TargetOpt options, float offsetX = 0, float offsetY = 0, float offsetZ = 0)
    {
        if (nowInInventory)
            return;

        var target = new GLTarget();
        var mv = target.Modelview;
        var m3 = new float[4];

        GL.Interface.glGetFloatv(PName.ProjectionMatrix, target.Projection);
        GL.Interface.glGetFloatv(PName.ModelviewMatrix, mv);

        for (int i = 0; i < 4; i++)
            m3[i] = mv[i] * offsetX + mv[i + 4] * offsetY + mv[i + 8] * offsetZ + mv[i + 12];

        for (int i = 0; i < 4; i++)
            mv[12 + i] = m3[i];

        options.Targets.Add(target);
    }
}