namespace Core;
public unsafe struct Quad
{
    public Vertex* VertexNode;
    public int StartVertexIndex;

    public Vertex* A => VertexNode + StartVertexIndex;
    public Vertex* B => VertexNode + StartVertexIndex + 1;
    public Vertex* C => VertexNode + StartVertexIndex + 2;
    public Vertex* D => VertexNode + StartVertexIndex + 3;
}