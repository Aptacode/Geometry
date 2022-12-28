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
        return new Point(0, 0);
    }

    [Benchmark]
    public Primitive CreateEllipse()
    {
        return new Circle(0, 0, 2);
    }

    [Benchmark]
    public Primitive CreatePolygon()
    {
        return new Polygon(0, 0, 0, 1, 1, 1, 1, 0);
    }

    [Benchmark]
    public Primitive CreatePolyline()
    {
        return new PolyLine(0, 0, 0, 1, 1, 1, 1, 0);
    }
}