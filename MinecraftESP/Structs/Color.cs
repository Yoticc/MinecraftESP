using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Structs;
public struct Color
{
    public Color(sbyte r, sbyte g, sbyte b, sbyte a = sbyte.MaxValue) => Init<sbyte>(sbyte.MaxValue, r, g, b, a);
    public Color(byte r, byte g, byte b, byte a = byte.MaxValue) => Init<byte>(byte.MaxValue, r, g, b, a);
    public Color(short r, short g, short b, short a = short.MaxValue) => Init<short>(short.MaxValue, r, g, b, a);
    public Color(ushort r, ushort g, ushort b, ushort a = ushort.MaxValue) => Init<ushort>(ushort.MaxValue, r, g, b, a);
    public Color(int r, int g, int b, int a = int.MaxValue) => Init<int>(int.MaxValue, r, g, b, a);
    public Color(uint r, uint g, uint b, uint a = uint.MaxValue) => Init<uint>(uint.MaxValue, r, g, b, a);
    public Color(long r, long g, long b, long a = long.MaxValue) => Init<long>(long.MaxValue, r, g, b, a);
    public Color(ulong r, ulong g, ulong b, ulong a = ulong.MaxValue) => Init<ulong>(ulong.MaxValue, r, g, b, a);
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

    private void Init<T>(decimal max, decimal r, decimal g, decimal b, decimal a)
    {
        R = (float)((double)r / (long)max);
        G = (float)((double)g / (long)max);
        B = (float)((double)b / (long)max);
        A = (float)((double)a / (long)max);
    }

    public float R = 0, G = 0, B = 0, A = 1;
}