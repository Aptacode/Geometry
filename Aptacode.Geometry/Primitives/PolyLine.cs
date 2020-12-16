using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record PolyLine : Primitive
    {
        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Construction

        public PolyLine(VertexArray vertices) : base(vertices)
        {
            _lineSegments = null;
        }

        public PolyLine(VertexArray vertices, BoundingCircle boundingCircle,
            IEnumerable<(Vector2 p1, Vector2 p2)> lineSegments) : base(vertices, boundingCircle)
        {
            _lineSegments = lineSegments;
        }

        public static readonly PolyLine Zero = Create(Vector2.Zero, Vector2.Zero);

        public static PolyLine Create(Vector2 p1, Vector2 p2, params Vector2[] points) =>
            new(VertexArray.Create(p1, p2, points));

        #endregion

        #region Properties

        private IEnumerable<(Vector2 p1, Vector2 p2)> CalculateLineSegments()
        {
            return Vertices.Zip(Vertices.Skip(1), (a, b) => (a, b));
        }

        private IEnumerable<(Vector2 p1, Vector2 p2)> _lineSegments;
        public IEnumerable<(Vector2 p1, Vector2 p2)> LineSegments => _lineSegments ??= CalculateLineSegments();

        #endregion

        #region Transformations

        public override PolyLine Translate(Vector2 delta)
        {
            return new(
                Vertices.Translate(delta),
                BoundingCircle.Translate(delta),
                LineSegments.Select(l => (l.p1 + delta, l.p2 + delta)));
        }

        public override PolyLine Rotate(float delta) => this;

        public override PolyLine Scale(Vector2 delta) => this;

        public override PolyLine Skew(Vector2 delta) => this;

        #endregion
    }
}