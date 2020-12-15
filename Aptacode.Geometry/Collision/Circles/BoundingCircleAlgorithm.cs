using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Collision.Circles
{
    public static class BoundingCircleAlgorithm
    {
        public static BoundingCircle Welzl_Helper(IEnumerable<Vector2> points, List<Vector2> boundarySet, int n)
        {
            if (n == 0 || boundarySet.Count == 3)
            {
                return boundarySet.Count switch
                {
                    0 => BoundingCircle.Zero,
                    1 => BoundingCircle.FromOnePoint(boundarySet[0]),
                    2 => BoundingCircle.FromTwoPoints(boundarySet[0], boundarySet[1]),
                    _ => BoundingCircle.FromThreePoints(boundarySet[0], boundarySet[1], boundarySet[2])
                };
            }

            var index = n - 1;
            var p = points.ElementAt(index);

            var d = Welzl_Helper(points, boundarySet, index);

            if (d.Contains(p))
            {
                return d;
            }

            if (boundarySet.Count < 3)
            {
                boundarySet.Add(p);
            }
            else
            {
                boundarySet = new List<Vector2> {p};
            }

            return Welzl_Helper(points, boundarySet, index);
        }

        public static BoundingCircle MinimumBoundingCircle(this Primitive p)
        {
            return p switch
            {
                Point point => new BoundingCircle(point.Position, 0.0f),
                Circle circle => new BoundingCircle(circle.Position, circle.Radius),
                Triangle triangle => BoundingCircle.FromThreePoints(triangle.P1, triangle.P2, triangle.P3),
                Rectangle rectangle => BoundingCircle.FromTwoPoints(rectangle.TopLeft, rectangle.BottomRight),
                _ => Welzl_Helper(p.Vertices, new List<Vector2>(), p.Vertices.Count())
            };
        }
    }
}