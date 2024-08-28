namespace Core;
public class RenderUtils
{    
    public static void DrawOutlineAABB(AABB bb)
    {
        GL.Begin(Mode.Lines);

        GL.Vertex(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MinZ);

        GL.End();
    }

    public static void DrawSolidAABB(AABB bb)
    {
        GL.Begin(Mode.Quads);

        GL.Vertex(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MinZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MaxX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MaxX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MinZ);
        GL.Vertex(bb.MinX, bb.MinY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MaxZ);
        GL.Vertex(bb.MinX, bb.MaxY, bb.MinZ);

        GL.End();
    }

    public static void DrawTracer(float fx, float fy, float fz, float tx, float ty, float tz)
    {
        GL.Begin(Mode.Lines);
        GL.Vertex(fx, fy, fz);
        GL.Vertex(tx, ty, tz);
        GL.End();
    }

    public static void Color(Color color) => GL.Color(color.R, color.G, color.B, color.A);

    public static double GetDistance(float x1, float y1, float z1, float x2 = 0, float y2 = 0, float z2 = 0) => sqrt(pow(x1 - x2) + pow(y1 - y2) + pow(z1 - z2));
}