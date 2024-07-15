namespace Core;
public static class ColorUtils
{
    static ColorUtils() => RGBSpeed = 2000;

    public static int RGBSpeed { get => speed; set => ticksPerPhase = (speed = value) / 3; }

    static int speed, ticksPerPhase;
    public static Color GetRGB()
    {
        int ticks = kernel32.GetTickCount() % RGBSpeed;
        int phase = ticks / ticksPerPhase;
        int dest = ticks % ticksPerPhase;
        int rest = ticksPerPhase - dest;

        return phase switch
        {
            0 => new((double)dest / ticksPerPhase, (double)rest / ticksPerPhase, 0),
            1 => new((double)rest / ticksPerPhase, 0, (double)dest / ticksPerPhase),
            2 => new(0, (double)dest / ticksPerPhase, (double)rest / ticksPerPhase),
            _ => new(0, 0, 0)
        };
    }

    public static Color GetDistColor(double max, double value) => new(
        clamp((value / max) <= .5 ? 1 : 1 - ((value / max) - .5) * 2, 0, 1),
        clamp((value / max) <= .5 ? (value / max) * 2 : 1, 0, 1),
        0,
        clamp((double)(1 - value / (max * (1 / 0.5))), .7, 1)
    );
}