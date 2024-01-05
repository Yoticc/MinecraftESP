using Core;
using Core.Abstracts;
using Vec3F = (float x, float y, float z);
using Vec3D = (double x, double y, double z);

namespace vCristalix;
public unsafe class Render : DefaultRender
{
    public bool TranslateF(Vec3F vec)
    {
        if (vec == (.5, .4375, .9375))
            SetTarget(Targets.Chest, 0, .0625f, -.4375f);
        else if (vec == (1, .4375, .9375))
            SetTarget(Targets.LargeChest, 0, .0625f, -.4375f);
        else
            SetTarget(Targets.Other);

        return true;
    }

    public bool ScaleD(Vec3D vec)
    {
        if (vec == (.9375, .9375, .9375))
            SetTarget(Targets.Player, 0, -1, 0);
        else if (vec == (.25, .25, .25))
            SetTarget(Targets.Item);
        else if (vec == (.5, .5, .5))
            SetTarget(Targets.Item);
        else
            SetTarget(Targets.Other);

        return true;
    }

    public bool ScaleF(Vec3F vec)
    {
        if (vec == (F2D3, -F2D3, -F2D3))
            SetTarget(Targets.Sign);

        return true;
    }
}