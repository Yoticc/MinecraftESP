﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Structs;

public record struct AABB(double MinX, double MinY, double MinZ, double MaxX, double MaxY, double MaxZ);