using Vertex = Core.Vertex;

namespace v117;
public unsafe class Render : AbstractRender
{
    Vertex* lastBuffer;
    int bufferSize;

    public void DrawElements(ref Mode mode, int count, BUType type, pointer indicies)
    {
        if (count > 10)
            mode = Mode.LineLoop;

        if (lastBuffer is not null && mode == Mode.Triangles && bufferSize >= count && count % 3 == 0 && count == 432)
        {
            /* Creates lags 
            Push();
            GL.LoadIdentity();
            GL.LineWidth(1);

            GL.Begin(Mode.Triangles);
            for (var vertexIndex = 0; vertexIndex < count; vertexIndex += 3)
            {
                var a = lastBuffer[vertexIndex].Position;
                var b = lastBuffer[vertexIndex + 1].Position;
                var c = lastBuffer[vertexIndex + 2].Position;

                GL.Vertex3f(a[0], a[1], a[2]);
                GL.Vertex3f(b[0], b[1], b[2]);
                GL.Vertex3f(c[0], c[1], c[2]);
            }
            GL.End();

            Pop();
            */

            lastBuffer = null;
        }
    }

    public void BufferData(BufferType type, int size, pointer data, BufferUsage usage)
    {
        if (type == BufferType.ElementArray && usage == BufferUsage.DynamicDraw)
        {
            lastBuffer = (Vertex*)data;
            bufferSize = size;
        }
    }
}