using System;
using System.Numerics;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Primitives;

public sealed class Point : Primitive
{
    #region Properties

    public Vector2 Position { get; set; }
    public override Vector2 GetCentroid()
    {
        return Position;
    }

    #endregion

    public override string ToString() => $"Point ({Position.X},{Position.Y})";
    #region IEquatable

    public override Point Transform(Matrix3x2 matrix)
    {
        Position = Vector2.Transform(Position, matrix);
        return this;
    }

    public override Point Copy() => new(Position);

    public override bool AreEqual(Primitive other)
    {
        if (other == null || other is not Point otherPoint)
        {
            return false;
        }

        return Math.Abs(Position.X - otherPoint.Position.X) < Constants.Tolerance &&
               Math.Abs(Position.Y - otherPoint.Position.Y) < Constants.Tolerance;
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

    public override bool CollidesWith(Circle p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(PolyLine p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(Polygon p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    #endregion

    #region Construction

    public Point(Vector2 position)
    {
        Position = position;
    }
    public Point(float x, float y)
    {
        Position = new Vector2(x,y);
    }

    public override Point Translate(Vector2 delta)
    {
        Position += delta;
        return this;
    }

    public override Point Rotate(float theta)
    {
        return this;
    }

    public override Point Rotate(Vector2 rotationCenter, float theta)
    {
        return this;
    }

    public override Point ScaleAboutCenter(Vector2 delta)
    {
        return this;
    }

    public override Point Scale(Vector2 scaleCenter, Vector2 delta)
    {
        return this;
    }

    public override Point Skew(Vector2 delta)
    {
        return this;
    }

    public static Point Zero => new(Vector2.Zero);
    public static Point Unit => new(Vector2.One);

    #endregion
}