using ESP.Structs.Options;
using ESP.Utils;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Enums;
using RH = ESP.RenderHook;
using RU = ESP.Utils.RenderUtils;

namespace ESP.Structs;
public unsafe struct GLTarget : IDisposable
{
    private const int DATA_SIZE = 16;
    private const int DATA_SIZE_P2 = DATA_SIZE * DATA_SIZE;
    private const int FLOAT_SIZE = sizeof(float);
    private const int FLOAT_DATA_SIZE = DATA_SIZE * FLOAT_SIZE;
    private const int FLOAT_DATA_SIZE_P2 = FLOAT_DATA_SIZE * FLOAT_DATA_SIZE;

    public GLTarget()
    {
        Projection = (float*)NativeMemory.AlignedAlloc(FLOAT_DATA_SIZE, FLOAT_DATA_SIZE_P2);
        Modelview = (float*)NativeMemory.AlignedAlloc(FLOAT_DATA_SIZE, FLOAT_DATA_SIZE_P2);
    }

    public bool IsValid;

    public float* Projection;
    public float* Modelview;

    public void DrawDuring(TargetOpt options)
    {
        if (options.Chams.Enabled)
        {
            if (options.Chams.Colored)
                RU.Color(options.Chams.Color);

            if (options.Chams.ThroughWall)
                GL.PolygonOffset(1, 1100000);
        }
    }

    public void DrawOver(TargetOpt options)
    {
        GL.MatrixMode(Matrix.Projection);
        GL.Interface.glLoadMatrixf(Projection);

        GL.MatrixMode(Matrix.Modelview);
        GL.Interface.glLoadMatrixf(Modelview);

        float dist = RU.GetDistance(0, 0, 0, Modelview[12], Modelview[13], Modelview[14]);

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
            RU.DrawTracer(0, 0, -0.1f, Modelview[12] + options.Tracer.OffsetX, Modelview[13] + options.Tracer.OffsetY, Modelview[14] + options.Tracer.OffsetZ);
        }
    }

    private static void SetColor(Color baseColor, float dist)
    {
        if (baseColor.Equals(Color.RGBColor))
            RU.Color(ColorUtils.GetRGB());
        else if (baseColor.Equals(Color.DistanceColor))
            RU.Color(ColorUtils.GetDistColor(64, dist));
        else RU.Color(baseColor);
    }

    public void Dispose()
    {
        NativeMemory.AlignedFree(Projection);
        NativeMemory.AlignedFree(Modelview);
    }
}