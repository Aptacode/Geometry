using System.Numerics;

namespace Aptacode.Geometry.Primitives.Extensions
{
    public static class PointExtensions
    {
        public static Point ToPoint(this Vector2 position)
        {
            return new(position);
        }
    }
}