﻿namespace Core.Utils;
public static class ColorUtils
{
    const int COUNT_PHASE = 3;

    static ColorUtils() => Speed = 2000;

    public static int Speed
    {
        get => speed; set
        {
            speed = value;
            ticksPerPhase = speed / COUNT_PHASE;
        }
    }

    static int speed, ticksPerPhase;
    public static Color GetRGB()
    {
        int ticks = kernel32.GetTickCount() % Speed;
        int phase = ticks / ticksPerPhase;
        int dest = ticks % ticksPerPhase;
        int rest = ticksPerPhase - dest;

        switch (phase)
        {
            case 0:
                return new((float)dest / ticksPerPhase, (float)rest / ticksPerPhase, 0);
            case 1:
                return new((float)rest / ticksPerPhase, 0, (float)dest / ticksPerPhase);
            case 2:
                return new(0, (float)dest / ticksPerPhase, (float)rest / ticksPerPhase);
            default:
                return new(0, 0, 0);
        }
    }

    public static Color GetDistColor(float max, float value)
    {
        float perc = value / max;
        float a = Math.Clamp((float)(1 - value / (max * (1 / 0.5))), .7f, 1);
        float r = Math.Clamp(perc <= .5f ? 1 : 1 - (perc - .5f) * 2, 0, 1),
              g = Math.Clamp(perc <= .5f ? perc * 2 : 1, 0, 1);

        return new(r, g, 0, a);
    }
}