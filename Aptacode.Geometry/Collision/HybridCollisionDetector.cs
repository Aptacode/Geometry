using Aptacode.Geometry.Composites;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class HybridCollisionDetector : CollisionDetector
    {
        public static readonly BoundingCircleCollisionDetector CoarseCollisionDetector = new();
        public static readonly FineCollisionDetector FineCollisionDetector = new();

        #region Point

        public override bool CollidesWith(Point p1, Point p2) => p1.Equals(p2);

        public override bool CollidesWith(Point p1, PolyLine p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                    FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Point p1, Polygon p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                   FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Point p1, Ellipse p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                   FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Point p1, PrimitiveGroup p2) =>
            CoarseCollisionDetector.CollidesWith(p1, p2) &&
            FineCollisionDetector.CollidesWith(p1, p2);

        #endregion

        #region PolyLine

        public override bool CollidesWith(PolyLine p1, Point p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                    FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PolyLine p1, PolyLine p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                       FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PolyLine p1, Polygon p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                      FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PolyLine p1, Ellipse p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                      FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PolyLine p1, PrimitiveGroup p2) =>
            CoarseCollisionDetector.CollidesWith(p1, p2) &&
            FineCollisionDetector.CollidesWith(p1, p2);

        #endregion

        #region Polygon

        public override bool CollidesWith(Polygon p1, Point p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                   FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Polygon p1, PolyLine p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                      FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Polygon p1, Polygon p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                     FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Polygon p1, Ellipse p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                     FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Polygon p1, PrimitiveGroup p2) =>
            CoarseCollisionDetector.CollidesWith(p1, p2) &&
            FineCollisionDetector.CollidesWith(p1, p2);

        #endregion

        #region Circle

        public override bool CollidesWith(Ellipse p1, Point p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                   FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Ellipse p1, PolyLine p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                      FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Ellipse p1, Polygon p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                     FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(Ellipse p1, Ellipse p2) => CoarseCollisionDetector.CollidesWith(p1, p2) &&
                                                                     FineCollisionDetector.CollidesWith(p1, p2);


        public override bool CollidesWith(Ellipse p1, PrimitiveGroup p2) =>
            CoarseCollisionDetector.CollidesWith(p1, p2) &&
            FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PrimitiveGroup p1, PrimitiveGroup p2) =>
            CoarseCollisionDetector.CollidesWith(p1, p2) &&
            FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PrimitiveGroup p1, Point p2) =>
            CoarseCollisionDetector.CollidesWith(p1, p2) &&
            FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PrimitiveGroup p1, PolyLine p2) =>
            CoarseCollisionDetector.CollidesWith(p1, p2) &&
            FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PrimitiveGroup p1, Polygon p2) =>
            CoarseCollisionDetector.CollidesWith(p1, p2) &&
            FineCollisionDetector.CollidesWith(p1, p2);

        public override bool CollidesWith(PrimitiveGroup p1, Ellipse p2) =>
            CoarseCollisionDetector.CollidesWith(p1, p2) &&
            FineCollisionDetector.CollidesWith(p1, p2);

        #endregion
    }
}