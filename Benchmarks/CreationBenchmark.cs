using Aptacode.Geometry.Primitives;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters.Csv;

namespace Aptacode.Geometry.Benchmarks;

[JsonExporterAttribute.Full]
[CsvMeasurementsExporter]
[CsvExporter(CsvSeparator.Comma)]
[HtmlExporter]
[MarkdownExporterAttribute.GitHub]
public class CreationBenchmark
{
    [Benchmark]
    public Primitive CreatePoint()
    {
        return Point.Create(0, 0);
    }

    [Benchmark]
    public Primitive CreateEllipse()
    {
        return Ellipse.Create(0, 0, 1, 2, 2);
    }

    [Benchmark]
    public Primitive CreatePolygon()
    {
        return Polygon.Create(0, 0, 0, 1, 1, 1, 1, 0);
    }

    [Benchmark]
    public Primitive CreatePolyline()
    {
        return PolyLine.Create(0, 0, 0, 1, 1, 1, 1, 0);
    }
}