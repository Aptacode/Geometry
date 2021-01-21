using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Polygon : Primitive
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
            return CollisionDetectorMethods.CollidesWith(p, this);
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

        public Polygon(params Vector2[] points) : base(VertexArray.Create(points))
        {
        }
        public Polygon(VertexArray vertices) : base(vertices)
        {
        }

        public static readonly Polygon Zero = new(Vector2.Zero, Vector2.Zero, Vector2.Zero);

        public static Polygon Create(params float[] points)
        {
            if (points.Length < 3)
            {
                return Polygon.Zero;
            }

            var vertices = new Vector2[points.Length / 2];

            var pointIndex = 0;
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vector2(points[pointIndex++], points[pointIndex++]);
            }

            return new Polygon(VertexArray.Create(vertices));
        }

        public static class Rectangle
        {
            public static Polygon Create(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft)
            {
                return new(VertexArray.Create(topLeft, topRight, bottomRight, bottomLeft));
            }
            public static Polygon FromTwoPoints(Vector2 topLeft, Vector2 bottomRight)
            {
                var topRight = new Vector2(bottomRight.X, topLeft.Y);
                var bottomLeft = new Vector2(topLeft.X, bottomRight.Y);

                return Create(topLeft, topRight, bottomRight, bottomLeft);
            }
            public static Polygon FromPositionAndSize(Vector2 topLeft, Vector2 size)
            {
                var bottomRight = topLeft + size;
                return FromTwoPoints(topLeft, bottomRight);
            }
        }

        public static class Triangle
        {
            public static Polygon Create(Vector2 a, Vector2 b, Vector2 c)
            {
                return new(VertexArray.Create(a, b, c));
            }
        }
        
        #endregion

        #region Properties

        private (Vector2 p1, Vector2 p2)[] GetEdges()
        {
            var edges = new (Vector2 p1, Vector2 p2)[Vertices.Length];
            for (var i = 0; i < Vertices.Length - 1; i++)
            {
                edges[i] = (Vertices[i], Vertices[i + 1]);
            }

            edges[Vertices.Length - 1] = (Vertices[^1], Vertices[0]);

            return edges;
        }

        private (Vector2 p1, Vector2 p2)[] _edges;
        public (Vector2 p1, Vector2 p2)[] Edges => _edges ??= GetEdges();

        #endregion

        #region Transformations

        public override Polygon Translate(Vector2 delta)
        {
            if (_edges != null)
            {
                for (var i = 0; i < _edges.Length; i++)
                {
                    var (p1, p2) = _edges[i];
                    _edges[i] = (p1 + delta, p2 + delta);
                }
            }

            base.Translate(delta);
            return this;
        }

        public override Polygon Scale(Vector2 delta)
        {
            _edges = null;
            base.Scale(delta);
            return this;
        }

        public virtual Polygon Rotate(float theta)
        {
            _edges = null;
            base.Rotate(theta);
            return this;
        }

        public virtual Polygon Rotate(Vector2 rotationCenter, float theta)
        {
            _edges = null;
            base.Rotate(rotationCenter, theta);
            return this;
        }

        public virtual Polygon Skew(Vector2 delta)
        {
            _edges = null;
            base.Skew(delta);
            return this;
        }

        #endregion
    }
}