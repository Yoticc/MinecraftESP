namespace Core;
public static class SugarExtensions
{
    public static bool IsBetween(this float val, float min, float max) => min < val && max > val;
}