namespace Core;
public unsafe abstract class AbstractRender
{
    public virtual bool Enable(Cap cap)
    {
        if (cap == Cap.Lighting && Cfg->NoLightEnabled) { }
        else if (cap == Cap.Fog && Cfg->NoFogEnabled) { }
        else if (cap == Cap.DepthTest && Cfg->CaveViewerEnabled) { }
        else return true;

        return false;
    }

    public virtual bool Disable(Cap cap)
    {
        if (cap == Cap.Texture2D && Cfg->NoBackgroundEnabled) { }
        else return true;

        return false;
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
        GL.BlendFunc(FactorEnum.SrcAlpha, FactorEnum.OneMinusSrcAlpha);
    }

    protected void Pop()
    {
        GL.PopAttrib();
        GL.PopMatrix();
    }   
}