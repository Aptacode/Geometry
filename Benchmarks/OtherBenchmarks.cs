using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class OtherBenchmarks
    {

        private readonly SweepingLine _sl = new();
        private readonly Polygon _poly1 = Polygon.Create(Helpers.MakeLargeVertexList(5, 0));
        private readonly Polygon _poly2 = Polygon.Create(Helpers.MakeLargeVertexList(5, 10));

        [Benchmark]
        public bool Old()
        {
            _poly1.CollidesWith(_poly2);
            _poly1.CollidesWith(_poly2);
            _poly1.CollidesWith(_poly2);
            _poly1.CollidesWith(_poly2);
            _poly1.CollidesWith(_poly2);
            _poly1.CollidesWith(_poly2);
            _poly1.CollidesWith(_poly2);
            _poly1.CollidesWith(_poly2);
            _poly1.CollidesWith(_poly2);
            return _poly1.CollidesWith(_poly2);
        }

        [Benchmark]
        public bool New()
        {
            _sl.RunSweepingLine(_poly1, _poly2);
            _sl.RunSweepingLine(_poly1, _poly2);
            _sl.RunSweepingLine(_poly1, _poly2);
            _sl.RunSweepingLine(_poly1, _poly2);
            _sl.RunSweepingLine(_poly1, _poly2);
            _sl.RunSweepingLine(_poly1, _poly2);
            _sl.RunSweepingLine(_poly1, _poly2);
            _sl.RunSweepingLine(_poly1, _poly2);
            _sl.RunSweepingLine(_poly1, _poly2);
            return _sl.RunSweepingLine(_poly1, _poly2);
        }

    }
}