using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class CollisionDetector : ICollisionDetector
    {
        #region Point

        public bool CollidesWith(Point p1, Point p2) => p1 == p2;

        public bool CollidesWith(Point p1, PolyLine p2)
        {
            foreach (var pair in p2.LineSegments())
            {
                var q = pair.p2 - pair.p1;
                var r = p1.Position - pair.p1;
                var s = q / r;
                if (s.X >= 0 && s.X <= 1 && s.Y >= 0 && s.Y <= 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CollidesWith(Point p1, Polygon p2)
        {
            var collision = false;
            var edges = p2.Edges();
            var point = p1.Position;
            foreach (var (a, b) in edges)
            {
                if ((a.Y >= point.Y && b.Y < point.Y || a.Y < point.Y && b.Y >= point.Y) &&
                    point.X < (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
                {
                    collision = !collision;
                }
            }


            return collision;
        }

        public bool CollidesWith(Point p1, Circle p2) => false;

        #endregion

        #region PolyLine

        public bool CollidesWith(PolyLine p1, Point p2) => false;

        public bool CollidesWith(PolyLine p1, PolyLine p2) => false;

        public bool CollidesWith(PolyLine p1, Polygon p2) => false;

        public bool CollidesWith(PolyLine p1, Circle p2) => false;

        #endregion

        #region Polygon

        public bool CollidesWith(Polygon p1, Point p2) => false;

        public bool CollidesWith(Polygon p1, PolyLine p2) => false;

        public bool CollidesWith(Polygon p1, Polygon p2) => false;

        public bool CollidesWith(Polygon p1, Circle p2) => false;

        #endregion

        #region Circle

        public bool CollidesWith(Circle p1, Point p2) => false;

        public bool CollidesWith(Circle p1, PolyLine p2) => false;

        public bool CollidesWith(Circle p1, Polygon p2) => false;

        public bool CollidesWith(Circle p1, Circle p2) => false;

        #endregion
    }
}