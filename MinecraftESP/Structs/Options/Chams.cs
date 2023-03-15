using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Structs.Options;
public record struct Chams(bool Enabled, bool Colored, Color Color, bool ThroughWall);