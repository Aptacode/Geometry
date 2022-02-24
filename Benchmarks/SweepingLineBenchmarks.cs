using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks;

public class SweepingLineBenchmarks
{
    private readonly Polygon _poly1 = Polygon.Create(10, 10, 10, 15, 15, 20, 20, 15, 20, 10);
    private readonly Polygon _poly2 = Polygon.Create(25, 10, 25, 15, 30, 20, 35, 15, 35, 10);

    [Benchmark]
    public bool Old()
    {
        _poly1.CollidesWith(_poly2);
        _poly1.CollidesWith(_poly2);
        _poly1.CollidesWith(_poly2);
        _poly1.CollidesWith(_poly2);
        _poly1.CollidesWith(_poly2);
        _poly1.CollidesWith(_poly2);
        _poly1.CollidesWith(_poly2);
        _poly1.CollidesWith(_poly2);
        _poly1.CollidesWith(_poly2);
        return _poly1.CollidesWith(_poly2);
    }

    [Benchmark]
    public bool New()
    {
        SweepingLine.CheckCollision(_poly1, _poly2);
        SweepingLine.CheckCollision(_poly1, _poly2);
        SweepingLine.CheckCollision(_poly1, _poly2);
        SweepingLine.CheckCollision(_poly1, _poly2);
        SweepingLine.CheckCollision(_poly1, _poly2);
        SweepingLine.CheckCollision(_poly1, _poly2);
        SweepingLine.CheckCollision(_poly1, _poly2);
        SweepingLine.CheckCollision(_poly1, _poly2);
        SweepingLine.CheckCollision(_poly1, _poly2);
        return SweepingLine.CheckCollision(_poly1, _poly2);
    }
}