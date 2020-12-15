using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public abstract class CollisionDetector
    {
        public bool CollidesWith(Primitive p1, Primitive p2)
        {
            return p1 switch
            {
                Point p => CollidesWith(p, p2),
                PolyLine p => CollidesWith(p, p2),
                Polygon p => CollidesWith(p, p2),
                Circle p => CollidesWith(p, p2),
                _ => false
            };
        }

        #region Point

        public bool CollidesWith(Point p1, Primitive p2)
        {
            return p2 switch
            {
                Point p => CollidesWith(p1, p),
                PolyLine p => CollidesWith(p1, p),
                Polygon p => CollidesWith(p1, p),
                Circle p => CollidesWith(p1, p),
                _ => false
            };
        }

        public abstract bool CollidesWith(Point p1, Point p2);
        public abstract bool CollidesWith(Point p1, PolyLine p2);
        public abstract bool CollidesWith(Point p1, Polygon p2);
        public abstract bool CollidesWith(Point p1, Circle p2);

        #endregion

        #region Polyline

        public bool CollidesWith(PolyLine p1, Primitive p2)
        {
            return p2 switch
            {
                Point p => CollidesWith(p1, p),
                PolyLine p => CollidesWith(p1, p),
                Polygon p => CollidesWith(p1, p),
                Circle p => CollidesWith(p1, p),
                _ => false
            };
        }

        public abstract bool CollidesWith(PolyLine p1, Point p2);
        public abstract bool CollidesWith(PolyLine p1, PolyLine p2);
        public abstract bool CollidesWith(PolyLine p1, Polygon p2);
        public abstract bool CollidesWith(PolyLine p1, Circle p2);

        #endregion

        #region Polygon

        public bool CollidesWith(Polygon p1, Primitive p2)
        {
            return p2 switch
            {
                Point p => CollidesWith(p1, p),
                PolyLine p => CollidesWith(p1, p),
                Polygon p => CollidesWith(p1, p),
                Circle p => CollidesWith(p1, p),
                _ => false
            };
        }

        public abstract bool CollidesWith(Polygon p1, Point p2);
        public abstract bool CollidesWith(Polygon p1, PolyLine p2);
        public abstract bool CollidesWith(Polygon p1, Polygon p2);
        public abstract bool CollidesWith(Polygon p1, Circle p2);

        #endregion


        #region Circle

        public bool CollidesWith(Circle p1, Primitive p2)
        {
            return p2 switch
            {
                Point p => CollidesWith(p1, p),
                PolyLine p => CollidesWith(p1, p),
                Polygon p => CollidesWith(p1, p),
                Circle p => CollidesWith(p1, p),
                _ => false
            };
        }

        public abstract bool CollidesWith(Circle p1, Point p2);
        public abstract bool CollidesWith(Circle p1, PolyLine p2);
        public abstract bool CollidesWith(Circle p1, Polygon p2);
        public abstract bool CollidesWith(Circle p1, Circle p2);

        #endregion
    }
}