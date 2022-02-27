using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Demo.Pages;

public static class ProfileHelpers
{
    public static Ellipse CreateEllipse(Random r)
    {
        return Ellipse.Create(r.Next(), r.Next(), r.Next(), r.Next(), r.Next());
    }


    public static Point CreatePoint(Random r)
    {
        return Point.Create(r.Next(), r.Next());
    }


    public static Polygon CreatePolygon(Random r)
    {
        var points = new Vector2[r.Next(1, 10)];
        for (var i = 0; i < points.Length; i++) points[i] = new Vector2(r.Next(), r.Next());

        return Polygon.Create(points);
    }

    public static PolyLine CreatePolyline(Random r)
    {
        var points = new Vector2[r.Next(1, 10)];
        for (var i = 0; i < points.Length; i++) points[i] = new Vector2(r.Next(), r.Next());

        return PolyLine.Create(points);
    }
}