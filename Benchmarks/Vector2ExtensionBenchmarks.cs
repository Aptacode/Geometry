using System.Numerics;
using Aptacode.Geometry.Utilities;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks;

public class Vector2ExtensionBenchmarks
{
    private readonly Vector2 A = new(10, 10);
    private readonly Vector2 B = new(20, 20);


    [Benchmark]
    public float Vector2Cross()
    {
        A.VectorCross(B);
        A.VectorCross(B);
        A.VectorCross(B);
        A.VectorCross(B);
        A.VectorCross(B);
        A.VectorCross(B);
        A.VectorCross(B);
        A.VectorCross(B);
        A.VectorCross(B);
        return A.VectorCross(B);
    }

    [Benchmark]
    public float Vector2PerpDot()
    {
        A.PerpDot(B);
        A.PerpDot(B);
        A.PerpDot(B);
        A.PerpDot(B);
        A.PerpDot(B);
        A.PerpDot(B);
        A.PerpDot(B);
        A.PerpDot(B);
        A.PerpDot(B);
        return A.PerpDot(B);
    }
}