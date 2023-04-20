using ESP.Structs;
using OpenGL;
using static OpenGL.Enums;
using RH = ESP.RenderHook;

namespace ESP.Utils;
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

    public static void DrawSolidAABB(AABB bb)
    {
        RH.Begin(Mode.Quads);

        GL.Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);

        GL.End();
    }

    public static void DrawTracer(float fx, float fy, float fz, float tx, float ty, float tz)
    {
        RH.Begin(Mode.Lines);
        GL.Vertex3f(fx, fy, fz);
        GL.Vertex3f(tx, ty, tz);
        GL.End();
    }

    public static void Color(Color color) => GL.Color4f(color.R, color.G, color.B, color.A);

    public static float GetDistance(float x1, float y1, float z1, float x2, float y2, float z2) => (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2) + Math.Pow(z1 - z2, 2));
}