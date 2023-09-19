namespace Core.Utils;
public class RenderUtils
{
    public static void Push()
    {
        GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

        GL.Enable(Cap.LineSmooth);
        GL.Disable(Cap.Texture2D);
        GL.Enable(Cap.CullFace);
        GL.Disable(Cap.DepthTest);
    }

    public static void Pop()
    {
        GL.Enable(Cap.DepthTest);
        GL.Enable(Cap.Texture2D);
        GL.Disable(Cap.LineSmooth);
    }

    public static void DrawOutlineAABB(AABB bb)
    {
        GL.Begin(Mode.Lines);

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
        GL.Begin(Mode.Quads);

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
        GL.Begin(Mode.Lines);
        GL.Vertex3f(fx, fy, fz);
        GL.Vertex3f(tx, ty, tz);
        GL.End();
    }

    public static void Color(Color color) => GL.Color4f(color.R, color.G, color.B, color.A);

    public static float GetDistance(float x1, float y1, float z1, float x2 = 0, float y2 = 0, float z2 = 0) => (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2) + Math.Pow(z1 - z2, 2));
}