﻿namespace Core;
public unsafe struct GLTarget : IDisposable
{
    public GLTarget()
    {
        Projection = Alloc<float>(16);
        Modelview = Alloc<float>(16);
    }

    public float* Projection, Modelview;

    public void DrawOver(TargetOpt options)
    {
        GL.LoadMatrixf(Matrix.Projection, Projection);
        GL.LoadMatrixf(Matrix.Modelview, Modelview);

        var (x, y, z) = (Modelview[12], Modelview[13], Modelview[14]);

        var dist = RU.GetDistance(x, y, z);

        if (options.Box.L.Enabled)
        {
            GL.LineWidth(options.Box.L.LineWidth);
            SetColor(options.Box.L.CAABB.Color, dist);
            RU.DrawOutlineAABB(options.Box.L.CAABB.AABB);
        }

        if (options.Box.P.Enabled)
        {
            SetColor(options.Box.P.CAABB.Color, dist);
            RU.DrawSolidAABB(options.Box.P.CAABB.AABB);
        }

        if (options.Tracer.Enabled)
        {
            GL.LoadIdentity();
            GL.LineWidth(options.Tracer.LineWidth);
            SetColor(options.Tracer.Color, dist);
            RU.DrawTracer(0, 0, -0.1f, x + options.Tracer.OffsetX, y + options.Tracer.OffsetY, z + options.Tracer.OffsetZ);
        }
    }

    static void SetColor(Color baseColor, float dist)
    {
        if (baseColor.Equals(Color.RGBColor))
            RU.Color(ColorUtils.GetRGB());
        else if (baseColor.Equals(Color.DistanceColor))
            RU.Color(ColorUtils.GetDistColor(64, dist));
        else RU.Color(baseColor);
    }

    public void Dispose() => Free(Projection, Modelview);
}