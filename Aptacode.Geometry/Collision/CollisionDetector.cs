using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public abstract class CollisionDetector
    {
        public bool CollidesWith(Primitive p1, Primitive p2)
        {
            switch (p1)
            {
                case Point p:
                    return CollidesWith(p, p2);
                case PolyLine p:
                    return CollidesWith(p, p2);
                case Polygon p:
                    return CollidesWith(p, p2);
                case Circle p:
                    return CollidesWith(p, p2);
            }

            return false;
        }

        #region Point

        public bool CollidesWith(Point p1, Primitive p2)
        {
            switch (p2)
            {
                case Point p:
                    return CollidesWith(p1, p);
                case PolyLine p:
                    return CollidesWith(p1, p);
                case Polygon p:
                    return CollidesWith(p1, p);
                case Circle p:
                    return CollidesWith(p1, p);
            }

            return false;
        }
        public abstract bool CollidesWith(Point p1, Point p2);
        public abstract bool CollidesWith(Point p1, PolyLine p2);
        public abstract bool CollidesWith(Point p1, Polygon p2);
        public abstract bool CollidesWith(Point p1, Circle p2);

        #endregion

        #region Polyline
        public bool CollidesWith(PolyLine p1, Primitive p2)
        {
            switch (p2)
            {
                case Point p:
                    return CollidesWith(p1, p);
                case PolyLine p:
                    return CollidesWith(p1, p);
                case Polygon p:
                    return CollidesWith(p1, p);
                case Circle p:
                    return CollidesWith(p1, p);
            }

            return false;
        }
        public abstract bool CollidesWith(PolyLine p1, Point p2);
        public abstract bool CollidesWith(PolyLine p1, PolyLine p2);
        public abstract bool CollidesWith(PolyLine p1, Polygon p2);
        public abstract bool CollidesWith(PolyLine p1, Circle p2);

        #endregion

        #region Polygon
        public bool CollidesWith(Polygon p1, Primitive p2)
        {
            switch (p2)
            {
                case Point p:
                    return CollidesWith(p1, p);
                case PolyLine p:
                    return CollidesWith(p1, p);
                case Polygon p:
                    return CollidesWith(p1, p);
                case Circle p:
                    return CollidesWith(p1, p);
            }

            return false;
        }
        public abstract bool CollidesWith(Polygon p1, Point p2);
        public abstract bool CollidesWith(Polygon p1, PolyLine p2);
        public abstract bool CollidesWith(Polygon p1, Polygon p2);
        public abstract bool CollidesWith(Polygon p1, Circle p2);

        #endregion


        #region Circle
        public bool CollidesWith(Circle p1, Primitive p2)
        {
            switch (p2)
            {
                case Point p:
                    return CollidesWith(p1, p);
                case PolyLine p:
                    return CollidesWith(p1, p);
                case Polygon p:
                    return CollidesWith(p1, p);
                case Circle p:
                    return CollidesWith(p1, p);
            }

            return false;
        }
        public abstract bool CollidesWith(Circle p1, Point p2);
        public abstract bool CollidesWith(Circle p1, PolyLine p2);
        public abstract bool CollidesWith(Circle p1, Polygon p2);
        public abstract bool CollidesWith(Circle p1, Circle p2);

        #endregion

    }
}