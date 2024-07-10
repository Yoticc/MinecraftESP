using System.Collections.Generic;
using static OpenGL.Enums;

namespace v115;
public unsafe class Render : DefaultRender
{
    List<(List<(float X, float Y, float Z)> Vertices, float[] Modelview, float[] Projection)> entities = [];

    struct Vertex
    {
        public fixed float Position[3];
        public fixed byte Color[4];
        public fixed float TexCoord1[2];
        public fixed short TexCoord2[2];
        public fixed short TexCoord3[2];
        public fixed byte Normal[4];
    }

    Vertex* lastPointer;
    public bool DrawArrays(Mode mode, int first, int count)
    {
        if (mode == Mode.Quads)
        {
            if (lastPointer is not null)
            {
                var mv = new float[16];
                var pt = new float[16];

                GL.GetFloatv(PName.ProjectionMatrix, pt);
                GL.GetFloatv(PName.ModelviewMatrix, mv);

                entities.Add((
                        Enumerable.Range(first, count - first).Select(i => (lastPointer[i].Position[0], lastPointer[i].Position[1], lastPointer[i].Position[2])).ToList(),
                        mv,
                        pt
                    ));
                /*
                GL.PushAttrib(0x000fffff);
                GL.PushMatrix();

                GL.Disable(Cap.Texture2D);
                GL.Disable(Cap.CullFace);
                GL.Disable(Cap.Lighting);
                GL.Disable(Cap.DepthTest);

                GL.Enable(Cap.LineSmooth);

                GL.Enable(Cap.Blend);
                GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

                GL.LineWidth(3);
                GL.Color3f(1, 0, 0);

                GL.LoadIdentity();

                GL.Enable(Cap.PolygonOffsetFill);
                GL.PolygonOffset(0, -1100000);

                GL.Begin(Mode.Quads);
                for (var vertexIndex = first; vertexIndex < count; vertexIndex++)
                {
                    var vertex = lastPointer[vertexIndex];
                    var pos = vertex.Position;
                    GL.Vertex3f(pos[0], pos[1], pos[2]);
                    Logger.WriteLine($"{pos[0]} {pos[1]} {pos[2]}");
                }
                GL.End();
                
                GL.PopMatrix();
                GL.PopAttrib();
                */
            }
        }

        return true;
    }

    public bool VertexPointer(int size, TexType type, int stride, pointer pointer)
    {
        if (size == 3 && type == TexType.Float && stride == 36 && pointer > 0x10000)
        {
            lastPointer = (Vertex*)pointer;
        }

        return true;
    }

    public new bool Ortho(double left, double right, double bottom, double top, double zNear, double zFar)
    {
        if (zNear != 1000 || zFar != 3000)
            return true;

        nowInInventory = true;

        GL.PushAttrib(0x000fffff);
        GL.PushMatrix();

        GL.Disable(Cap.Texture2D);
        GL.Disable(Cap.CullFace);
        GL.Disable(Cap.Lighting);
        GL.Disable(Cap.DepthTest);

        GL.Enable(Cap.LineSmooth);

        GL.Enable(Cap.Blend);
        GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

        foreach (var entity in entities)
        {
            if (entity.Vertices.Count < 6)
                continue;

            GL.PushMatrix();
            fixed (float* ptr = entity.Projection)
                GL.LoadMatrixf(Matrix.Projection, ptr);
            fixed (float* ptr = entity.Modelview)
                GL.LoadMatrixf(Matrix.Modelview, ptr);            

            GL.Begin(Mode.Quads);
            for (var vertexIndex = 0; vertexIndex < entity.Vertices.Count; vertexIndex++)
            {
                var pos = entity.Vertices[vertexIndex];
                GL.Vertex3f(pos.X, pos.Y, pos.Z);
            }
            GL.End();
            GL.PopMatrix();
        }
        entities.Clear();

        GL.PopAttrib();
        GL.PopMatrix();

        return true;

        void Draw(bool enabled, params TargetOpt[] targetOpts)
        {
            if (enabled)
                foreach (var targetOpt in targetOpts)
                    foreach (var target in targetOpt.Targets)
                        target.DrawOver(targetOpt);
        }
    }
}