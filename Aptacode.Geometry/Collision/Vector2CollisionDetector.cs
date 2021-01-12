using System;
using System.Linq;
using System.Numerics;
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
            //Todo
            return false;
        }
        
        public static bool Contains(this Ellipse p2, Vector2 p1)
        {
            //Todo
            return false;
        }

        #endregion
    }
}