using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public interface ICollisionDetector
    {
        bool CollidesWith(Point p1, Point p2);
        bool CollidesWith(Point p1, PolyLine p2);
        bool CollidesWith(Point p1, Polygon p2);
        bool CollidesWith(Point p1, Circle p2);

        bool CollidesWith(PolyLine p1, Point p2);
        bool CollidesWith(PolyLine p1, PolyLine p2);
        bool CollidesWith(PolyLine p1, Polygon p2);
        bool CollidesWith(PolyLine p1, Circle p2);

        bool CollidesWith(Polygon p1, Point p2);
        bool CollidesWith(Polygon p1, PolyLine p2);
        bool CollidesWith(Polygon p1, Polygon p2);
        bool CollidesWith(Polygon p1, Circle p2);

        bool CollidesWith(Circle p1, Point p2);
        bool CollidesWith(Circle p1, PolyLine p2);
        bool CollidesWith(Circle p1, Polygon p2);
        bool CollidesWith(Circle p1, Circle p2);
    }
}