using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Utilities;
using Aptacode.Geometry.Vertices;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class Vector2ExtensionBenchmarks
    {
        private readonly Vector2 A = new(10, 10);
        private readonly Vector2 B = new(20, 20);


        [Benchmark]
        public float Vector2Cross()
        {
            Vector2Extensions.VectorCross(A, B);
            Vector2Extensions.VectorCross(A, B);
            Vector2Extensions.VectorCross(A, B);
            Vector2Extensions.VectorCross(A, B);
            Vector2Extensions.VectorCross(A, B);
            Vector2Extensions.VectorCross(A, B);
            Vector2Extensions.VectorCross(A, B);
            Vector2Extensions.VectorCross(A, B);
            Vector2Extensions.VectorCross(A, B);
            return Vector2Extensions.VectorCross(A, B);
        }

        [Benchmark]
        public float Vector2PerpDot()
        {
            Vector2Extensions.PerpDot(A, B);
            Vector2Extensions.PerpDot(A, B);
            Vector2Extensions.PerpDot(A, B);
            Vector2Extensions.PerpDot(A, B);
            Vector2Extensions.PerpDot(A, B);
            Vector2Extensions.PerpDot(A, B);
            Vector2Extensions.PerpDot(A, B);
            Vector2Extensions.PerpDot(A, B);
            Vector2Extensions.PerpDot(A, B);
            return Vector2Extensions.PerpDot(A, B);
        }

    }
}