using System.Numerics;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Primitives
{
    public abstract class Primitive
    {
        #region Properties

        public Vector2[] Vertices;

        #endregion

        #region Construction

        protected Primitive(params Vector2[] vertices)
        {
            Vertices = vertices;
        }

        #endregion

        #region Collision Detection

        public abstract Vector2 GetCenter();
        public abstract float GetRadius();

        public virtual bool CollidesWith(Primitive p, CollisionDetector detector)
        {
            return detector.CollidesWith(this, p);
        }
        public abstract bool CollidesWith(Point p, CollisionDetector detector);
        public abstract bool CollidesWith(Polygon p, CollisionDetector detector);
        public abstract bool CollidesWith(PolyLine p, CollisionDetector detector);
        public abstract bool CollidesWith(Circle p, CollisionDetector detector);

        #endregion

        #region Transformations

        public virtual void Translate(Vector2 delta)
        {
            for (var i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] += delta;
            }
        }

        public abstract void Rotate(Vector2 delta);
        public abstract void Scale(Vector2 delta);
        public abstract void Skew(Vector2 delta);

        #endregion
    }
}