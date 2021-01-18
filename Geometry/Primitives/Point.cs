using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives.Polygons;
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
            return (Math.Abs(Position.X - other.Position.X) <= Constants.Tolerance && Math.Abs(Position.Y - other.Position.Y) <= Constants.Tolerance);
        }

        #endregion

        #region Collision Detection
        public override BoundingRectangle MinimumBoundingRectangle()
        {
            return new(Position, Position, Position, Position);
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

        public override bool CollidesWith(Rectangle p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(Polygon p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        #endregion

        #region Ctor

        public Point(Vector2 position) : base(VertexArray.Create(position))
        {
        }

        protected Point(VertexArray vertexArray) : base(vertexArray)
        {
        }

        public static Point Create(float x, float y)
        {
            return new(new Vector2(x, y));
        }

        #endregion

        #region Construction

        public static readonly Point Zero = new(Vector2.Zero);
        public static readonly Point Unit = new(Vector2.One);

        #endregion
    }
}