namespace Core;
public unsafe class DefaultRender : AbstractRender
{
    public virtual void SwapBuffers(nint hdc)
    {
        nowInInventory = false;

        foreach (var options in TargetCollection.AsArray)
            options.Targets.Clear();
    }

    public virtual void Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (zNear != 1000 || zFar != 3000)
            return;

        nowInInventory = true;

        Push();

        Draw(Cfg->PlayerESPEnabled, TargetCollection.Player);
        Draw(Cfg->ChestESPEnabled, TargetCollection.Chest, TargetCollection.LargeChest);
        Draw(Cfg->ItemESPEnabled, TargetCollection.Item);
        Draw(Cfg->SignESPEnabled, TargetCollection.Sign);
        Draw(TargetCollection.Other.Options.Enabled, TargetCollection.Other);

        Pop();

        void Draw(bool enabled, params TargetCollection[] targetOpts)
        {
            if (enabled)
                foreach (var targetOpt in targetOpts)
                    foreach (var target in targetOpt.Targets)
                        target.DrawOver(targetOpt);
        }
    }

    protected bool nowInInventory;

    public void SetTarget(TargetCollection target, float x = 0, float y = 0, float z = 0)
    {
        if (nowInInventory)
            return;

        if (!target.Options.Enabled)
            return;

        GLTarget targetEntity;

        GL.GetFloatv(PName.ProjectionMatrix, targetEntity.Projection);
        GL.GetFloatv(PName.ModelviewMatrix, targetEntity.Modelview);

        var mv = targetEntity.Modelview;
        mv[12] = mv[0] * x + mv[4] * y + mv[8] * z + mv[12];
        mv[13] = mv[1] * x + mv[5] * y + mv[9] * z + mv[13];
        mv[14] = mv[2] * x + mv[6] * y + mv[10] * z + mv[14];
        mv[15] = mv[3] * x + mv[7] * y + mv[11] * z + mv[15];

        target.Targets.Add(targetEntity);
    }
}