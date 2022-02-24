using System.Numerics;

namespace Aptacode.Geometry.Collision.Circles;

public readonly struct BoundingCircle
{
    public readonly Vector2 Center;
    public readonly float Radius;

    public BoundingCircle(Vector2 center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    public static BoundingCircle Zero => new(new Vector2(0.0f, 0.0f), 0.0f);

    public static BoundingCircle FromOnePoint(Vector2 p1)
    {
        return new BoundingCircle(p1, 0.0f);
    }

    public static BoundingCircle FromTwoPoints(Vector2 p1, Vector2 p2)
    {
        var midpoint = p1 + (p2 - p1) / 2;
        var position = new Vector2(midpoint.X, midpoint.Y);
        var radius = (p2 - p1).Length() / 2;

        return new BoundingCircle(position, radius);
    }

    public static BoundingCircle FromThreePoints(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        BoundingCircle tempCircle;
        if ((tempCircle = FromTwoPoints(p1, p2)).Contains(p3)) return tempCircle;

        if ((tempCircle = FromTwoPoints(p1, p3)).Contains(p2)) return tempCircle;

        if ((tempCircle = FromTwoPoints(p2, p3)).Contains(p1)) return tempCircle;

        var a = p3.LengthSquared() - p2.LengthSquared();
        var b = p1.LengthSquared() - p3.LengthSquared();
        var c = p2.LengthSquared() - p1.LengthSquared();
        var d = (p3.X - p1.X) * (p1.Y - p2.Y) -
                (p2.X - p1.X) *
                (p1.Y - p3.Y); //This is zero if the 3 points are colinear, might need some exception handling


        var cx = (a * p1.Y + b * p2.Y + c * p3.Y) / (2 * d);
        var cy = (a * p1.X + b * p2.X + c * p3.X) / (-2 * d);

        var position = new Vector2(cx, cy);
        var radius = (p1 - position).Length();

        return new BoundingCircle(position, radius);
    }
}