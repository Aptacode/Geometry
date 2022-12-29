using System;
using System.Numerics;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Primitives;

public sealed class Circle : Primitive
{
    #region Properties

    public float Radius { get; protected set; }
    public Vector2 Position { get; protected set; }
    public override Vector2 GetCentroid() => Position;

    #endregion

    #region Construction

    public Circle(Vector2 position, float radius) 
    {
        Position = position;
        Radius = radius;
    }
    public Circle(float x, float y, float radius)
    {
        Position = new Vector2(x,y);
        Radius = radius;
    }

    public static Circle Zero => new(Vector2.Zero, 0.0f);
    public static Circle Unit => new(Vector2.Zero, 1.0f);

    #endregion
    public override string ToString()
    {
        return $"Circle ({Position.X},{Position.Y}), {Radius}";
    }

    #region Collision Detection

    public override bool CollidesWith(Vector2 p)
    {
        return Vector2CollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(Point p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(p, this);
    }

    public override bool CollidesWith(Circle p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(PolyLine p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(p, this);
    }

    public override bool CollidesWith(Polygon p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(p, this);
    }

    #endregion

    #region Transformations

    public override Circle Translate(Vector2 delta)
    {
        Position += delta;
        return this;
    }

    public override Circle ScaleAboutCenter(Vector2 delta)
    {
        return this;
    }

    public override Circle Rotate(float theta)
    {
        return this;
    }

    public override Circle Rotate(Vector2 rotationCenter, float theta)
    {
        return this;
    }

    public override Circle Skew(Vector2 delta)
    {
        return this;
    }

    public override Circle Scale(Vector2 scaleCenter, Vector2 delta)
    {

        return this;
    }


    #endregion

    public override Circle Transform(Matrix3x2 matrix)
    {
        Position = Vector2.Transform(Position, matrix);
        return this;
    }

    public override Circle Copy()
    {
        return new Circle(Position, Radius);
    }

    public void CopyTo(Circle destination)
    {
        destination.Position = Position;
        destination.Radius = Radius;
    }

    public override bool AreEqual(Primitive other)
    {
        if (other == null)
        {
            return false;
        }

        if (other is not Circle otherEllipse)
        {
            return false;
        }

        if (Math.Abs(Radius - otherEllipse.Radius) > Constants.Tolerance)
        {
            return false;
        }

        var delta = Position - otherEllipse.Position;
        return Math.Abs(delta.X + delta.Y) < Constants.Tolerance;
    }
}