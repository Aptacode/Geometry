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

        #region Transformations

        public override Point Translate(Vector2 delta) => (Point) base.Translate(delta);

        public override Point Rotate(float theta) => (Point) base.Rotate(theta);

        public override Point Rotate(Vector2 rotationCenter, float theta) => (Point) base.Rotate(rotationCenter, theta);

        public override Point Scale(Vector2 delta) => (Point) base.Scale(delta);

        public override Point Skew(Vector2 delta) => (Point) base.Skew(delta);

        #endregion
    }
}