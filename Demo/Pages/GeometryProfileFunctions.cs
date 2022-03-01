using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Demo.Pages;

public static class GeometryProfileFunctions
{
    public static IReadOnlyList<ProfileFunction> GeometryFunctions()
    {
        return new ProfileFunction[]
        {
            //Ellipse
            new PrimitiveCreation<Ellipse>(),
            new PrimitiveFunction<Ellipse>(f => f.GetBoundingPrimitive(10), "get bounding primitive"),
            new PrimitiveFunction<Ellipse>(f => f.Rotate(10), "rotate"),
            new PrimitiveFunction<Ellipse>(f => f.Scale(Vector2.One, new Vector2(10, 5)), "scale"),
            new PrimitiveFunction<Ellipse>(f => f.Skew(new Vector2(10, 5)), "skew"),
            new PrimitiveFunction<Ellipse>(f => f.Translate(new Vector2(10, 5)), "translate"),
            new PrimitiveFunction<Ellipse>(f => f.CollidesWith(new Vector2(10, 5)), "point collision"),
            new PrimitiveFunction<Ellipse>(
                f => f.CollidesWith(new BoundingRectangle(new Vector2(0, 0), new Vector2(10, 10))),
                "rectangle collision"),

            //Point
            new PrimitiveCreation<Point>(),
            new PrimitiveFunction<Point>(f => f.GetBoundingPrimitive(10), "get bounding primitive"),
            new PrimitiveFunction<Point>(f => f.Rotate(10), "rotate"),
            new PrimitiveFunction<Point>(f => f.Scale(Vector2.One, new Vector2(10, 5)), "scale"),
            new PrimitiveFunction<Point>(f => f.Skew(new Vector2(10, 5)), "skew"),
            new PrimitiveFunction<Point>(f => f.Translate(new Vector2(10, 5)), "translate"),
            new PrimitiveFunction<Point>(f => f.CollidesWith(new Vector2(10, 5)), "point collision"),
            new PrimitiveFunction<Point>(
                f => f.CollidesWith(new BoundingRectangle(new Vector2(0, 0), new Vector2(10, 10))),
                "rectangle collision"),

            //PolyLine
            new PrimitiveCreation<PolyLine>(),
            new PrimitiveFunction<PolyLine>(f => f.GetBoundingPrimitive(10), "get bounding primitive"),
            new PrimitiveFunction<PolyLine>(f => f.Rotate(10), "rotate"),
            new PrimitiveFunction<PolyLine>(f => f.Scale(Vector2.One, new Vector2(10, 5)), "scale"),
            new PrimitiveFunction<PolyLine>(f => f.Skew(new Vector2(10, 5)), "skew"),
            new PrimitiveFunction<PolyLine>(f => f.Translate(new Vector2(10, 5)), "translate"),
            new PrimitiveFunction<PolyLine>(f => f.CollidesWith(new Vector2(10, 5)), "point collision"),
            new PrimitiveFunction<PolyLine>(
                f => f.CollidesWith(new BoundingRectangle(new Vector2(0, 0), new Vector2(10, 10))),
                "rectangle collision"),

            //Polygon
            new PrimitiveCreation<Polygon>(),
            new PrimitiveFunction<Polygon>(f => f.GetBoundingPrimitive(10), "get bounding primitive"),
            new PrimitiveFunction<Polygon>(f => f.Rotate(10), "rotate"),
            new PrimitiveFunction<Polygon>(f => f.Scale(Vector2.One, new Vector2(10, 5)), "scale"),
            new PrimitiveFunction<Polygon>(f => f.Skew(new Vector2(10, 5)), "skew"),
            new PrimitiveFunction<Polygon>(f => f.Translate(new Vector2(10, 5)), "translate"),
            new PrimitiveFunction<Polygon>(f => f.CollidesWith(new Vector2(10, 5)), "point collision"),
            new PrimitiveFunction<Polygon>(
                f => f.CollidesWith(new BoundingRectangle(new Vector2(0, 0), new Vector2(10, 10))),
                "rectangle collision")
        };
    }
}