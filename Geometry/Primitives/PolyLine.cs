using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives.Polygons;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record PolyLine : Primitive
    {
        #region Collision Detection

        public override BoundingRectangle GetBoundingRectangle()
        {
            return Vertices.ToBoundingRectangle();
        }

        public override bool CollidesWith(Vector2 p)
        {
            return Vector2CollisionDetector.CollidesWith(this, p);
        }

        public override bool CollidesWith(Point p)
        {
            return CollisionDetectorMethods.CollidesWith(p, this);
        }

        public override bool CollidesWith(Ellipse p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(PolyLine p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(BoundingRectangle p)
        {
            return p.CollidesWith(this);
        }

        public override bool CollidesWith(Polygon p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

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

        public static readonly PolyLine Zero = Create(Vector2.Zero, Vector2.Zero);

        public static PolyLine Create(Vector2 p1, Vector2 p2, params Vector2[] points)
        {
            return new(VertexArray.Create(p1, p2, points));
        }

        public static PolyLine Create(Vector2[] points)
        {
            return new(VertexArray.Create(points));
        }

        #endregion

        #region Properties

        private (Vector2 p1, Vector2 p2)[] GetLineSegments()
        {
            var lineSegments = new (Vector2 p1, Vector2 p2)[Vertices.Length - 1];
            for (var i = 0; i < Vertices.Length - 1; i++)
            {
                lineSegments[i] = (Vertices[i], Vertices[i + 1]);
            }

            return lineSegments;
        }

        private (Vector2 p1, Vector2 p2)[] _lineSegments;
        public (Vector2 p1, Vector2 p2)[] LineSegments => _lineSegments ??= GetLineSegments();

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
            base.Translate(delta);
            return this;
        }

        public override PolyLine Scale(Vector2 delta)
        {
            _lineSegments = null;
            base.Scale(delta);
            return this;
        }

        public virtual PolyLine Rotate(float theta)
        {
            _lineSegments = null;
            base.Rotate(theta);
            return this;
        }

        public virtual PolyLine Rotate(Vector2 rotationCenter, float theta)
        {
            _lineSegments = null;
            base.Rotate(rotationCenter, theta);
            return this;
        }

        public virtual PolyLine Skew(Vector2 delta)
        {
            _lineSegments = null;
            base.Skew(delta);
            return this;
        }

        #endregion
    }
}