using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class HybridCollisionDetector : CollisionDetector
    {
        public static readonly CoarseCollisionDetector CoarseCollisionDetector = new();
        public static readonly FineCollisionDetector FineCollisionDetector = new();

        #region Point

        public override bool CollidesWith(Point p1, Point p2) => p1 == p2;

        public override bool CollidesWith(Point p1, PolyLine p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                    FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Point p1, Polygon p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                   FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Point p1, Circle p2) => p2.BoundingCircle.Contains(p1.Position);

        #endregion

        #region PolyLine

        public override bool CollidesWith(PolyLine p1, Point p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                    FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PolyLine p1, PolyLine p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                       FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PolyLine p1, Polygon p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                      FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PolyLine p1, Circle p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                     FineCollisionDetector.CollidesWith(p1, p2);

        #endregion

        #region Polygon

        public override bool CollidesWith(Polygon p1, Point p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                   FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Polygon p1, PolyLine p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                      FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Polygon p1, Polygon p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                     FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Polygon p1, Circle p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                    FineCollisionDetector.CollidesWith(p1, p2);

        #endregion

        #region Circle

        public override bool CollidesWith(Circle p1, Point p2) => p1.BoundingCircle.Contains(p2.Position);

        public override bool CollidesWith(Circle p1, PolyLine p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                     FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Circle p1, Polygon p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                    FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Circle p1, Circle p2)
        {
            var d = (p2.Position - p1.Position).Length();
            return d < p1.Radius + p2.Radius;
        }

        #endregion
    }
}