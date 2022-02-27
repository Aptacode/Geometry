using System.Numerics;
using Aptacode.Geometry.Collision;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class LineSegmentBenchmarks
    {
        private readonly Vector2 A1 = new(10, 10);
        private readonly Vector2 A2 = new(20, 20);

        private readonly Vector2 B1 = new(20, 10);
        private readonly Vector2 B2 = new(10, 20);

        [Benchmark]
        public bool LineSegmentIntersection()
        {
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            return Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
        }

        [Benchmark]
        public bool newLineSegmentIntersection()
        {
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
            return Helpers.LineSegmentIntersection((A1, A2), (B1, B2));
        }

    }
}