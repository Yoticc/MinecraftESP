using Core;
using Core.Abstracts;
using OpenGL;
using static Core.Globals;
using static OpenGL.Enums;
using Vec3F = (float x, float y, float z);

namespace v115;
public unsafe class Render : DefaultRender
{
    public bool TranslateF(Vec3F vec)
    {
        if (vec == (.5, .4375, .9375))
            SetTarget(Targets.Chest, 0, .0625f, -.4375f);
        else if (vec == (1, .4375, .9375))
            SetTarget(Targets.LargeChest, 0, .0625f, -.4375f);
        else if (vec == (0, 0.2, 0))
            SetTarget(Targets.Player);
        else
            SetTarget(Targets.Other);

        return true;
    }

    public bool ScaleF(Vec3F vec)
    {
        if (vec == (.9375, .9375, .9375))
            SetTarget(Targets.Player, 0, -1, 0);
        else if (vec == (.25, .25, .25))
            SetTarget(Targets.Item);
        else if (vec == (.5, .5, .5))
            SetTarget(Targets.Item);
        else if (vec == (F2D3, -F2D3, -F2D3))
            SetTarget(Targets.Sign);
        else
            SetTarget(Targets.Other);

        return true;
    }

    public new bool Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (zNear != 1000 || zFar != 3000)
            return true;

        nowInInventory = true;

        GL.PushAttrib(0x000fffff);
        GL.PushMatrix();

        GL.Disable(Cap.Texture2D);
        GL.Disable(Cap.CullFace);
        GL.Disable(Cap.Lighting);
        GL.Disable(Cap.DepthTest);

        GL.Enable(Cap.LineSmooth);

        GL.Enable(Cap.Blend);
        GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

        Draw(Config->PlayerESPEnabled, Targets.Player);
        Draw(Config->ChestESPEnabled, Targets.Chest, Targets.LargeChest);
        Draw(Config->ItemESPEnabled, Targets.Item);
        Draw(Config->SignESPEnabled, Targets.Sign);
        Draw(Targets.Other.Enabled, Targets.Other);

        GL.PopAttrib();
        GL.PopMatrix();

        return true;

        void Draw(bool enabled, params TargetOpt[] targetOpts)
        {
            if (enabled)
                foreach (var targetOpt in targetOpts)
                    foreach (var target in targetOpt.Targets)
                        target.DrawOver(targetOpt);
        }
    }
}