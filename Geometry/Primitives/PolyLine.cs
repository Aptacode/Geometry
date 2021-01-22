using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record PolyLine : Primitive
    {
        #region Properties

        public (Vector2 p1, Vector2 p2)[] LineSegments { get; protected set; }

        #endregion

        #region Collision Detection

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

        protected PolyLine(VertexArray vertices, BoundingRectangle boundingRectangle, (Vector2 p1, Vector2 p2)[] lineSegments) : base(vertices, boundingRectangle)
        {
            LineSegments = lineSegments;
        }

        public static PolyLine Create(params float[] points)
        {
            if (points.Length < 2)
            {
                return Zero;
            }

            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minY = float.MaxValue;
            var maxY = float.MinValue;

            var vertexCount = points.Length / 2;
            var vertices = new Vector2[vertexCount];
            var lineSegments = new (Vector2 p1, Vector2 p2)[vertexCount - 1];

            var count = 0;
            var lastVertex = Vector2.Zero;
            for (var i = 0; i < points.Length; i += 2)
            {
                var vertex = vertices[count++] = new Vector2(points[i], points[i + 1]);

                if (count > 1)
                {
                    lineSegments[count - 2] = (lastVertex, vertex);
                }

                lastVertex = vertex;

                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }
                else if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }
                else if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }
            }

            return new PolyLine(VertexArray.Create(vertices),
                BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY)),
                lineSegments);
        }

        public static readonly PolyLine Zero = Create(Vector2.Zero, Vector2.Zero);

        public static PolyLine Create(params Vector2[] points)
        {
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minY = float.MaxValue;
            var maxY = float.MinValue;

            var lineSegments = new (Vector2 p1, Vector2 p2)[points.Length - 1];

            var lastVertex = Vector2.Zero;
            for (var i = 0; i < points.Length; i++)
            {
                var vertex = points[i];

                if (i > 0)
                {
                    lineSegments[i - 1] = (lastVertex, vertex);
                }

                lastVertex = vertex;

                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }
                else if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }
                else if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }
            }

            return new PolyLine(VertexArray.Create(points),
                BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY)),
                lineSegments);
        }

        #endregion

        #region Transformations

        public override PolyLine Translate(Vector2 delta)
        {
            for (var i = 0; i < LineSegments.Length; i++)
            {
                var (p1, p2) = LineSegments[i];
                LineSegments[i] = (p1 + delta, p2 + delta);
            }

            base.Translate(delta);
            return this;
        }

        public override PolyLine Scale(Vector2 delta)
        {
            //Todo Scale LineSegments
            base.Scale(delta);
            return this;
        }

        public virtual PolyLine Rotate(float theta)
        {
            //Todo Rotate LineSegments
            base.Rotate(theta);
            return this;
        }

        public virtual PolyLine Rotate(Vector2 rotationCenter, float theta)
        {
            //Todo Rotate LineSegments
            base.Rotate(rotationCenter, theta);
            return this;
        }

        public virtual PolyLine Skew(Vector2 delta)
        {
            //Todo Skew LineSegments
            base.Skew(delta);
            return this;
        }

        #endregion
    }
}