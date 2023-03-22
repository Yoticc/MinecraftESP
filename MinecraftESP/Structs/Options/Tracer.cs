using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Structs.Options;
public record struct Tracer(bool Enabled, Color Color, float LineWidth, float OffsetX = 0, float OffsetY = 0, float OffsetZ = 0);