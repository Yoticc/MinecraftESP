using Vec3F = (float, float, float);

namespace v109;
public unsafe class Render : DefaultRender
{
    public void TranslateF(Vec3F vec)
    {
        if (vec == (.5, .4375, .9375))
            SetTarget(Chest, 0, .0625f, -.4375f);
        else if (vec == (1, .4375, .9375))
            SetTarget(LargeChest, 0, .0625f, -.4375f);
        else SetTarget(TargetCollection.Other);
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
        else SetTarget(TargetCollection.Other);
    }
}