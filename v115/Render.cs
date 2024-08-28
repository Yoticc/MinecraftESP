using Vertex = Core.Vertex;
using Vec3F = (float X, float Y, float Z);

namespace v115;
public unsafe class Render : AbstractRender
{
    record struct Line
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

        var countQuads = countVertices / 4;
        var quads = (Quad*)lastBuffer;
        
        if (countVertices % 144 == 0 && TexPredicates.Player.IsSuitable(quads, countQuads)) // Can be used 288
        {
            Push();
            GL.LoadIdentity();
            GL.LineWidth(1);

            for (var vertexIndex = 0; vertexIndex < countVertices; vertexIndex += 4)
            {
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

                GL.Color(.1, .8, .7, .75);
                DrawOutlined(quad);

                GL.Color(.1, .7, .8, .05);
                DrawQuad(quad);
            }

            Pop();
        }

        if (countVertices % 72 == 0 && TexPredicates.Chest.IsSuitable(quads, countQuads))
        {
            Push();
            GL.LoadIdentity();
            GL.LineWidth(1);

            for (var quadIndex = 0; quadIndex < countQuads; quadIndex++)
            {
                var quad = quads + quadIndex;

                GL.Color(.8, .5, 0, .5);
                DrawOutlined(quad);

                GL.Color(.8, .5, 0, .09);
                DrawQuad(quad);
            }

            Pop();
        }        

        void DrawOutlined(Quad* quad)
        {
            GL.Begin(Mode.LineLoop);
            Vertex(quad->A, quad->B, quad->C, quad->D, quad->A);
            GL.End();
        }

        void DrawQuad(Quad* quad)
        {
            GL.Begin(Mode.Quads);
            Vertex(quad->A, quad->B, quad->C, quad->D);
            GL.End();
        }

        void Vertex(params Vertex[] vertices)
        {
            foreach (var vertex in vertices)
                GL.Vertex3(vertex.Position);
        }
    }

    public void VertexPointer(int size, TexType type, int stride, pointer pointer)
    {
        if (size == 3 && type == TexType.Float && stride == 36 && pointer > 0x10000)
            lastBuffer = (Vertex*)pointer;
    }

    public void ScaleF(Vec3F vec) => lastScaleF = vec;
}