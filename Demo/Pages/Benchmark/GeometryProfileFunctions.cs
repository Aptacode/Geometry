using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Demo.Pages.Benchmark;

public static class GeometryProfileFunctions
{
    public static IReadOnlyList<ProfileFunction> GeometryFunctions()
    {
        return new ProfileFunction[]
        {
            //Ellipse
            new PrimitiveCreation<Circle>(),
            new PrimitiveFunction<Circle>(f => f.Rotate(10), "rotate"),
            new PrimitiveFunction<Circle>(f => f.Scale(Vector2.One, new Vector2(10, 5)), "scale"),
            new PrimitiveFunction<Circle>(f => f.Skew(new Vector2(10, 5)), "skew"),
            new PrimitiveFunction<Circle>(f => f.Translate(new Vector2(10, 5)), "translate"),
            new PrimitiveFunction<Circle>(f => f.CollidesWith(new Vector2(10, 5)), "point collision"),

            //Point
            new PrimitiveCreation<Point>(),
            new PrimitiveFunction<Point>(f => f.Rotate(10), "rotate"),
            new PrimitiveFunction<Point>(f => f.Scale(Vector2.One, new Vector2(10, 5)), "scale"),
            new PrimitiveFunction<Point>(f => f.Skew(new Vector2(10, 5)), "skew"),
            new PrimitiveFunction<Point>(f => f.Translate(new Vector2(10, 5)), "translate"),
            new PrimitiveFunction<Point>(f => f.CollidesWith(new Vector2(10, 5)), "point collision"),

            //PolyLine
            new PrimitiveCreation<PolyLine>(),
            new PrimitiveFunction<PolyLine>(f => f.Rotate(10), "rotate"),
            new PrimitiveFunction<PolyLine>(f => f.Scale(Vector2.One, new Vector2(10, 5)), "scale"),
            new PrimitiveFunction<PolyLine>(f => f.Skew(new Vector2(10, 5)), "skew"),
            new PrimitiveFunction<PolyLine>(f => f.Translate(new Vector2(10, 5)), "translate"),
            new PrimitiveFunction<PolyLine>(f => f.CollidesWith(new Vector2(10, 5)), "point collision"),

            //Polygon
            new PrimitiveCreation<Polygon>(),
            new PrimitiveFunction<Polygon>(f => f.Rotate(10), "rotate"),
            new PrimitiveFunction<Polygon>(f => f.Scale(Vector2.One, new Vector2(10, 5)), "scale"),
            new PrimitiveFunction<Polygon>(f => f.Skew(new Vector2(10, 5)), "skew"),
            new PrimitiveFunction<Polygon>(f => f.Translate(new Vector2(10, 5)), "translate"),
            new PrimitiveFunction<Polygon>(f => f.CollidesWith(new Vector2(10, 5)), "point collision"),
        };
    }
}