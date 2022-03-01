using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class Point : Primitive
{
    #region Properties

    public Vector2 Position => Vertices[0];

    #endregion

    #region IEquatable

    public override bool Equals(object other)
    {
        if (other is not Point otherPoint) return false;

        return Math.Abs(Position.X - otherPoint.Position.X) < Constants.Tolerance &&
               Math.Abs(Position.Y - otherPoint.Position.Y) < Constants.Tolerance;
    }

    #endregion

    public override string ToString()
    {
        return $"Point {Vertices}";
    }

    #region Collision Detection

    public override Primitive GetBoundingPrimitive(float margin)
    {
        var marginScale = new Vector2(margin, margin);
        return Polygon.Rectangle.FromTwoPoints(Position + marginScale, Position - marginScale);
    }

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

    private Point(VertexArray vertexArray, BoundingRectangle boundingRectangle) : base(vertexArray, boundingRectangle)
    {
    }

    public static Point Create(float x, float y)
    {
        return Create(new Vector2(x, y));
    }

    public static Point Create(Vector2 position)
    {
        return new Point(VertexArray.Create(position), new BoundingRectangle(position, position));
    }

    public static Point Zero => Create(Vector2.Zero);
    public static Point Unit => Create(Vector2.One);

    #endregion
}