using System;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public static class Vector2CollisionDetector
    {
        #region Vector2

        public static bool CollidesWith(Vector2 p1, Primitive p2)
        {
            return p2 switch
            {
                Point p => CollidesWith(p1, p),
                PolyLine p => CollidesWith(p1, p),
                Polygon p => CollidesWith(p1, p),
                Ellipse p => CollidesWith(p1, p),
                _ => false
            };
        }

        public static bool Contains(this Point p2, Vector2 p1)
        {
            return (Math.Abs(p1.X - p2.Position.X) <= Constants.Tolerance && Math.Abs(p1.Y - p2.Position.Y) <= Constants.Tolerance);

        }
        
        public static bool Contains(this PolyLine p2, Vector2 p1 )
        {
            return p2.LineSegments.Any(l => l.OnLineSegment(p1));
        }

        public static bool Contains(this Polygon p2, Vector2 p1 )
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
        
        public static bool Contains(this Ellipse p2, Vector2 p1)
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

        #endregion
    }
}