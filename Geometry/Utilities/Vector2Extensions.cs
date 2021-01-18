using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Aptacode.Geometry.Utilities
{
    public static class Vector2Extensions
    {
        public static float VectorCross(this Vector2 a, Vector2 b)
        {
            return (a.X * b.Y) - (a.Y * b.X);
        }

        public static Vector2 Perp(this Vector2 a)
        {
            return Vector2.Transform(a, new Matrix3x2(0, -1, 1, 0, 0, 0));
        }

        public static float PerpDot(this Vector2 a, Vector2 b) //This is worse than the VectorCross method performance wise for the same result.
        {
            return Vector2.Dot(a.Perp(), b);
        }
    }
}
