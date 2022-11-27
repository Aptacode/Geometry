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
public class EllipseCollisionBenchmarks
{
    public IEnumerable<object[]> CollidingPrimitives()
    {
        yield return new object[] { Ellipse.Unit, Point.Zero };
        yield return new object[] { Ellipse.Unit, Ellipse.Unit };
        yield return new object[] { Ellipse.Unit, Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One) };
        yield return new object[] { Ellipse.Unit, PolyLine.Create(0, 0, 1, 1) };
    }

    [Benchmark]
    [ArgumentsSource(nameof(CollidingPrimitives))]
    public bool PointCollision(Primitive a, Primitive b)
    {
        return a.CollidesWithPrimitive(b);
    }
}