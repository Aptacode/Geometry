using System.Numerics;
using Aptacode.Geometry.Primitives;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class EllipseCollisionDetectionBenchmarks
    {
        private readonly Ellipse _collidingEllipse;
        private readonly Point _collidingPoint;
        private readonly Polygon _collidingPolygon;
        private readonly PolyLine _collidingPolyline;
        private readonly Ellipse _ellipse;

        public EllipseCollisionDetectionBenchmarks()
        {
            _ellipse = Ellipse.Create(new Vector2(2, 2), new Vector2(2, 2), 0);
            _collidingEllipse = Ellipse.Create(new Vector2(2, 3), new Vector2(2, 2), 0);
            _collidingPoint = Point.Create(new Vector2(2, 1));
            _collidingPolyline = PolyLine.Create(new Vector2(0, 0), new Vector2(4, 4));
        }

        [Benchmark]
        public bool PointCollision()
        {
            return _ellipse.CollidesWith(_collidingPoint);
        }

        [Benchmark]
        public bool PolylineCollision()
        {
            return _ellipse.CollidesWith(_collidingPolyline);
        }

        [Benchmark]
        public bool PolygonCollision()
        {
            return _ellipse.CollidesWith(_collidingPolygon);
        }

        [Benchmark]
        public bool CircleCollision()
        {
            return _ellipse.CollidesWith(_collidingEllipse);
        }
    }
}