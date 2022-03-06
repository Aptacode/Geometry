using System.Numerics;
using Aptacode.Geometry.Primitives;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks;

public class PolygonCollisionDetectionBenchmarks
{
    private readonly Ellipse _collidingEllipse;
    private readonly Point _collidingPoint;
    private readonly Polygon _collidingPolygon;
    private readonly PolyLine _collidingPolyline;

    public PolygonCollisionDetectionBenchmarks()
    {
        _collidingEllipse = Ellipse.Create(new Vector2(2, 2), new Vector2(2, 2), 0);
        _collidingPoint = Point.Create(new Vector2(4, 0));
        _collidingPolyline = PolyLine.Create(new Vector2(0, 0), new Vector2(4, 4));
        _collidingPolygon = Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One);
    }

    [Benchmark]
    public bool PointCollision()
    {
        return _collidingPoint.CollidesWith(_collidingPolygon);
    }

    [Benchmark]
    public bool PolylineCollision()
    {
        return _collidingPolyline.CollidesWith(_collidingPolygon);
    }

    [Benchmark]
    public bool PolygonCollision()
    {
        return _collidingPolygon.CollidesWith(_collidingPolygon);
    }

    [Benchmark]
    public bool CircleCollision()
    {
        return _collidingEllipse.CollidesWith(_collidingPolygon);
    }
}