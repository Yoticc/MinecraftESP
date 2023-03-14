using ESP.Structs;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenGL.Enums;
using Render = ESP.Render;
using RH = ESP.RenderHook;

namespace ESP;
public class RenderUtils
{
    public static void Push()
    {
        GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

        RH.Enable(Cap.LineSmooth);
        RH.Disable(Cap.Texture2D);
        RH.Enable(Cap.CullFace);
        RH.Disable(Cap.DepthTest);
    }

    public static void Pop()
    {
        RH.Enable(Cap.DepthTest);
        RH.Enable(Cap.Texture2D);
        RH.Disable(Cap.LineSmooth);
    }

    public static void DrawOutlineAABB(AABB bb)
    {
        RH.Begin(Mode.Lines);

        GL.Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);

        GL.End();
    }
}