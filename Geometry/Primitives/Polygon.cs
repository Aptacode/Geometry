using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Polygon : Primitive
    {
        #region Properties

        public (Vector2 p1, Vector2 p2)[] Edges { get; protected set; }

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

        protected Polygon(VertexArray vertices, BoundingRectangle boundingRectangle, (Vector2 p1, Vector2 p2)[] edges) : base(vertices, boundingRectangle)
        {
            Edges = edges;
        }

        public static Polygon Create(params Vector2[] vertices)
        {
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minY = float.MaxValue;
            var maxY = float.MinValue;

            var edges = new (Vector2 p1, Vector2 p2)[vertices.Length];
            var vertexArray = new Vector2[vertices.Length];
            var lastVertex = Vector2.Zero;
            for (var i = 0; i < vertices.Length; i++)
            {
                var vertex = vertexArray[i] = vertices[i];

                if (i > 0)
                {
                    edges[i - 1] = (lastVertex, vertex);
                }

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

            return new Polygon(VertexArray.Create(vertexArray),
                BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY)),
                edges);
        }

        public static Polygon Create(params float[] points)
        {
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minY = float.MaxValue;
            var maxY = float.MinValue;

            var vertexArray = new Vector2[points.Length / 2];
            var edges = new (Vector2 p1, Vector2 p2)[points.Length / 2];
            var lastVertex = Vector2.Zero;

            var vertexIndex = 0;
            for (var i = 0; i < vertexArray.Length; i++)
            {
                var vertex = vertexArray[vertexIndex++] = new Vector2(points[i], points[i++]);

                if (vertexIndex > 0)
                {
                    edges[vertexIndex - 1] = (lastVertex, vertex);
                }

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

            return new Polygon(VertexArray.Create(vertexArray),
                BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY)),
                edges);
        }

        public static class Rectangle
        {
            public static Polygon Create(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft)
            {
                return Polygon.Create(topLeft, topRight, bottomRight, bottomLeft);
            }

            public static Polygon FromTwoPoints(Vector2 topLeft, Vector2 bottomRight)
            {
                var topRight = new Vector2(bottomRight.X, topLeft.Y);
                var bottomLeft = new Vector2(topLeft.X, bottomRight.Y);

                return Polygon.Create(topLeft, topRight, bottomRight, bottomLeft);
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
                return Polygon.Create(a, b, c);
            }
        }

        #endregion

        #region Transformations

        public override Polygon Translate(Vector2 delta)
        {
            for (var i = 0; i < Edges.Length; i++)
            {
                var (p1, p2) = Edges[i];
                Edges[i] = (p1 + delta, p2 + delta);
            }

            base.Translate(delta);
            return this;
        }

        public override Polygon Scale(Vector2 delta)
        {
            //ToDo Scale Edges
            base.Scale(delta);
            return this;
        }

        public virtual Polygon Rotate(float theta)
        {
            //ToDo Rotate Edges
            base.Rotate(theta);
            return this;
        }

        public virtual Polygon Rotate(Vector2 rotationCenter, float theta)
        {
            //ToDo Rotate Edges
            base.Rotate(rotationCenter, theta);
            return this;
        }

        public virtual Polygon Skew(Vector2 delta)
        {
            //ToDo Skew Edges
            base.Skew(delta);
            return this;
        }

        #endregion
    }
}