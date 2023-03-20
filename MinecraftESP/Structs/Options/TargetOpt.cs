using ESP.Structs.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Structs.Options;
public class TargetOpt
{
    public TargetOpt(bool enabled, Box box, Tracer tracer = default, Chams chams = default)
    {
        Enabled = enabled;
        Box = box;
        Tracer = tracer;
        Chams = chams;

        for (int i = 0; i < 256; i++)
            Targets[i] = new GLTarget();
    }

    public GLTarget[] Targets { get; private set; } = new GLTarget[256];

    public bool Enabled;
    public Box Box;
    public Tracer Tracer;
    public Chams Chams;
    public int Index = 0;

    public void DrawDuring(GLTarget target) => target.DrawDuring(this);
}