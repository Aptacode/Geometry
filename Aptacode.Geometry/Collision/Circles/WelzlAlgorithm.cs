using System;
using System.Numerics;

namespace Aptacode.Geometry.Collision.Circles
{
    public static class WelzlAlgorithm
    {
        public static BoundingCircle Compute(Span<Vector2> points, Span<Vector2> boundarySet, int n,
            int boundarySetIndex)
        {
            if (n == 0 || boundarySetIndex == 2)
            {
                return boundarySetIndex switch
                {
                    0 => BoundingCircle.Zero,
                    1 => BoundingCircle.FromOnePoint(boundarySet[0]),
                    2 => BoundingCircle.FromTwoPoints(boundarySet[0], boundarySet[1]),
                    _ => BoundingCircle.FromThreePoints(boundarySet[0], boundarySet[1], boundarySet[2])
                };
            }

            var index = n - 1;
            var p = points[index];

            var d = Compute(points, boundarySet, index, boundarySetIndex);

            if (d.Contains(p))
            {
                return d;
            }

            if (boundarySetIndex == 3)
            {
                boundarySetIndex = 0;
            }

            boundarySet[boundarySetIndex] = p;
            boundarySetIndex++;

            return Compute(points, boundarySet, index, boundarySetIndex);
        }
    }
}