using System.Numerics;
using Aptacode.Geometry.Primitives;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class PolygonCollisionDetectionBenchmarks
    {
        private readonly Ellipse _collidingEllipse;
        private readonly Point _collidingPoint;
        private readonly Polygon _collidingPolygon;
        private readonly PolyLine _collidingPolyline;
        private readonly Polygon _polygon;

        public PolygonCollisionDetectionBenchmarks()
        {
            _collidingEllipse = Ellipse.Create(new Vector2(2, 2), new Vector2(2, 2), 0);
            _collidingPoint = Point.Create(new Vector2(4, 0));
            _collidingPolyline = PolyLine.Create(new Vector2(0, 0), new Vector2(4, 4));
        }

        [Benchmark]
        public bool PointCollision()
        {
            return _collidingPoint.CollidesWith(_polygon);
        }

        [Benchmark]
        public bool PolylineCollision()
        {
            return _collidingPolyline.CollidesWith(_polygon);
        }

        [Benchmark]
        public bool PolygonCollision()
        {
            return _collidingPolygon.CollidesWith(_polygon);
        }

        [Benchmark]
        public bool CircleCollision()
        {
            return _collidingEllipse.CollidesWith(_polygon);
        }
    }
}