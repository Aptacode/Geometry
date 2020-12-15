using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;

namespace Aptacode.Geometry.Primitives
{
    public abstract record Primitive
    {
        public Vector2[] Vertices { get; init; }
        
        protected Primitive(Vector2[] vertices)
        {
            Vertices = vertices;
            UpdateBoundingCircle();
        }
        
        #region Collision Detection

        public void UpdateBoundingCircle()
        {
            BoundingCircle = this.MinimumBoundingCircle();
        }

        public BoundingCircle BoundingCircle { get; protected set; }

        public virtual bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);
        
        #endregion

        #region Transformations

        public virtual void Translate(Vector2 delta)
        {
            for (var i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] += delta;
            }

            BoundingCircle = BoundingCircle.Translate(delta);
        }

        public abstract void Rotate(Vector2 delta);
        public abstract void Scale(Vector2 delta);
        public abstract void Skew(Vector2 delta);

        #endregion
    }
}