namespace Core;

public record struct Tracer(bool Enabled, Color Color, float LineWidth, float OffsetX = 0, float OffsetY = 0, float OffsetZ = 0)
{
    public static implicit operator Tracer((bool enabled, Color color, float lineWidth, float offsetX, float offsetY, float offsetZ) a) => new(a.enabled, a.color, a.lineWidth, a.offsetX, a.offsetY, a.offsetZ);
}
public record struct Box(LBox L = default, PBox P = default)
{
    public static implicit operator Box((LBox l, PBox p) a) => new(a.l, a.p);
    public static implicit operator Box(LBox l) => new(l);
}
public record struct LBox(bool Enabled, CAABB CAABB, float LineWidth) 
{
    public static implicit operator LBox((bool enalbed, CAABB caabb, float lineWidth) a) => new(a.enalbed, a.caabb, a.lineWidth);
}
public record struct PBox(bool Enabled, CAABB CAABB)
{
    public static implicit operator PBox((bool enalbed, CAABB caabb) a) => new(a.enalbed, a.caabb);
}
public record struct CAABB(Color Color, AABB AABB)
{
    public static implicit operator CAABB((Color color, AABB aabb) a) => new(a.color, a.aabb);
}
public record struct AABB(double MinX, double MinY, double MinZ, double MaxX, double MaxY, double MaxZ)
{
    public static implicit operator AABB((double minX, double minY, double minZ, double maxX, double maxY, double maxZ) a) => new(a.minX, a.minY, a.minZ, a.maxX, a.maxY, a.maxZ);
}

public record struct Color(float R, float G, float B, float A = 1)
{
    public Color(byte r, byte g, byte b, byte a = byte.MaxValue) : this((float)r / byte.MaxValue, (float)g / byte.MaxValue, (float)b / byte.MaxValue, (float)a / byte.MaxValue) { }
    public Color(double r, double g, double b, double a = 1) : this((float)r, (float)g, (float)b, (float)a) { }

    public static Color DistanceColor = new(1, 0, 0, 0), RGBColor = new(2, 0, 0, 0);

    public static implicit operator Color((float r, float g, float b) a) => new(a.r, a.g, a.b);
    public static implicit operator Color((float r, float g, float b, float a) a) => new(a.r, a.g, a.b, a.a);

    public static implicit operator Color((double r, double g, double b) a) => new(a.r, a.g, a.b);
    public static implicit operator Color((double r, double g, double b, double a) a) => new(a.r, a.g, a.b, a.a);
}

public record TargetOpt(bool Enabled, Box Box, Tracer Tracer = default)
{
    public List<GLTarget> Targets = [];

    public static implicit operator TargetOpt((bool enalbed, Box box) a) => new(a.enalbed, a.box);
    public static implicit operator TargetOpt((bool enalbed, Box box, Tracer tracer) a) => new(a.enalbed, a.box, a.tracer);
}