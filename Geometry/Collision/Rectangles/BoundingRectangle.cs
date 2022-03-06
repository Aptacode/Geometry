using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision.Rectangles;

public readonly struct BoundingRectangle : IEquatable<BoundingRectangle>
{
    #region Properties

    public Vector2 TopRight { get; init; }
    public Vector2 BottomLeft { get; init; }
    public Vector2 Size => new(TopRight.X - BottomLeft.X, TopRight.Y - BottomLeft.Y);
    public Vector2 Center => new(X + Width / 2, Y + Height / 2);
    public float X => BottomLeft.X;
    public float Y => BottomLeft.Y;
    public float Width => TopRight.X - BottomLeft.X;
    public float Height => TopRight.Y - BottomLeft.Y;

    #endregion


    #region Ctor

    public BoundingRectangle(Vector2 a, Vector2 b)
    {
        var minX = a.X;
        var maxX = b.X;
        var minY = a.Y;
        var maxY = b.Y;
        if (a.X > b.X)
        {
            minX = b.X;
            maxX = a.X;
        }

        if (a.Y > b.Y)
        {
            minY = b.Y;
            maxY = a.Y;
        }

        TopRight = new Vector2(maxX, maxY);
        BottomLeft = new Vector2(minX, minY);
    }

    public static readonly BoundingRectangle Zero = new()
    {
        BottomLeft = Vector2.Zero,
        TopRight = Vector2.One
    };

    #endregion

    #region Collision

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CollidesWith(BoundingRectangle rect)
    {
        return BottomLeft.X <= rect.TopRight.X &&
               TopRight.X >= rect.BottomLeft.X &&
               TopRight.Y >= rect.BottomLeft.Y &&
               BottomLeft.Y <= rect.TopRight.Y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CollidesWith(Vector2 point)
    {
        return BottomLeft.X <= point.X &&
               TopRight.Y >= point.Y &&
               TopRight.X >= point.X &&
               BottomLeft.Y <= point.Y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CollidesWith(Primitive primitive)
    {
        return CollidesWith(primitive.BoundingRectangle);
    }

    #endregion

    #region ToString

    public override string ToString()
    {
        return $"BoundingRectangle ({BottomLeft.X},{BottomLeft.Y}), ({TopRight.X},{TopRight.Y})";
    }

    #endregion

    #region IEquatable

    public override int GetHashCode()
    {
        return HashCode.Combine(TopRight, BottomLeft);
    }

    public override bool Equals(object other)
    {
        return other is BoundingRectangle b && Equals(b);
    }
    public bool Equals(BoundingRectangle other)
    {
        return TopRight.Equals(other.TopRight) && BottomLeft.Equals(other.BottomLeft);
    }

    public static bool operator ==(BoundingRectangle lhs, BoundingRectangle rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(BoundingRectangle lhs, BoundingRectangle rhs)
    {
        return !(lhs == rhs);
    }

    #endregion
}