using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Structs;
public struct AABB
{
    public AABB(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
    {
        MinX = minX;
        MinY = minY;
        MinZ = minZ;
        MaxX = maxX;
        MaxY = maxY;
        MaxZ = maxZ;
    }

    public double MinX, MinY, MinZ, MaxX, MaxY, MaxZ;
}