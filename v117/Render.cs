using Vertex = Core.Vertex;
using Vec3F = (float X, float Y, float Z);

namespace v117;
public unsafe class Render : DefaultRender
{
    record Line
    {
        public Vertex First;
        public Vertex Last;
        public float Length => round(pow(First.Position[0] - Last.Position[0]) + pow(First.Position[1] - Last.Position[1]) + pow(First.Position[2] - Last.Position[2]), 4);
    }

    Vertex* lastBuffer;
    Vec3F lastScaleF;

    public void DrawArrays(Mode mode, int first, int countVertices)
    {
        if (mode != Mode.Quads)
            return;

        // No buffer, like it do for blocks
        if (lastBuffer is null)
            return;

        // Most entities
        if (lastScaleF is not (F2D30, F2D30, F2D30))
            return;

        if (first != 0)
            return;

        if (countVertices % 4 != 0)
            return;

        GL.PushAttrib(0x000fffff);
        GL.PushMatrix();

        GL.Disable(Cap.Texture2D);
        GL.Disable(Cap.CullFace);
        GL.Disable(Cap.Lighting);
        GL.Disable(Cap.DepthTest);

        GL.Enable(Cap.LineSmooth);

        GL.Enable(Cap.Blend);
        GL.BlendFunc(Factor.SrcAlpha, Factor.OneMinusSrcAlpha);

        GL.LoadIdentity();

        GL.LineWidth(1);

        var countQuads = countVertices / 4;

        var quads = stackalloc Quad[countQuads];
        for (var i = 0; i < countQuads; i++)
            quads[i] = new Quad
            {
                StartVertexIndex = i * 4,
                VertexNode = lastBuffer
            };

        if (countVertices % 144 == 0) // Idk why not % 144
        {
            if (TexPredicates.Player.IsSuitable(quads, countQuads))
            {
                for (var vertexIndex = 0; vertexIndex < countVertices; vertexIndex += 4)
                {
                    var a = lastBuffer[vertexIndex].Position;
                    var b = lastBuffer[vertexIndex + 1].Position;
                    var c = lastBuffer[vertexIndex + 2].Position;
                    var d = lastBuffer[vertexIndex + 3].Position;

                    var line = new Line
                    {
                        First = lastBuffer[vertexIndex],
                        Last = lastBuffer[vertexIndex + 1]
                    };

                    // Remove additional layer
                    if (line.Length is .0421f or .0695f or .248f or .2481f or .2781f or .5364f or .5365f)
                        continue;

                    var quadIndex = vertexIndex / 4;
                    var quad = quads + quadIndex;

                    GL.Color4d(.1, .8, .7, .75);
                    DrawOutlined(quad);

                    GL.Color4d(.1, .7, .8, .05);
                    DrawQuad(quad);
                }
            }
        }
        else if (countVertices % 72 == 0)
        {
            if (TexPredicates.Chest.IsSuitable(quads, countQuads))
            {
                for (var quadIndex = 0; quadIndex < countQuads; quadIndex++)
                {
                    var quad = quads + quadIndex;

                    GL.Color4d(.8, .5, 0, .5);
                    DrawOutlined(quad);

                    GL.Color4d(.8, .5, 0, .09);
                    DrawQuad(quad);
                }
            }
        }

        GL.PopMatrix();
        GL.PopAttrib();

        return;

        void DrawOutlined(Quad* quad)
        {
            GL.Begin(Mode.LineLoop);
            Vertex(quad->A->Position);
            Vertex(quad->B->Position);
            Vertex(quad->C->Position);
            Vertex(quad->D->Position);
            Vertex(quad->A->Position);
            GL.End();
        }

        void DrawQuad(Quad* quad)
        {
            GL.Begin(Mode.Quads);
            Vertex(quad->A->Position);
            Vertex(quad->B->Position);
            Vertex(quad->C->Position);
            Vertex(quad->D->Position);
            GL.End();
        }

        void Vertex(float* arr) => GL.Vertex3f(arr[0], arr[1], arr[2]);
    }

    public void VertexPointer(int size, TexType type, int stride, pointer pointer)
    {
        if (size == 3 && type == TexType.Float && stride == 36 && pointer > 0x10000)
            lastBuffer = (Vertex*)pointer;
    }

    public void ScaleF(Vec3F vec) => lastScaleF = vec;
}