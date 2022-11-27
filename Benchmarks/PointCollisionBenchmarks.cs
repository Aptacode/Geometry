using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Csv;

namespace Aptacode.Geometry.Benchmarks;

[JsonExporterAttribute.Full]
[CsvMeasurementsExporter]
[CsvExporter(CsvSeparator.Comma)]
[HtmlExporter]
[MarkdownExporterAttribute.GitHub]
public class PointCollisionBenchmarks
{
    public IEnumerable<object[]> CollidingPrimitives()
    {
        yield return new object[] { Point.Zero, Point.Zero };
        yield return new object[] { Point.Zero, Ellipse.Unit };
        yield return new object[] { Point.Zero, Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One) };
        yield return new object[] { Point.Zero, PolyLine.Create(0, 0, 1, 1) };
    }

    public IEnumerable<object[]> NonCollidingPrimitives()
    {
        yield return new object[] { Point.Create(10, 10), Point.Zero };
        yield return new object[] { Point.Create(10, 10), Ellipse.Unit };
        yield return new object[] { Point.Create(10, 10), Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One) };
        yield return new object[] { Point.Create(10, 10), PolyLine.Create(0, 0, 1, 1) };
    }

    [Benchmark]
    [ArgumentsSource(nameof(CollidingPrimitives))]
    public bool PointCollision(Primitive a, Primitive b)
    {
        return a.CollidesWithPrimitive(b);
    }

    [Benchmark]
    [ArgumentsSource(nameof(NonCollidingPrimitives))]
    public bool PointNonCollision(Primitive a, Primitive b)
    {
        return a.CollidesWithPrimitive(b);
    }
}