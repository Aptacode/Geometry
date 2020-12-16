using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class CoarseCollisionDetector : CollisionDetector
    {
        public static bool CoarseCollision(Primitive p1, Primitive p2)
        {
            var p1Center = p1.BoundingCircle.Center;
            var p1Radius = p1.BoundingCircle.Radius;
            var p2Center = p2.BoundingCircle.Center;
            var p2Radius = p2.BoundingCircle.Radius;

            return (p1Center - p2Center).Length() <= p1Radius + p2Radius;
        }

        #region Point

        public override bool CollidesWith(Point p1, Point p2) => CoarseCollision(p1, p2);

        public override bool CollidesWith(Point p1, PolyLine p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Point p1, Polygon p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Point p1, Circle p2) => CoarseCollision(p1, p2);

        #endregion

        #region PolyLine

        public override bool CollidesWith(PolyLine p1, Point p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PolyLine p1, PolyLine p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PolyLine p1, Polygon p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PolyLine p1, Circle p2) => CoarseCollision(p1, p2);

        #endregion

        #region Polygon

        public override bool CollidesWith(Polygon p1, Point p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Polygon p1, PolyLine p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Polygon p1, Polygon p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Polygon p1, Circle p2) => CoarseCollision(p1, p2);

        #endregion

        #region Circle

        public override bool CollidesWith(Circle p1, Point p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Circle p1, PolyLine p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Circle p1, Polygon p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Circle p1, Circle p2) => CoarseCollision(p1, p2);

        #endregion
    }
}