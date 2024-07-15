namespace Core;
public unsafe struct GLTarget
{
    public fixed float Projection[16];
    public fixed float Modelview[16];

    public void DrawOver(TargetCollection target)
    {
        fixed (float* projection = Projection)
            GL.LoadMatrixf(Matrix.Projection, projection);
        fixed (float* modelview = Modelview)
            GL.LoadMatrixf(Matrix.Modelview, modelview);

        var (x, y, z) = (Modelview[12], Modelview[13], Modelview[14]);

        var distance = RU.GetDistance(x, y, z);
        var options = target.Options;

        if (options.Box.L.Enabled)
        {
            GL.LineWidth(options.Box.L.LineWidth);
            SetColor(options.Box.L.CAABB.Color, distance);
            RU.DrawOutlineAABB(options.Box.L.CAABB.AABB);
        }

        if (options.Box.P.Enabled)
        {
            SetColor(options.Box.P.CAABB.Color, distance);
            RU.DrawSolidAABB(options.Box.P.CAABB.AABB);
        }

        if (options.Tracer.Enabled)
        {
            GL.LoadIdentity();
            GL.LineWidth(options.Tracer.LineWidth);
            SetColor(options.Tracer.Color, distance);
            RU.DrawTracer(0, 0, -0.1f, x + options.Tracer.OffsetX, y + options.Tracer.OffsetY, z + options.Tracer.OffsetZ);
        }
    }

    static void SetColor(Color baseColor, double distance)
    {
        if (baseColor.Equals(Color.RGBColor))
            RU.Color(ColorUtils.GetRGB());
        else if (baseColor.Equals(Color.DistanceColor))
            RU.Color(ColorUtils.GetDistColor(64, distance));
        else RU.Color(baseColor);
    }
}