namespace Core.Abstracts;
public unsafe abstract class AbstractRender
{
    protected const float F2D3 = 2f / 3;
    protected const float F2D30 = 2f / 30;

    public Targets Targets = new();
    protected bool nowInInventory;

    public void SetTarget(TargetOpt options, float x = 0, float y = 0, float z = 0)
    {
        if (nowInInventory)
            return;

        if (!options.Enabled)
            return;

        var target = new GLTarget();
        var mv = target.Modelview;
        var mt = stackalloc float[4];

        GL.GetFloatv(PName.ProjectionMatrix, target.Projection);
        GL.GetFloatv(PName.ModelviewMatrix, mv);

        mv[12] = mt[0] = mv[0] * x + mv[4] * y + mv[8] * z + mv[12];
        mv[13] = mt[1] = mv[1] * x + mv[5] * y + mv[9] * z + mv[13];
        mv[14] = mt[2] = mv[2] * x + mv[6] * y + mv[10] * z + mv[14];
        mv[15] = mt[3] = mv[3] * x + mv[7] * y + mv[11] * z + mv[15];

        options.Targets.Add(target);
    }
}