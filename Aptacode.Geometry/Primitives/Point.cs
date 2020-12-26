using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Point : Primitive
    {
        public Point(Vector2 position) : base(VertexArray.Create(position)) { }
        protected Point(VertexArray vertexArray) : base(vertexArray) { }

        public Point(Vector2 position, BoundingCircle? boundingCircle, BoundingRectangle? boundingRectangle) : base(
            VertexArray.Create(position), boundingCircle, boundingRectangle) { }

        public Vector2 Position => Vertices[0];

        public static Point Create(float x, float y) => new(new Vector2(x, y));

        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Construction

        public static readonly Point Zero = new(Vector2.Zero);
        public static readonly Point Unit = new(Vector2.One);

        #endregion

    }
}