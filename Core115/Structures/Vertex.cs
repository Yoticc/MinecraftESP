namespace Core;
public unsafe struct Vertex
{
    public fixed float Position[3];
    public fixed byte Color[4];
    public fixed float TexCoord1[2];
    public fixed short TexCoord2[2];
    public fixed short TexCoord3[2];
    public fixed byte Normal[4];
}