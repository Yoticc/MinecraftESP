using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Structs.Options;

public record struct Box(LBox L = default, PBox P = default);
public record struct LBox(bool Enabled, CAABB CAABB, float LineWidth);
public record struct PBox(bool Enabled, CAABB CAABB);