using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Point : Primitive
    {
        #region Properties

        public Vector2 Position => Vertices[0];

        #endregion

        #region IEquatable

        public virtual bool Equals(Point other)
        {
            if (other is null)
            {
                return false;
            }

            return Math.Abs(Position.X - other.Position.X) <= Constants.Tolerance && Math.Abs(Position.Y - other.Position.Y) <= Constants.Tolerance;
        }

        #endregion

        #region Collision Detection

        public override Primitive GetBoundingPrimitive(float margin)
        {
            var marginScale = new Vector2(margin, margin);
            return Polygon.Rectangle.FromTwoPoints(Position + marginScale, Position - marginScale);
        }

        public override bool CollidesWith(Vector2 p)
        {
            return Vector2CollisionDetector.CollidesWith(this, p);
        }

        public override bool CollidesWith(Point p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(Ellipse p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(PolyLine p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(BoundingRectangle p)
        {
            return p.CollidesWith(this);
        }

        public override bool CollidesWith(Polygon p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        #endregion

        #region Construction

        protected Point(VertexArray vertexArray, BoundingRectangle boundingRectangle) : base(vertexArray, boundingRectangle)
        {
        }

        public static Point Create(float x, float y)
        {
            return Create(new Vector2(x, y));
        }

        public static Point Create(Vector2 position)
        {
            return new(VertexArray.Create(position), new BoundingRectangle(position, position, position, position));
        }

        public static readonly Point Zero = Create(Vector2.Zero);
        public static readonly Point Unit = Create(Vector2.One);

        #endregion
    }
}