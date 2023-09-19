namespace Core;

public record struct Tracer(bool Enabled, Color Color, float LineWidth, float OffsetX = 0, float OffsetY = 0, float OffsetZ = 0);
public record struct Box(LBox L = default, PBox P = default);
public record struct LBox(bool Enabled, CAABB CAABB, float LineWidth);
public record struct PBox(bool Enabled, CAABB CAABB);
public record struct CAABB(Color Color, AABB AABB);
public record struct AABB(double MinX, double MinY, double MinZ, double MaxX, double MaxY, double MaxZ);

public record struct Color(float R, float G, float B, float A = 1)
{
    public Color(byte r, byte g, byte b, byte a = byte.MaxValue) : this((float)r / byte.MaxValue, (float)g / byte.MaxValue, (float)b / byte.MaxValue, (float)a / byte.MaxValue) { }
    public Color(double r, double g, double b, double a = 1) : this((float)r, (float)g, (float)b, (float)a) { }

    public static Color DistanceColor = new(1, 0, 0, 0), RGBColor = new(2, 0, 0, 0);
}

public record TargetOpt(bool Enabled, Box Box, Tracer Tracer = default)
{
    public List<GLTarget> Targets { get; set; } = new List<GLTarget>();
}