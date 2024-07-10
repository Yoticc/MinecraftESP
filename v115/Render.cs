using Memory;
using Vec3F = (float X, float Y, float Z);

namespace v115;
public unsafe class Render : DefaultRender
{
    struct Vertex
    {
        public fixed float Position[3];
        public fixed byte Color[4];
        public fixed float TexCoord1[2];
        public fixed short TexCoord2[2];
        public fixed short TexCoord3[2];
        public fixed byte Normal[4];
    }

    struct Line
    {
        public Vertex First;
        public Vertex Last;
        public float Length => round(pow(First.Position[0] - Last.Position[0]) + pow(First.Position[1] - Last.Position[1]) + pow(First.Position[2] - Last.Position[2]), 4);
    }

    Vertex* lastPointer;
    Vec3F lastScaleF;

    public void DrawArrays(Mode mode, int first, int count)
    {
        if (mode != Mode.Quads)
            return;

        if (lastPointer is null)
            return;

        if (lastScaleF is (F2D30, F2D30, F2D30) && first == 0 && count == 288)
        {
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
            GL.Color3ub(255, 0, 0);

            Logger.WriteLine($"Render: {string.Join('\n', MemEx.Read<Vertex>(lastPointer, count).Select(v => $"{Math.Round(v.Position[0], 5)} {Math.Round(v.Position[1], 5)} {Math.Round(v.Position[2], 5)}"))}");

            for (var vertexIndex = first; vertexIndex < count; vertexIndex += 4)
            {
                var a = lastPointer[vertexIndex].Position;
                var b = lastPointer[vertexIndex + 1].Position;
                var c = lastPointer[vertexIndex + 2].Position;
                var d = lastPointer[vertexIndex + 3].Position;

                var line = new Line()
                {
                    First = lastPointer[vertexIndex],
                    Last = lastPointer[vertexIndex + 1]
                };

                if (line.Length is .0421f or .0695f or .248f or .2481f or .5364f or .5365f)
                    continue;

                GL.Begin(Mode.LineLoop);
                Vertex(a);
                Vertex(b);
                Vertex(c);
                Vertex(d);
                Vertex(a);
                GL.End();
            }

            void Vertex(float* arr) => GL.Vertex3f(arr[0], arr[1], arr[2]);

            GL.PopMatrix();
            GL.PopAttrib();
        }

        return;
    }

    public void VertexPointer(int size, TexType type, int stride, pointer pointer)
    {
        if (size == 3 && type == TexType.Float && stride == 36 && pointer > 0x10000)
            lastPointer = (Vertex*)pointer;
    }

    public void ScaleF(Vec3F vec)
    {
        lastScaleF = vec;
    }
}