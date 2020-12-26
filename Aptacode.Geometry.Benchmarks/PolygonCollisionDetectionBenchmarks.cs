using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class PolygonCollisionDetectionBenchmarks
    {
        private readonly Ellipse _collidingEllipse;
        private readonly Point _collidingPoint;
        private readonly Polygon _collidingPolygon;
        private readonly PolyLine _collidingPolyline;
        private readonly CollisionDetector _collisionDetector = new FineCollisionDetector();
        private readonly Polygon _polygon;

        public PolygonCollisionDetectionBenchmarks()
        {
            _polygon = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(4, 0));
            _collidingEllipse = new Ellipse(new Vector2(2, 2), new Vector2(2, 2), 0);
            _collidingPoint = new Point(new Vector2(4, 0));
            _collidingPolygon = Rectangle.Create(new Vector2(0, 0), new Vector2(4, 4));
            _collidingPolyline = PolyLine.Create(new Vector2(0, 0), new Vector2(4, 4));
        }

        [Benchmark]
        public bool PointCollision() => _polygon.CollidesWith(_collidingPoint, _collisionDetector);

        [Benchmark]
        public bool PolylineCollision() => _polygon.CollidesWith(_collidingPolyline, _collisionDetector);

        [Benchmark]
        public bool PolygonCollision() => _polygon.CollidesWith(_collidingPolygon, _collisionDetector);

        [Benchmark]
        public bool CircleCollision() => _polygon.CollidesWith(_collidingEllipse, _collisionDetector);
    }
}