using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class EllipseCollisionDetectionBenchmarks
    {
        private readonly Ellipse _collidingEllipse;
        private readonly Point _collidingPoint;
        private readonly Polygon _collidingPolygon;
        private readonly PolyLine _collidingPolyline;
        private readonly CollisionDetector _collisionDetector = new FineCollisionDetector();
        private readonly Ellipse _ellipse;

        public EllipseCollisionDetectionBenchmarks()
        {
            _ellipse = new Ellipse(new Vector2(2, 2), new Vector2(2, 2), 0);
            _collidingEllipse = new Ellipse(new Vector2(2, 3), new Vector2(2, 2), 0);
            _collidingPoint = new Point(new Vector2(2, 1));
            _collidingPolygon = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(4, 0));
            _collidingPolyline = PolyLine.Create(new Vector2(0, 0), new Vector2(4, 4));
        }

        [Benchmark]
        public bool PointCollision() => _ellipse.CollidesWith(_collidingPoint, _collisionDetector);

        [Benchmark]
        public bool PolylineCollision() => _ellipse.CollidesWith(_collidingPolyline, _collisionDetector);

        [Benchmark]
        public bool PolygonCollision() => _ellipse.CollidesWith(_collidingPolygon, _collisionDetector);

        [Benchmark]
        public bool CircleCollision() => _ellipse.CollidesWith(_collidingEllipse, _collisionDetector);
    }
}