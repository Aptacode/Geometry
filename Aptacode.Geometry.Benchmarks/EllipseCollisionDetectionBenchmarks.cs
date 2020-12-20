using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class EllipseCollisionDetectionBenchmarks
    {
        private readonly CollisionDetector _collisionDetector = new FineCollisionDetector();
        private readonly Ellipse _ellipse;
        
        private readonly Ellipse _collidingEllipse;
        private readonly Point _collidingPoint;
        private readonly Polygon _collidingPolygon;
        private readonly PolyLine _collidingPolyline;

        public EllipseCollisionDetectionBenchmarks()
        {
            _ellipse = new Ellipse(new Vector2(2, 2), 2);
            _collidingEllipse = new Ellipse(new Vector2(2, 3), 2);
            _collidingPoint = new Point(new Vector2(2, 1));
            _collidingPolygon = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(4, 0));
            _collidingPolyline = PolyLine.Create(new Vector2(0, 0), new Vector2(4, 4));
        }

        [Benchmark] 
        public bool PointCollision()
        {
            return _ellipse.CollidesWith(_collidingPoint, _collisionDetector);
        }

        [Benchmark]
        public bool PolylineCollision()
        {
            return _ellipse.CollidesWith(_collidingPolyline, _collisionDetector);
        }
        [Benchmark]
        public bool PolygonCollision()
        {
            return _ellipse.CollidesWith(_collidingPolygon, _collisionDetector);
        }
        [Benchmark]
        public bool CircleCollision()
        {
            return _ellipse.CollidesWith(_collidingEllipse, _collisionDetector);
        }
    }
}