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
    }

    public List<GLTarget> Targets { get; private set; } = new List<GLTarget>();

    public bool Enabled;
    public Box Box;
    public Tracer Tracer;
    public Chams Chams;

    public void Add(GLTarget target)
    {
        Targets.Add(target);
        target.DrawDuring(this);
    }
}