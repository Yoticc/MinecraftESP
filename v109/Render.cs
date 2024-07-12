using Vec3F = (float, float, float);

namespace v109;
public unsafe class Render : DefaultRender
{
    public void TranslateF(Vec3F vec)
    {
        if (vec == (.5, .4375, .9375))
            SetTarget(Targets.Chest, 0, .0625f, -.4375f);
        else if (vec == (1, .4375, .9375))
            SetTarget(Targets.LargeChest, 0, .0625f, -.4375f);
        else SetTarget(Targets.Other);
    }

    public void ScaleF(Vec3F vec)
    {
        if (vec == (.9375, .9375, .9375))
            SetTarget(Targets.Player, 0, -1, 0);
        else if (vec == (.25, .25, .25))
            SetTarget(Targets.Item);
        else if (vec == (.5, .5, .5))
            SetTarget(Targets.Item);
        else if (vec == (F2D3, -F2D3, -F2D3))
            SetTarget(Targets.Sign);
        else SetTarget(Targets.Other);
    }
}