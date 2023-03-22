using ESP.Structs.Options;
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

    private AABB tracerBox = new AABB(-.1, -.1, -.1, .1, .1, .1);
    public void DrawOver(TargetOpt options)
    {
        GL.MatrixMode(Matrix.Projection);
        GL.Interface.glLoadMatrixf(Projection);

        GL.MatrixMode(Matrix.Modelview);
        GL.Interface.glLoadMatrixf(Modelview);

        if (options.Box.L.Enabled)
        {
            GL.LineWidth(options.Box.L.LineWidth);
            RU.Color(options.Box.L.Box.Color);
            RU.DrawOutlineAABB(options.Box.L.Box.AABB);
        }

        if (options.Box.P.Enabled)
        {
            RU.Color(options.Box.P.Box.Color);
            RU.DrawSolidAABB(options.Box.P.Box.AABB);
        }

        if (options.Tracer.Enabled)
        {
            GL.LoadIdentity();
            GL.LineWidth(options.Tracer.LineWidth);      
            RU.Color(options.Tracer.Color);
            RU.DrawTracer(0, 0, -0.1f, Modelview[12] + options.Tracer.OffsetX, Modelview[13] + options.Tracer.OffsetY, Modelview[14] + options.Tracer.OffsetZ);
        }
    }

    public void Dispose()
    {
        NativeMemory.AlignedFree(Projection);
        NativeMemory.AlignedFree(Modelview);
    }
}