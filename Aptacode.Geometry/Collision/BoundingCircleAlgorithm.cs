using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class BoundingCircleAlgorithm
    {
        public static Circle MinimumEnclosingCircleTrivial(List<Point> points)
        {
            if (points.Count == 0)
            {
                return new Circle(new Vector2(0, 0), 0);
            }

            if (points.Count == 1)
            {
                return new Circle(points[0].Position, 0);
            }

            if (points.Count == 2)
            {
                return CreateCircleFromTwoPoints(points[0].Position, points[1].Position);
            }

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var c = CreateCircleFromTwoPoints(points[i].Position, points[j].Position);
                    var pointsOutside = points.Where(p => IsInside(c, p.Position) == false);
                    if (!pointsOutside.Any())
                    {
                        return c;
                    }
                }
            }

            return CreateCircleFromThreePoints(points[0].Position, points[1].Position, points[2].Position);
        }

        public static Circle Welzl_Helper(List<Point> points, List<Point> boundarySet, int n)
        {
            if (n == 0 || boundarySet.Count == 3)
            {
                return MinimumEnclosingCircleTrivial(boundarySet);
            }

            //Random rng = new Random();
            //var pindex = points.Count - 1;
            var p = points[n - 1];
            //points.Remove(p);

            var d = Welzl_Helper(points.Take(n - 1).ToList(), boundarySet, n - 1);

            if (IsInside(d, p.Position))
            {
                return d;
            }

            if (boundarySet.Count < 3)
            {
                boundarySet.Add(p);
            }
            else
            {
                boundarySet = new List<Point> {p};
            }

            return Welzl_Helper(points, boundarySet, n - 1);
        }

        public static Circle MinimumBoundingCircle(Primitive p)
        {
            var verticesAsPoints = new List<Point>();
            foreach (var vertex in p.Vertices)
            {
                verticesAsPoints.Add(new Point(vertex));
            }

            //Todo Add in randomisation, not very important right now
            return Welzl_Helper(verticesAsPoints, new List<Point>(), verticesAsPoints.Count);
        }

        public static bool IsInside(Circle circle, Vector2 point) =>
            (point - circle.Position).Length() <= circle.Radius;

        public static Circle CreateCircleFromTwoPoints(Vector2 p1, Vector2 p2)
        {
            var midpoint = p1 + (p2 - p1) / 2;
            var position = new Vector2(midpoint.X, midpoint.Y);
            var radius = (p2 - p1).Length() / 2;

            return new Circle(position, radius);
        }

        public static Circle CreateCircleFromThreePoints(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            var a = p3.LengthSquared() - p2.LengthSquared();
            var b = p1.LengthSquared() - p3.LengthSquared();
            var c = p2.LengthSquared() - p1.LengthSquared();
            var d = (p3.X - p1.X) * (p1.Y - p2.Y) -
                    (p2.X - p1.X) *
                    (p1.Y - p3.Y); //This is zero if the 3 points are colinear, might need some exception handling


            var cx = (a * p1.Y + b * p2.Y + c * p3.Y) / (2 * d);
            var cy = (a * p1.X + b * p2.X + c * p3.X) / (-2 * d);

            var position = new Vector2(cx, cy);
            var radius = (p1 - position).Length();

            return new Circle(position, radius);
        }
    }
}