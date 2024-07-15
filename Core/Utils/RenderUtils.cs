using static OpenGL.GL;

namespace Core;
public class RenderUtils
{    
    public static void DrawOutlineAABB(AABB bb)
    {
        Begin(Mode.Lines);

        Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);

        End();
    }

    public static void DrawSolidAABB(AABB bb)
    {
        Begin(Mode.Quads);

        Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MinZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MaxX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MaxX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MinZ);
        Vertex3d(bb.MinX, bb.MinY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MaxZ);
        Vertex3d(bb.MinX, bb.MaxY, bb.MinZ);

        End();
    }

    public static void DrawTracer(float fx, float fy, float fz, float tx, float ty, float tz)
    {
        Begin(Mode.Lines);
        Vertex3f(fx, fy, fz);
        Vertex3f(tx, ty, tz);
        End();
    }

    public static void Color(Color color) => Color4f(color.R, color.G, color.B, color.A);

    public static double GetDistance(float x1, float y1, float z1, float x2 = 0, float y2 = 0, float z2 = 0) => sqrt(pow(x1 - x2) + pow(y1 - y2) + pow(z1 - z2));
}