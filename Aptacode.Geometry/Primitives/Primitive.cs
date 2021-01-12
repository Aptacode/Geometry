using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public abstract record Primitive
    {
        public readonly VertexArray Vertices;

        #region IEquatable

        public virtual bool Equals(Primitive other)
        {
            return other is not null && Vertices.Equals(other.Vertices);
        }

        #endregion

        #region Collision Detection

        protected Primitive(VertexArray vertices)
        {
            Vertices = vertices;
            _boundingCircle = null;
        }

        protected Primitive(VertexArray vertices, BoundingCircle? boundingCircle, BoundingRectangle? boundingRectangle)
        {
            Vertices = vertices;
            _boundingCircle = boundingCircle;
            _boundingRectangle = boundingRectangle;
        }

        protected BoundingCircle? _boundingCircle;

        public BoundingCircle BoundingCircle =>
            _boundingCircle ?? (_boundingCircle = this.MinimumBoundingCircle()).Value;

        protected BoundingRectangle? _boundingRectangle;


        public void ResetCircle()
        {
            _boundingCircle = null;
        }

        public void ResetRectangle()
        {
            _boundingRectangle = null;
        }

        public BoundingRectangle BoundingRectangle =>
            _boundingRectangle ?? (_boundingRectangle = this.MinimumBoundingRectangle()).Value;

        public virtual bool CollidesWith(Primitive p)
        {
            return HybridCollisionDetector.CollisionDetector.CollidesWith(this, p);
        }

        #endregion

        #region Transformations

        public virtual Primitive Translate(Vector2 delta)
        {
            Vertices.Translate(delta);
            _boundingCircle = _boundingCircle?.Translate(delta);
            _boundingRectangle = _boundingRectangle?.Translate(delta);
            return this;
        }

        public virtual Primitive Rotate(float theta)
        {
            var center = BoundingCircle.Center;

            Vertices.Rotate(center, theta);
            _boundingRectangle = null;
            return this;
        }

        public virtual Primitive Rotate(Vector2 rotationCenter, float theta)
        {
            Vertices.Rotate(rotationCenter, theta);
            _boundingRectangle = _boundingRectangle?.Rotate(rotationCenter, theta);
            return this;
        }

        public virtual Primitive Scale(Vector2 delta)
        {
            var oldPosition = BoundingCircle.Center;
            Vertices.Scale(oldPosition, delta);
            Vertices.Translate(oldPosition * delta - oldPosition);

            _boundingCircle = null;
            _boundingRectangle = null;
            return this;
        }

        public virtual Primitive Skew(Vector2 delta)
        {
            Vertices.Skew(delta);
            _boundingCircle = null;
            _boundingRectangle = null;
            return this;
        }

        #endregion
    }
}