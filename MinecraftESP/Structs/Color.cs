namespace ESP.Structs;
public struct Color
{
    public Color(byte r, byte g, byte b, byte a = byte.MaxValue)
    {
        R = (float)r / 256;
        G = (float)g / 256;
        B = (float)b / 256;
        A = (float)a / 256;
    }
    public Color(float r, float g, float b, float a = 1)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    public Color(double r, double g, double b, double a = 1)
    {
        R = (float)r;
        G = (float)g;
        B = (float)b;
        A = (float)a;
    }

    public float R = 0, G = 0, B = 0, A = 1;

    public static Color DistanceColor = new Color(1, 0, 0, 0);
    public static Color RGBColor = new Color(2, 0, 0, 0);

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        Color color = (Color)obj;

        return color.R == R && color.G == G && color.B == B && color.A == A;
    }
}