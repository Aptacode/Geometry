using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives.Polygons;
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

        protected Primitive(VertexArray vertices)
        {
            Vertices = vertices;
        }

        protected Primitive(VertexArray vertices, BoundingRectangle? boundingRectangle)
        {
            Vertices = vertices;
            _boundingRectangle = boundingRectangle;
        }

        #region Collision Detection

        protected BoundingRectangle? _boundingRectangle;

        public void ResetRectangle()
        {
            _boundingRectangle = null;
        }

        public BoundingRectangle BoundingRectangle =>
            _boundingRectangle ?? (_boundingRectangle = MinimumBoundingRectangle()).Value;

        public abstract BoundingRectangle MinimumBoundingRectangle();

        public abstract bool CollidesWith(Vector2 p);
        public abstract bool CollidesWith(Point p);
        public abstract bool CollidesWith(Ellipse p);
        public abstract bool CollidesWith(PolyLine p);
        public abstract bool CollidesWith(Rectangle p);
        public abstract bool CollidesWith(Polygon p);
        public virtual bool CollidesWithPrimitive(Primitive p)
        {
            return p switch
            {
                Point point => CollidesWith(point),
                Ellipse ellipse => CollidesWith(ellipse),
                PolyLine polyline => CollidesWith(polyline),
                Rectangle rectangle => CollidesWith(rectangle),
                Polygon polygon => CollidesWith(polygon),
                _ => false
            };
        }
        
        public bool HybridCollidesWith(Vector2 p) => BoundingRectangle.Contains(p) && CollidesWith(p);
        public bool HybridCollidesWith(Point p) => p.BoundingRectangle.CollidesWith(BoundingRectangle) && CollidesWith(p);
        public bool HybridCollidesWith(Ellipse p) => p.BoundingRectangle.CollidesWith(BoundingRectangle) && CollidesWith(p);
        public bool HybridCollidesWith(PolyLine p) => p.BoundingRectangle.CollidesWith(BoundingRectangle) && CollidesWith(p);
        public bool HybridCollidesWith(Rectangle p) => p.BoundingRectangle.CollidesWith(BoundingRectangle) && CollidesWith(p);
        public bool HybridCollidesWith(Polygon p) => p.BoundingRectangle.CollidesWith(BoundingRectangle) && CollidesWith(p);
        public bool HybridCollidesWithPrimitive(Primitive p)
        {
            return p.BoundingRectangle.CollidesWith(BoundingRectangle) && CollidesWithPrimitive(p);
        }
        
        #endregion

        #region Transformations

        public virtual Primitive Translate(Vector2 delta)
        {
            Vertices.Translate(delta);
            _boundingRectangle = _boundingRectangle?.Translate(delta);
            return this;
        }

        public virtual Primitive Rotate(float theta)
        {
            var center = BoundingRectangle.Center;

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
            var oldPosition = BoundingRectangle.Center;
            Vertices.Scale(oldPosition, delta);
            Vertices.Translate(oldPosition * delta - oldPosition);

            _boundingRectangle = null;
            return this;
        }

        public virtual Primitive Skew(Vector2 delta)
        {
            Vertices.Skew(delta);
            _boundingRectangle = null;
            return this;
        }

        #endregion
    }
}