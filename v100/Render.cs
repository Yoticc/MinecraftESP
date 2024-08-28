using Vec3F = (float, float, float);

namespace v100;
public unsafe class Render : DefaultRender
{
    public void TranslateF(Vec3F vec)
    {
        if (vec == (.5, .4375, .9375))
            SetTarget(Chest, 0, .0625f, -.4375f);
        else if (vec == (1, .4375, .9375))
            SetTarget(LargeChest, 0, .0625f, -.4375f);
        else SetTarget(Other);
    }

    public void ScaleF(Vec3F vec)
    {
        if (vec == (.9375, .9375, .9375))
            SetTarget(Player, 0, -1, 0);
        else if (vec == (.25, .25, .25))
            SetTarget(Item);
        else if (vec == (.5, .5, .5))
            SetTarget(Item);
        else if (vec == (F2D3, -F2D3, -F2D3))
            SetTarget(Sign);
        else SetTarget(Other);
    }

    public override void Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (zNear != 1000 || zFar != 3000)
            return;

        nowInInventory = true;

        Push();

        Draw(Cfg->PlayerESPEnabled, Player);
        Draw(Cfg->ChestESPEnabled, Chest, LargeChest);
        GL.Scale(2, 2, 2);
        Draw(Cfg->ItemESPEnabled, Item);
        GL.Scale(.5f, .5f, .5f);
        Draw(Cfg->SignESPEnabled, Sign);
        Draw(Other.Options.Enabled, Other);

        Pop();

        void Draw(bool enabled, params TargetCollection[] targetOpts)
        {
            if (enabled)
                foreach (var targetOpt in targetOpts)
                    foreach (var target in targetOpt.Targets)
                        target.DrawOver(targetOpt);
        }
    }
}