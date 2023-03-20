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
    public GLTarget()
    {
        projectionHandle = GCHandle.Alloc(Projection, GCHandleType.Pinned);
        modelviewHandle = GCHandle.Alloc(Modelview, GCHandleType.Pinned);
    }

    public bool IsValid;

    public float[] Projection = new float[16];
    public float[] Modelview = new float[16];

    private GCHandle projectionHandle;
    private GCHandle modelviewHandle;

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
        GL.LoadMatrixf(Projection);

        GL.MatrixMode(Matrix.Modelview);
        GL.LoadMatrixf(Modelview);

        if (options.Box.L.Enabled)
        {
            GL.LineWidth(options.Box.L.LineWidth);
            RU.Color(options.Box.L.Box.Color);
            RU.DrawOutlineAABB(options.Box.L.Box.AABB);
        }

        /*
        if (options.Box.P.Enabled)
        {
            RU.Color(options.Box.P.Box.Color);
            RU.DrawSolidAABB(options.Box.L.Box.AABB);
        }
        */

        if (options.Tracer.Enabled)
        {
            GL.LoadIdentity();
            GL.LineWidth(options.Tracer.LineWidth);      
            RU.Color(options.Tracer.Color);
            RU.DrawTracer(0, 0, -0.1f, Modelview[12] + options.Tracer.OffsetX, Modelview[13] + options.Tracer.OffsetY, Modelview[14] + options.Tracer.OffsetZ);
        }

        /*
        GL.Begin(Mode.Lines);
        GL.Vertex3f(0, 0, -.1f);
        GL.Vertex3f(Modelview[12] + options.Tracer.OffsetX, Modelview[13] + options.Tracer.OffsetY, Modelview[14] + options.Tracer.OffsetZ);
        GL.End();
        */
        /*
        GL.MatrixMode(Matrix.Projection);
        GL.LoadMatrixf(Projection);

        GL.MatrixMode(Matrix.Modelview);
        GL.LoadMatrixf(Modelview);
                
        if (options.Box.L.Enabled)
        {
            GL.LineWidth(options.Box.L.LineWidth);
            RU.Color(options.Box.L.Box.Color);
            RU.DrawOutlineAABB(options.Box.L.Box.AABB);
        }

        if (options.Box.P.Enabled)
        {
            RU.Color(options.Box.P.Box.Color);
            RU.DrawSolidAABB(options.Box.L.Box.AABB);
        }

        if (options.Tracer.Enabled)
        {
            //Console.Write($"Draw tracer to {Modelview[12]} {Modelview[13]} {Modelview[14]}");

            GL.LoadIdentity();
            GL.LineWidth(options.Tracer.LineWidth);
            RU.Color(options.Tracer.Color);
            RU.DrawTracer(0, 0, -0.1f, Modelview[12] + options.Tracer.OffsetX, Modelview[13] + options.Tracer.OffsetY, Modelview[14] + options.Tracer.OffsetZ);
        }
        */
    }

    public void Dispose()
    {
        projectionHandle.Free();
        modelviewHandle.Free();
    }
}