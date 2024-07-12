namespace Core;
public unsafe class DefaultRender : AbstractRender
{
    public virtual bool Enable(Cap cap)
    {
        if (cap == Cap.Lighting && Config->NoLightEnabled) { }
        else if (cap == Cap.Fog && Config->NoFogEnabled) { }
        else if (cap == Cap.DepthTest && Config->CaveViewerEnabled) { }
        else return true;

        return false;
    }

    public virtual bool Disable(Cap cap)
    {
        if (cap == Cap.Texture2D && Config->NoBackgroundEnabled) { }
        else return true;

        return false;
    }

    public virtual void SwapBuffers(nint hdc)
    {
        nowInInventory = false;

        foreach (var options in Targets.AsArray)
        {
            foreach (var target in options.Targets)
                target.Dispose();
            options.Targets.Clear();
        }
    }

    public virtual void Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (zNear != 1000 || zFar != 3000)
            return;

        nowInInventory = true;

        Push();

        Draw(Config->PlayerESPEnabled, Targets.Player);
        Draw(Config->ChestESPEnabled, Targets.Chest, Targets.LargeChest);
        Draw(Config->ItemESPEnabled, Targets.Item);
        Draw(Config->SignESPEnabled, Targets.Sign);
        Draw(Targets.Other.Enabled, Targets.Other);

        Pop();

        void Draw(bool enabled, params TargetOpt[] targetOpts)
        {
            if (enabled)
                foreach (var targetOpt in targetOpts)
                    foreach (var target in targetOpt.Targets)
                        target.DrawOver(targetOpt);
        }
    }

    protected void Push()
    {
        GL.PushAttrib(0x000fffff);
        GL.PushMatrix();

        GL.Disable(Cap.Texture2D);
        GL.Disable(Cap.CullFace);
        GL.Disable(Cap.Lighting);
        GL.Disable(Cap.DepthTest);

        GL.Enable(Cap.LineSmooth);

        GL.Enable(Cap.Blend);
        GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);
    }

    protected void Pop()
    {
        GL.PopAttrib();
        GL.PopMatrix();
    }
}