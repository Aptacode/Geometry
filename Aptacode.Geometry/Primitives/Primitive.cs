using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public abstract record Primitive
    {
        public readonly VertexArray Vertices;

        #region IEquatable

        public virtual bool Equals(Primitive other) => Vertices.Equals(other.Vertices);

        #endregion

        #region Collision Detection

        protected Primitive(VertexArray vertices)
        {
            Vertices = vertices;
            _boundingCircle = null;
        }

        protected Primitive(VertexArray vertices, BoundingCircle boundingCircle)
        {
            Vertices = vertices;
            _boundingCircle = boundingCircle;
        }

        private BoundingCircle? _boundingCircle;

        public BoundingCircle BoundingCircle =>
            _boundingCircle ?? (_boundingCircle = this.MinimumBoundingCircle()).Value;

        public virtual bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Transformations

        public abstract Primitive Translate(Vector2 delta);
        public abstract Primitive Rotate(float delta);
        public abstract Primitive Scale(Vector2 delta);
        public abstract Primitive Skew(Vector2 delta);

        #endregion
    }
}