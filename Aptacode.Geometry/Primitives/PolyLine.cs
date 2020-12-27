using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
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

        public static PolyLine Create(params float[] points)
        {
            if (points.Length < 2)
            {
                return Zero;
            }

            var vertices = new Vector2[points.Length / 2];
            var count = 0;
            for (var i = 0; i < points.Length; i += 2)
            {
                vertices[count++] = new Vector2(points[i], points[i + 1]);
            }

            return new PolyLine(VertexArray.Create(vertices));
        }

        public PolyLine(VertexArray vertices, BoundingCircle? boundingCircle, BoundingRectangle? boundingRectangle) :
            base(vertices, boundingCircle, boundingRectangle) { }

        public static readonly PolyLine Zero = Create(Vector2.Zero, Vector2.Zero);

        public static PolyLine Create(Vector2 p1, Vector2 p2, params Vector2[] points) =>
            new(VertexArray.Create(p1, p2, points));

        #endregion

        #region Properties

        private (Vector2 p1, Vector2 p2)[] CalculateLineSegments()
        {
            var lineSegments = new (Vector2 p1, Vector2 p2)[Vertices.Length - 1];
            for (var i = 0; i < Vertices.Length - 1; i++)
            {
                lineSegments[i] = (Vertices[i], Vertices[i + 1]);
            }

            return lineSegments;
        }

        private (Vector2 p1, Vector2 p2)[] _lineSegments;
        public (Vector2 p1, Vector2 p2)[] LineSegments => _lineSegments ??= CalculateLineSegments();

        #endregion

        #region Transformations

        public override PolyLine Translate(Vector2 delta)
        {
            if (_lineSegments != null)
            {
                for (var i = 0; i < _lineSegments.Length; i++)
                {
                    var (p1, p2) = _lineSegments[i];
                    _lineSegments[i] = (p1 + delta, p2 + delta);
                }
            }

            return (PolyLine) base.Translate(delta);
        }
        
        public override PolyLine Scale(Vector2 delta)
        {
            if (_lineSegments != null)
            {
                for (var i = 0; i < _lineSegments.Length; i++)
                {
                    var (p1, p2) = _lineSegments[i];
                    _lineSegments[i] = (p1 * delta, p2 * delta);
                }
            }

            return (PolyLine)base.Scale(delta);
        }


        public virtual PolyLine Rotate(float theta)
        {
            _lineSegments = null;
            return (PolyLine)base.Rotate(theta);
        }

        public virtual PolyLine Rotate(Vector2 rotationCenter, float theta)
        {
            _lineSegments = null;
            return (PolyLine)base.Rotate(rotationCenter, theta);
        }

        public virtual PolyLine Skew(Vector2 delta)
        {
            _lineSegments = null;
            return (PolyLine)base.Skew(delta);
        }
        #endregion
    }
}