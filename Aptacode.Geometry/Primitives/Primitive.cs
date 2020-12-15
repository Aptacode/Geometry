using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;

namespace Aptacode.Geometry.Primitives
{
    public abstract record Primitive(IEnumerable<Vector2> Vertices)
    {
        #region Collision Detection

        public BoundingCircle UpdateBoundingCircle()
        {
            _boundingCircle = this.MinimumBoundingCircle();
            return _boundingCircle;
        }

        private BoundingCircle? _boundingCircle;

        public BoundingCircle BoundingCircle
        {
            get => _boundingCircle ?? UpdateBoundingCircle();
            set => _boundingCircle = value;
        }

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