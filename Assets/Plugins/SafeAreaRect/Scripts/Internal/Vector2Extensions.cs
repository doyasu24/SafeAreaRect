using UnityEngine;

namespace SafeAreaRect.Internal
{
    public static class Vector2Extensions
    {
        public static bool IsFinite(this Vector2 v)
        {
            return v.x.IsFinite() && v.y.IsFinite();
        }

        private static bool IsFinite(this float f)
        {
            return !float.IsNaN(f) && !float.IsInfinity(f);
        }
    }
}