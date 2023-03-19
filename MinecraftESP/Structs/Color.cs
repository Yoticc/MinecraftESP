using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public override string ToString() => $"{R} {G} {B} {A}";
}