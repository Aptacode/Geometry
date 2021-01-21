using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public static class Vector2CollisionDetector
    {
        #region Vector2

        public static bool CollidesWith(this Point p2, Vector2 p1)
        {
            return Math.Abs(p1.X - p2.Position.X) <= Constants.Tolerance && Math.Abs(p1.Y - p2.Position.Y) <= Constants.Tolerance;
        }

        public static bool CollidesWith(this PolyLine p2, Vector2 p1)
        {
            for (var i = 0; i < p2.LineSegments.Length; i++)
            {
                if (p2.LineSegments[i].OnLineSegment(p1))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CollidesWith(this Polygon p2, Vector2 p1)
        {
            var collision = false;
            var edges = p2.Edges;
            var point = p1;

            foreach (var (a, b) in edges)
            {
                if ((a, b).OnLineSegment(point))
                {
                    return true;
                }

                if ((a.Y >= point.Y && b.Y <= point.Y ||
                     a.Y <= point.Y && b.Y >= point.Y) &&
                    point.X <= (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
                {
                    collision = !collision;
                }
            }

            return collision;
        }

        public static bool CollidesWith(this Ellipse p2, Vector2 p1)
        {
            var f1dist = (p2.Foci.Item1 - p1).Length();
            var f2dist = (p2.Foci.Item2 - p1).Length();
            if (p2.Radii.X > p2.Radii.Y) //X is the major axis
            {
                return f1dist + f2dist <= 2 * p2.Radii.X;
            }

            if (p2.Radii.Y > p2.Radii.X) //Y is the major axis
            {
                return f1dist + f2dist <= 2 * p2.Radii.Y;
            }

            return p2.BoundingCircle.Contains(p1);
        }


        public static bool CollidesWith(this BoundingRectangle p2, Vector2 p1)
        {
            return p2.TopLeft.X <= p1.X &&
                   p2.TopLeft.Y <= p1.Y &&
                   p2.BottomRight.X >= p1.X &&
                   p2.BottomRight.Y >= p1.Y;
        }
        #endregion
    }
}