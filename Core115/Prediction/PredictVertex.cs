namespace Core;
public unsafe record struct PredictVertex(float X, float Y)
{
    public bool IsSuitable(Vertex* vertex) => vertex->TexCoord1[0] == X && vertex->TexCoord1[1] == Y;

    public static implicit operator PredictVertex((float X, float Y) args) => new(args.X, args.Y);
}