global using RU = Core.Utils.RenderUtils;
global using RH = v112.RenderHook;
global using static Core.Globals;
using Core;
using Core.Utils;
using OpenGL;
using static OpenGL.Enums;

namespace v112;
public unsafe class Render : AbstractRender
{
    public Render(Targets targets) : base(targets) { }

    bool nowInInventory;

    public bool Enable(ref Cap cap)
    {
        if (cap == Cap.Lighting && Config->NoLightEnabled) { }
        else if (cap == Cap.Fog && Config->NoFogEnabled) { }
        else if (cap == Cap.CullFace && Config->AntiCullFaceEnabled) { }
        else if (cap == Cap.DepthTest && Config->CaveViewerEnabled) { }
        else return true;

        return false;
    }

    public bool Disable(ref Cap cap)
    {
        if (cap == Cap.Blend && Config->WorldChamsEnabled) { }
        else if (cap == Cap.Texture2D && Config->NoBackgroundEnabled) { }
        else return true;

        return false;
    }

    public bool Begin(ref Mode mode)
    {
        if (mode == Mode.TriangleStript && Config->RainbowTextEnabled)
            RU.Color(ColorUtils.GetRGB());

        return true;
    }

    public bool TranslateF(ref float x, ref float y, ref float z)
    {
        if (x == .5 && y == .4375 && z == .9375)
            SetTarget(Targets.Chest, 0, .0625f, -.4375f);
        else if (x == 1 && y == 0.4375 && z == 0.9375)
            SetTarget(Targets.LargeChest, 0, .0625f, -.4375f);

        return true;
    }

    public bool ScaleF(ref float x, ref float y, ref float z)
    {
        if (x == .9375 && y == .9375 && z == .9375)
            SetTarget(Targets.Player, 0, -1, 0);
        else if (x == .25 && y == .25 && z == .25)
            SetTarget(Targets.Item);
        else if (x == .5 && y == .5 && z == .5)
            SetTarget(Targets.Item);
        else if (x.IsBetween(.666665f, .6666668f) && y.IsBetween(-.6666668f, -.666665f) && z.IsBetween(-.6666668f, -.666665f))
            SetTarget(Targets.Sign);
        else
        {
            SetTarget(Targets.Other);
        }

        return true;
    }

    public bool Ortho(ref double left, ref double right, ref double bottom, ref double top, ref double zNear, ref double zFar)
    {
        if (zNear != 1000 || zFar != 3000)
            return true;

        nowInInventory = true;

        if (!Config->ESPEnabled)
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

        foreach (TargetOpt targetOpt in Targets.AsList)
            if (targetOpt.Enabled)
                foreach (GLTarget target in targetOpt.Targets)
                    target.DrawOver(targetOpt);

        GL.PopAttrib();
        GL.PopMatrix();

        return true;
    }

    public void SwapBuffers(nint hdc)
    {
        nowInInventory = false;

        foreach (var options in Targets.AsList)
        {
            foreach (var target in options.Targets)
                target.Dispose();
            options.Targets.Clear();
        }
    }

    void SetTarget(TargetOpt options, float offsetX = 0, float offsetY = 0, float offsetZ = 0)
    {
        if (nowInInventory)
            return;

        var target = new GLTarget();
        var mv = target.Modelview;
        var mt = stackalloc float[4];

        GL.GetFloatv(PName.ProjectionMatrix, target.Projection);
        GL.GetFloatv(PName.ModelviewMatrix, mv);

        mt[0] = mv[0] * offsetX + mv[4] * offsetY + mv[8] * offsetZ + mv[12];
        mt[1] = mv[1] * offsetX + mv[5] * offsetY + mv[9] * offsetZ + mv[13];
        mt[2] = mv[2] * offsetX + mv[6] * offsetY + mv[10] * offsetZ + mv[14];
        mt[3] = mv[3] * offsetX + mv[7] * offsetY + mv[11] * offsetZ + mv[15];

        mv[12] = mt[0];
        mv[13] = mt[1];
        mv[14] = mt[2];
        mv[15] = mt[3];

        options.Targets.Add(target);
    }
}