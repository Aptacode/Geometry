using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class FineCollisionDetector : CollisionDetector
    {
        #region Point

        public override bool CollidesWith(Point p1, Point p2) => p1 == p2;

        public override bool CollidesWith(Point p1, PolyLine p2)
        {
            foreach (var (vector2, vector3) in p2.LineSegments())
            {
                var q = vector3 - vector2;
                var r = p1.Position - vector2;
                var s = q / r;
                if (s.X >= 0 && s.X <= 1 && s.Y >= 0 && s.Y <= 1)
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CollidesWith(Point p1, Polygon p2)
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

        public override bool CollidesWith(Point p1, Circle p2) => false;

        #endregion

        #region PolyLine

        public override bool CollidesWith(PolyLine p1, Point p2) => false;

        public override bool CollidesWith(PolyLine p1, PolyLine p2) => false;

        public override bool CollidesWith(PolyLine p1, Polygon p2) => false;

        public override bool CollidesWith(PolyLine p1, Circle p2) => false;

        #endregion

        #region Polygon

        public override bool CollidesWith(Polygon p1, Point p2) => false;

        public override bool CollidesWith(Polygon p1, PolyLine p2) => false;

        public override bool CollidesWith(Polygon p1, Polygon p2) => false;

        public override bool CollidesWith(Polygon p1, Circle p2) => false;

        #endregion

        #region Circle

        public override bool CollidesWith(Circle p1, Point p2) => false;

        public override bool CollidesWith(Circle p1, PolyLine p2) => false;

        public override bool CollidesWith(Circle p1, Polygon p2) => false;

        public override bool CollidesWith(Circle p1, Circle p2) => false;

        #endregion
    }
}