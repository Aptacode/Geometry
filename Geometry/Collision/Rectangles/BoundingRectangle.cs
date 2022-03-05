using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision.Rectangles;

public readonly struct BoundingRectangle
{
    #region Properties

    public readonly Vector2 TopLeft;
    public readonly Vector2 TopRight;
    public readonly Vector2 BottomRight;
    public readonly Vector2 BottomLeft;
    public Vector2 Size => new(BottomRight.X - TopLeft.X, TopLeft.Y - BottomRight.Y);
    public Vector2 Center => new(X + Width / 2, Y + Height / 2);
    public float X => BottomLeft.X;
    public float Y => BottomLeft.Y;
    public float Width => BottomRight.X - TopLeft.X;
    public float Height => TopLeft.Y - BottomRight.Y;

    #endregion

    #region Ctor

    public BoundingRectangle(Vector2 a, Vector2 b)
    {
        var minX = a.X <= b.X ? a.X : b.X;
        var maxX = a.X >= b.X ? a.X : b.X;
        var minY = a.Y <= b.Y ? a.Y : b.Y;
        var maxY = a.Y >= b.Y ? a.Y : b.Y;

        TopLeft = new Vector2(minX, maxY);
        TopRight = new Vector2(maxX, maxY);
        BottomRight = new Vector2(maxX, minY);
        BottomLeft = new Vector2(minX, minY);
    }

    public static readonly BoundingRectangle Zero = new(Vector2.Zero, Vector2.Zero);

    #endregion

    #region Collision

    public bool CollidesWith(BoundingRectangle rect)
    {
        return TopLeft.X <= rect.TopRight.X &&
               TopRight.X >= rect.TopLeft.X &&
               TopLeft.Y >= rect.BottomRight.Y &&
               BottomRight.Y <= rect.TopLeft.Y;
    }


    public bool CollidesWith(Vector2 point)
    {
        return TopLeft.X <= point.X &&
               TopLeft.Y >= point.Y &&
               BottomRight.X >= point.X &&
               BottomRight.Y <= point.Y;
    }

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
        return
            $"BoundingRectangle ({TopLeft.X},{TopLeft.Y}), ({TopRight.X},{TopRight.Y}), ({BottomRight.X},{BottomRight.Y}), ({BottomLeft.X},{BottomLeft.Y})";
    }

    #region IEquatable

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public override bool Equals(object other)
    {
        return other is BoundingRectangle otherBoundingRectangle &&
               TopLeft == otherBoundingRectangle.TopLeft &&
               TopRight == otherBoundingRectangle.TopRight &&
               BottomRight == otherBoundingRectangle.BottomRight &&
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