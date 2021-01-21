using System.Numerics;

namespace Aptacode.Geometry.Utilities
{
    public static class Vector2Extensions
    {
        private static readonly Matrix3x2 _rotationMatrix = new Matrix3x2(0, -1, 1, 0, 0, 0);

        public static float VectorCross(this Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static Vector2 Perp(this Vector2 a)
        {
            return Vector2.Transform(a, _rotationMatrix);
        }

        public static float PerpDot(this Vector2 a, Vector2 b) //This is worse than the VectorCross method performance wise for the same result.
        {
            return Vector2.Dot(a.Perp(), b);
        }
    }
}