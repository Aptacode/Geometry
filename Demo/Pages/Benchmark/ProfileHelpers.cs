using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Demo.Pages.Benchmark;

public static class ProfileHelpers
{
    public static Circle CreateEllipse(Random r)
    {
        return new Circle(r.Next(), r.Next(), r.Next());
    }


    public static Point CreatePoint(Random r)
    {
        return new Point(r.Next(), r.Next());
    }


    public static Polygon CreatePolygon(Random r)
    {
        var points = new Vector2[r.Next(1, 10)];
        for (var i = 0; i < points.Length; i++)
        {
            points[i] = new Vector2(r.Next(), r.Next());
        }

        return new Polygon(points);
    }

    public static PolyLine CreatePolyline(Random r)
    {
        var points = new Vector2[r.Next(1, 10)];
        for (var i = 0; i < points.Length; i++)
        {
            points[i] = new Vector2(r.Next(), r.Next());
        }

        return PolyLine.Create(points);
    }
}