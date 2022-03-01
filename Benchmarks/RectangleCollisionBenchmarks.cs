using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks;

public class RectangleCollisionBenchmarks
{
    private readonly Ellipse _ellipse = Ellipse.Create(25, 25, 5, 6, 0.0f);
    private readonly Polygon _poly = Polygon.Create(10, 10, 10, 15, 15, 15, 15, 10); //The same rectangle as below

    private readonly BoundingRectangle _rectangle =
        new BoundingRectangle(new Vector2(10, 10), new Vector2(15, 15));

    [Benchmark]
    public bool Old()
    {
        return CollisionDetectorMethods.CollidesWith(_poly, _ellipse);
    }

    [Benchmark]
    public bool New()
    {
        return _rectangle.CollidesWith(_ellipse);
    }
}