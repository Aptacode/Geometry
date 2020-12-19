using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Ellipse : Primitive
    {
        public readonly float Radius;
        public Vector2 Position => Vertices[0];

        #region IEquatable

        public virtual bool Equals(Ellipse other)
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

        public Ellipse(Vector2 position, float radius) : base(VertexArray.Create(position))
        {
            Radius = radius;
        }

        protected Ellipse(VertexArray vertexArray, float radius) : base(vertexArray)
        {
            Radius = radius;
        }
        
        public static Ellipse Create(float x, float y, float radius)
        {
            return new(new Vector2(x, y), radius);
        }

        public Ellipse(Vector2 position, float radius, BoundingCircle? boundingCircle) : base(
            VertexArray.Create(position), boundingCircle)
        {
            Radius = radius;
        }


        public static readonly Ellipse Zero = new(Vector2.Zero, 0.0f);
        public static readonly Ellipse Unit = new(Vector2.Zero, 1.0f);

        #endregion

        #region Transformations

        public override Ellipse Translate(Vector2 delta) => new(Position + delta, Radius, _boundingCircle);

        public override Ellipse Rotate(float theta) => this;

        public override Ellipse Rotate(Vector2 rotationCenter, float theta) =>
            new(Vertices.Rotate(rotationCenter, theta), Radius);

        public override Ellipse Scale(Vector2 delta) => new(Position, Radius * delta.Length());

        public override Ellipse Skew(Vector2 delta) => this;

        #endregion
    }
}