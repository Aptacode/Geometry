using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;

namespace Aptacode.Geometry.Primitives
{
    public record PolyLine(IEnumerable<Vector2> Points) : Primitive(Points)
    {
        #region Properties
        
        private IEnumerable<(Vector2 p1, Vector2 p2)> CalculateLineSegments()
        {
            return _lineSegments = Vertices.Zip(Vertices.Skip(1), (a, b) => (a, b));
        }

        private IEnumerable<(Vector2 p1, Vector2 p2)>? _lineSegments;

        public IEnumerable<(Vector2 p1, Vector2 p2)> LineSegments
        {
            get => _lineSegments ?? CalculateLineSegments();
            init => _lineSegments = value;
        } 

        #endregion

        #region Construction

        public static PolyLine Create(Vector2 p1, Vector2 p2, params Vector2[] points)
        {
            var allPoints = new List<Vector2>
            {
                p1, p2
            };

            allPoints.AddRange(points);

            return new PolyLine(allPoints.ToArray());
        }

        #endregion

        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Transformations

        public override PolyLine Translate(Vector2 delta)
        {
            return new(Vertices.Select(v => v + delta).ToArray())
            {
                BoundingCircle = BoundingCircle.Translate(delta),
                LineSegments = LineSegments.Select(l => (l.p1 + delta, l.p2 + delta))
            };
        }

        public override PolyLine Rotate(float delta) => this;

        public override PolyLine Scale(Vector2 delta) => this;

        public override PolyLine Skew(Vector2 delta) => this;

        #endregion
    }
}