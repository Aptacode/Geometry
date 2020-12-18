using System;
using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Collision.Circles
{
    public static class BoundingCircleAlgorithm
    {
        public static BoundingCircle Welzl_Helper(Span<Vector2> points, Span<Vector2> boundarySet, int n,
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

            var d = Welzl_Helper(points, boundarySet, index, boundarySetIndex);

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

            return Welzl_Helper(points, boundarySet, index, boundarySetIndex);
        }

        public static BoundingCircle MinimumBoundingCircle(this Primitive p)
        {
            return p switch
            {
                Point point => new BoundingCircle(point.Position, 0.0f),
                Ellipse circle => new BoundingCircle(circle.Position, circle.Radius),
                Triangle triangle => BoundingCircle.FromThreePoints(triangle.P1, triangle.P2, triangle.P3),
                Rectangle rectangle => BoundingCircle.FromTwoPoints(rectangle.TopLeft, rectangle.BottomRight),
                _ => Welzl_Helper(new Span<Vector2>(p.Vertices.Vertices),
                    new Span<Vector2>(new[] {Vector2.Zero, Vector2.Zero, Vector2.Zero}), p.Vertices.Length, 0)
            };
        }
    }
}