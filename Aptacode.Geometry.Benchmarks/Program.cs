using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Aptacode.Geometry.Benchmarks
{
    public class CollisionDetectionBenchmarks
    {
        private readonly CollisionDetector _collisionDetector = new CoarseCollisionDetector();
        private readonly Ellipse ellipse1;
        private readonly Ellipse ellipse2;

        public CollisionDetectionBenchmarks()
        {
            ellipse1 = new Ellipse(new Vector2(2, 2), 2);
            ellipse2 = new Ellipse(new Vector2(2, 3), 2);
        }

        [Benchmark] public bool EllipseEllipseCollision()
        {
            return ellipse1.CollidesWith(ellipse2, _collisionDetector);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<CollisionDetectionBenchmarks>();
        }
    }
}
