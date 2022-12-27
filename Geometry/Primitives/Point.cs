using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class Point : Primitive
{
    #region Properties

    public Vector2 Position { get; set; }

    #endregion

    public override string ToString()
    {
        return $"Point ({Position.X},{Position.Y})";
    }

    #region IEquatable

    public override bool Equals(Primitive? other)
    {
        if (other == null || other is not Point otherPoint)
        {
            return false;
        }

        return Math.Abs(Position.X - otherPoint.Position.X) < Constants.Tolerance &&
               Math.Abs(Position.Y - otherPoint.Position.Y) < Constants.Tolerance;
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    #endregion

    #region Collision Detection

    public override bool CollidesWith(Vector2 p)
    {
        return Vector2CollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(Point p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(Ellipse p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(PolyLine p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(BoundingRectangle p)
    {
        return p.CollidesWith(this);
    }

    public override bool CollidesWith(Polygon p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    #endregion

    #region Construction

    private Point(Vector2 position, BoundingRectangle boundingRectangle) : base(boundingRectangle)
    {
        Position = position;
    }

    public static Point Create(float x, float y)
    {
        return Create(new Vector2(x, y));
    }

    public static Point Create(Vector2 position)
    {
        return new Point(position, new BoundingRectangle
        {
            BottomLeft = position,
            TopRight = position
        });
    }

    public override Primitive Translate(Vector2 delta)
    {
        Position += delta;
        BoundingRectangle = BoundingRectangle.Translate(delta);
        return this;
    }

    public override Primitive Rotate(float theta)
    {
        return this;
    }

    public override Primitive Rotate(Vector2 rotationCenter, float theta)
    {
        return this;
    }

    public override Primitive ScaleAboutCenter(Vector2 delta)
    {
        return this;
    }

    public override Primitive Scale(Vector2 scaleCenter, Vector2 delta)
    {
        return this;
    }

    public override Primitive Skew(Vector2 delta)
    {
        return this;
    }


    public static Point Zero => Create(Vector2.Zero);
    public static Point Unit => Create(Vector2.One);

    #endregion
}