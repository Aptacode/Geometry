using Aptacode.Geometry.Composites;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class BoundingRectangleCollisionDetector : CollisionDetector
    {
        public static bool CoarseCollision(Primitive p1, Primitive p2)
        {
            var l1 = p1.BoundingRectangle.TopLeft;
            var r1 = p1.BoundingRectangle.BottomRight;
            var l2 = p2.BoundingRectangle.TopLeft;
            var r2 = p2.BoundingRectangle.BottomRight;

            // If one rectangle is on left side of other 
            if (l1.X > r2.X || l2.X > r1.X)
            {
                return false;
            }

            // If one rectangle is above other 
            if (l1.Y > r2.Y || l2.Y > r1.Y)
            {
                return false;
            }

            return true;
        }

        #region Point

        public override bool CollidesWith(Point p1, Point p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Point p1, PolyLine p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Point p1, Polygon p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Point p1, Ellipse p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Point p1, PrimitiveGroup p2) => CoarseCollision(p1, p2);

        #endregion

        #region PolyLine

        public override bool CollidesWith(PolyLine p1, Point p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PolyLine p1, PolyLine p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PolyLine p1, Polygon p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PolyLine p1, Ellipse p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PolyLine p1, PrimitiveGroup p2) => CoarseCollision(p1, p2);

        #endregion

        #region Polygon

        public override bool CollidesWith(Polygon p1, Point p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Polygon p1, PolyLine p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Polygon p1, Polygon p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Polygon p1, Ellipse p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Polygon p1, PrimitiveGroup p2) => CoarseCollision(p1, p2);

        #endregion

        #region Circle

        public override bool CollidesWith(Ellipse p1, Point p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Ellipse p1, PolyLine p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Ellipse p1, Polygon p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Ellipse p1, Ellipse p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(Ellipse p1, PrimitiveGroup p2) => CoarseCollision(p1, p2);

        #endregion

        #region PrimitiveGroup

        public override bool CollidesWith(PrimitiveGroup p1, PrimitiveGroup p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PrimitiveGroup p1, Point p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PrimitiveGroup p1, PolyLine p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PrimitiveGroup p1, Polygon p2) => CoarseCollision(p1, p2);
        public override bool CollidesWith(PrimitiveGroup p1, Ellipse p2) => CoarseCollision(p1, p2);

        #endregion
    }
}