using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Utilities;

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

    public bool CollidesWith(Point point) => CollidesWith(point.Position);

    public bool CollidesWith(PolyLine polyLine)
    {
        if (!polyLine.BoundingRectangle.CollidesWith(this)) return false;

        var topLeft = TopLeft;
        var topRight = TopRight;
        var bottomRight = BottomRight;
        var bottomLeft = BottomLeft;
        for (var i = 0; i < polyLine.LineSegments.Length; i++)
        {
            var lineSeg = polyLine.LineSegments[i];
            var lineAsVector = lineSeg.P2 - lineSeg.P1;
            var a = (topLeft - lineSeg.P1).VectorCross(lineAsVector);
            var b = (topRight - lineSeg.P1).VectorCross(lineAsVector);
            var c = (bottomRight - lineSeg.P1).VectorCross(lineAsVector);
            var d = (bottomLeft - lineSeg.P1).VectorCross(lineAsVector);
            if (a > 0 && b > 0 && c > 0 && d > 0 || a < 0 && b < 0 && c < 0 && d < 0) continue;

            return true;
        }

        return false;
    }


    public bool CollidesWith(Polygon polygon)
    {
        if (!polygon.BoundingRectangle.CollidesWith(this)) return false;

        var topLeft = TopLeft;
        var topRight = TopRight;
        var bottomRight = BottomRight;
        var bottomLeft = BottomLeft;
        for (var i = 0; i < polygon.Edges.Length; i++)
        {
            var edge = polygon.Edges[i];
            var lineAsVector = edge.P2 - edge.P1;
            var a = (topLeft - edge.P1).VectorCross(lineAsVector);
            var b = (topRight - edge.P1).VectorCross(lineAsVector);
            var c = (bottomRight - edge.P1).VectorCross(lineAsVector);
            var d = (bottomLeft - edge.P1).VectorCross(lineAsVector);
            if (a > 0 && b > 0 && c > 0 && d > 0 || a < 0 && b < 0 && c < 0 && d < 0) continue;

            return true;
        }

        return false;
    }

    public bool CollidesWith(Ellipse ellipse)
    {
        if (CollidesWith(ellipse.Position)
           ) //If the center of the ellipse is inside the Bounding rectangle then there is a collision.
            return true;

        var testDistance = ellipse.Position - Center;
        var testX = testDistance.X;
        var testY = testDistance.Y;
        var topEdge = (TopLeft, TopRight);
        var leftEdge = (TopLeft, BottomLeft);
        var rightEdge = (TopRight, BottomRight);
        var bottomEdge = (BottomLeft, BottomRight);
        var stdform = ellipse.StandardForm;

        //Otherwise using these values we check which of up to two edges (if any) collide with the ellipse.
        if (testX <= 0 && testY <= 0) //if it's in the top left quadrant
            return ellipse.CollidesWith(TopLeft) || ellipse.CollidesWith(TopRight) ||
                   ellipse.CollidesWith(BottomLeft) ||
                   leftEdge.LineSegmentEllipseIntersection(stdform) ||
                   topEdge.LineSegmentEllipseIntersection(stdform);

        if (testX > 0 && testY <= 0) //if it's in the top right quadrant
            return ellipse.CollidesWith(TopLeft) || ellipse.CollidesWith(TopRight) ||
                   ellipse.CollidesWith(BottomRight) ||
                   rightEdge.LineSegmentEllipseIntersection(stdform) ||
                   topEdge.LineSegmentEllipseIntersection(stdform);

        if (testX <= 0 && testY > 0) //if it's in the bottom left quadrant
            return ellipse.CollidesWith(TopLeft) || ellipse.CollidesWith(BottomLeft) ||
                   ellipse.CollidesWith(BottomRight) || leftEdge.LineSegmentEllipseIntersection(stdform) ||
                   bottomEdge.LineSegmentEllipseIntersection(stdform);

        if (testX > 0 && testY > 0) //if it's in the bottom right quadrant
            return ellipse.CollidesWith(BottomLeft) || ellipse.CollidesWith(TopRight) ||
                   ellipse.CollidesWith(BottomRight) || rightEdge.LineSegmentEllipseIntersection(stdform) ||
                   bottomEdge.LineSegmentEllipseIntersection(stdform);

        return false; //not sure we'll ever reach here
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