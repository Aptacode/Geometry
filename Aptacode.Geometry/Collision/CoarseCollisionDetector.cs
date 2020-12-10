using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class CoarseCollisionDetector : ICollisionDetector
    {
        public static bool CoarseCollision(Primitive p1, Primitive p2)
        {
            var p1Center = p1.GetCenter();
            var p1Radius = p1.GetRadius();
            var p2Center = p2.GetCenter();
            var p2Radius = p2.GetRadius();

            return (p1Center - p2Center).Length() <= p1Radius + p2Radius;
        }

        #region Point

        public bool CollidesWith(Point p1, Point p2) => p1 == p2;
        public bool CollidesWith(Point p1, PolyLine p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(Point p1, Polygon p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(Point p1, Circle p2) => CoarseCollision(p1, p2);

        #endregion

        #region PolyLine

        public bool CollidesWith(PolyLine p1, Point p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(PolyLine p1, PolyLine p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(PolyLine p1, Polygon p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(PolyLine p1, Circle p2) => CoarseCollision(p1, p2);

        #endregion

        #region Polygon

        public bool CollidesWith(Polygon p1, Point p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(Polygon p1, PolyLine p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(Polygon p1, Polygon p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(Polygon p1, Circle p2) => CoarseCollision(p1, p2);

        #endregion

        #region Circle

        public bool CollidesWith(Circle p1, Point p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(Circle p1, PolyLine p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(Circle p1, Polygon p2) => CoarseCollision(p1, p2);
        public bool CollidesWith(Circle p1, Circle p2) => CoarseCollision(p1, p2);

        #endregion
    }
}