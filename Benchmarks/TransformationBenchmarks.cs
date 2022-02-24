using System.Numerics;
using Aptacode.Geometry.Vertices;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks;

public class TransformationBenchmarks
{
    private readonly Vector2 _translation = new(10, 10);
    private readonly VertexArray _vertexArray1 = VertexArray.Create(new Vector2(0, 0));

    private readonly VertexArray _vertexArray20 = VertexArray.Create(
        new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0),
        new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0),
        new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0),
        new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0));

    private readonly VertexArray _vertexArray4 = VertexArray.Create(
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0));

    [Benchmark]
    public VertexArray Translate1()
    {
        _vertexArray1.Translate(_translation);
        return _vertexArray1;
    }

    [Benchmark]
    public VertexArray Translate4()
    {
        _vertexArray4.Translate(_translation);
        return _vertexArray4;
    }

    [Benchmark]
    public VertexArray Translate20()
    {
        _vertexArray20.Translate(_translation);
        return _vertexArray20;
    }
}