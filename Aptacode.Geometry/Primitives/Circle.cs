using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Circle : Primitive
    {
        public readonly float Radius;
        public Vector2 Position => Vertices[0];

        #region IEquatable

        public virtual bool Equals(Circle other)
        {
            var delta = Position - other.Position;
            return Math.Abs(delta.X) < Constants.Tolerance &&
                   Math.Abs(delta.Y) < Constants.Tolerance &&
                   Math.Abs(Radius) < Constants.Tolerance;
        }

        #endregion

        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Construction

        public Circle(Vector2 position, float radius) : base(VertexArray.Create(position))
        {
            Radius = radius;
        }

        protected Circle(VertexArray vertexArray, float radius) : base(vertexArray)
        {
            Radius = radius;
        }

        public Circle(Vector2 position, float radius, BoundingCircle? boundingCircle) : base(
            VertexArray.Create(position), boundingCircle)
        {
            Radius = radius;
        }


        public static readonly Circle Zero = new(Vector2.Zero, 0.0f);
        public static readonly Circle Unit = new(Vector2.Zero, 1.0f);

        #endregion

        #region Transformations

        public override Circle Translate(Vector2 delta) => new(Position + delta, Radius, _boundingCircle);

        public override Circle Rotate(float theta) => this;

        public override Circle Rotate(Vector2 rotationCenter, float theta) =>
            new(Vertices.Rotate(rotationCenter, theta), Radius);

        public override Circle Scale(Vector2 delta) => new(Position, Radius * delta.Length());

        public override Circle Skew(Vector2 delta) => this;

        #endregion
    }
}