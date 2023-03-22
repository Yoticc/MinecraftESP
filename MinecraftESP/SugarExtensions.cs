using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP;
public static class SugarExtensions
{
    public static bool IsBetween(this float val, float min, float max) => min < val && max > val;
}