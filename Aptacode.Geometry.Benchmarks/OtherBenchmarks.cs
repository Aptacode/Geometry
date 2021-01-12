using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Vertices;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class OtherBenchmarks
    {
        private readonly Vector2 A = new(10, 10);
        private readonly Vector2 B = new(20, 20);
        private readonly Vector2 C = new(15, 15);


        [Benchmark]
        public bool Old()
        {
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
           Helpers.OnLineSegment((A, B), C);
            return Helpers.OnLineSegment((A, B), C);
        }

        [Benchmark]
        public bool New()
        {
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            Helpers.newOnLineSegment((A, B), C);
            return Helpers.newOnLineSegment((A, B), C);
        }

    }
}