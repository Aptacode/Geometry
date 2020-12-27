using Aptacode.Geometry.Composites;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class HybridCollisionDetector : CollisionDetector
    {
        #region Properties

        public readonly CollisionDetector CoarseCollisionDetector;
        public readonly CollisionDetector FineCollisionDetector;

        #endregion

        #region Ctor

        public HybridCollisionDetector()
        {
            CoarseCollisionDetector = new BoundingRectangleCollisionDetector();
            FineCollisionDetector = new FineCollisionDetector();
        }

        public HybridCollisionDetector(CollisionDetector coarseCollisionDetector, CollisionDetector fineCollisionDetector)
        {
            CoarseCollisionDetector = coarseCollisionDetector;
            FineCollisionDetector = fineCollisionDetector;
        }
        
        public static HybridCollisionDetector CoarseCircleCollisionDetector()
        {
            return new HybridCollisionDetector(new BoundingCircleCollisionDetector(), new FineCollisionDetector());
        }

        public static HybridCollisionDetector CoarseRectangleCollisionDetector()
        {
            return new HybridCollisionDetector(new BoundingRectangleCollisionDetector(), new FineCollisionDetector());
        }
        #endregion


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