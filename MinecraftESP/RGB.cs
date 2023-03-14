using ESP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ESP;
public static class RGB
{
    private const int COUNT_PHASE = 3;

    static RGB()
    {
        Speed = 2000;
    }

    [DllImport("kernel32")]
    private static extern uint GetTickCount();

    public static int Speed { get => speed; set {
            speed = value;
            ticksPerPhase = speed / COUNT_PHASE;
        } }

    private static int speed;
    private static int ticksPerPhase;
    public static (float r, float g, float b) GetF()
    {
        int ticks = (int)GetTickCount() % Speed;
        int phase = ticks / ticksPerPhase;
        int dest = ticks % ticksPerPhase;
        int rest = ticksPerPhase - dest;

        switch (phase)
        {
            case 0:
                return ((float)dest / ticksPerPhase, (float)rest / ticksPerPhase, 0);
            case 1:
                return ((float)rest / ticksPerPhase, 0, (float)dest / ticksPerPhase);
            case 2:
                return (0, (float)dest / ticksPerPhase, (float)rest / ticksPerPhase);
            default:
                return (0, 0, 0);
        }
    }
}