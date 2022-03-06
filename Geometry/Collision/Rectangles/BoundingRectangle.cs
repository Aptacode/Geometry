using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision.Rectangles;

public readonly struct BoundingRectangle
{
    #region Properties

    public readonly Vector2 TopRight;
    public readonly Vector2 BottomLeft;

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

    public static readonly BoundingRectangle Zero = new(Vector2.Zero, Vector2.Zero);

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

    public Primitive GetBoundingPrimitive(float margin)
    {
        var marginDelta = new Vector2(margin, margin);
        return Polygon.Rectangle.FromTwoPoints(BottomLeft - marginDelta, TopRight + marginDelta);
    }

    public override string ToString()
    {
        return $"BoundingRectangle ({BottomLeft.X},{BottomLeft.Y}), ({TopRight.X},{TopRight.Y})";
    }

    #region IEquatable

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public override bool Equals(object other)
    {
        return other is BoundingRectangle otherBoundingRectangle &&
               TopRight == otherBoundingRectangle.TopRight &&
               BottomLeft == otherBoundingRectangle.BottomLeft;
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