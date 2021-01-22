using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;
using Aptacode.Geometry.Utilities;

namespace Aptacode.Geometry.Collision.Rectangles
{
    public readonly struct BoundingRectangle
    {
        #region Properties

        public readonly Vector2 TopLeft;
        public readonly Vector2 TopRight;
        public readonly Vector2 BottomRight;
        public readonly Vector2 BottomLeft;
        public readonly Vector2 Size;
        public readonly Vector2 Center;

        #endregion

        #region Ctor

        public BoundingRectangle(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
            Size = Vector2.Abs(BottomRight - TopLeft);
            Center = TopLeft + Size / 2.0f;
        }

        public static BoundingRectangle FromTwoPoints(Vector2 topLeft, Vector2 bottomRight)
        {
            var topRight = new Vector2(bottomRight.X, topLeft.Y);
            var bottomLeft = new Vector2(topLeft.X, bottomRight.Y);

            return new BoundingRectangle(topLeft, topRight, bottomRight, bottomLeft);
        }

        public static BoundingRectangle FromPositionAndSize(Vector2 topLeft, Vector2 size)
        {
            var bottomRight = topLeft + size;
            return FromTwoPoints(topLeft, bottomRight);
        }

        public static readonly BoundingRectangle Zero = FromTwoPoints(Vector2.Zero, Vector2.Zero);

        #endregion

        #region Collision

        public bool CollidesWith(BoundingRectangle p2)
        {
            // If one rectangle is on left side of other 
            if (TopLeft.X > p2.BottomRight.X || p2.TopLeft.X > BottomRight.X)
            {
                return false;
            }

            // If one rectangle is above other 
            if (TopLeft.Y > p2.BottomRight.Y || p2.TopLeft.Y > BottomRight.Y)
            {
                return false;
            }

            return true;
        }

        public bool CollidesWith(Vector2 point)
        {
            return TopLeft.X <= point.X &&
                   TopLeft.Y <= point.Y &&
                   BottomRight.X >= point.X &&
                   BottomRight.Y >= point.Y;
        }

        public bool CollidesWith(Point point)
        {
            return TopLeft.X <= point.Position.X &&
                   TopLeft.Y <= point.Position.Y &&
                   BottomRight.X >= point.Position.X &&
                   BottomRight.Y >= point.Position.Y;
        }

        public bool CollidesWith(PolyLine polyLine)
        {
            if (!polyLine.BoundingRectangle.CollidesWith(this))
            {
                return false;
            }

            var topLeft = TopLeft;
            var topRight = TopRight;
            var bottomRight = BottomRight;
            var bottomLeft = BottomLeft;
            for (var i = 0; i < polyLine.LineSegments.Length; i++)
            {
                var lineSeg = polyLine.LineSegments[i];
                var lineAsVector = lineSeg.Item2 - lineSeg.Item1;
                var a = (topLeft - lineSeg.Item1).VectorCross(lineAsVector);
                var b = (topRight - lineSeg.Item1).VectorCross(lineAsVector);
                var c = (bottomRight - lineSeg.Item1).VectorCross(lineAsVector);
                var d = (bottomLeft - lineSeg.Item1).VectorCross(lineAsVector);
                if (a > 0 && b > 0 && c > 0 && d > 0 || a < 0 && b < 0 && c < 0 && d < 0)
                {
                    continue;
                }

                return true;
            }

            return false;
        }


        public bool CollidesWith(Polygon polygon)
        {
            if (!polygon.BoundingRectangle.CollidesWith(this))
            {
                return false;
            }

            var topLeft = TopLeft;
            var topRight = TopRight;
            var bottomRight = BottomRight;
            var bottomLeft = BottomLeft;
            for (var i = 0; i < polygon.Edges.Length; i++)
            {
                var edge = polygon.Edges[i];
                var lineAsVector = edge.Item2 - edge.Item1;
                var a = (topLeft - edge.Item1).VectorCross(lineAsVector);
                var b = (topRight - edge.Item1).VectorCross(lineAsVector);
                var c = (bottomRight - edge.Item1).VectorCross(lineAsVector);
                var d = (bottomLeft - edge.Item1).VectorCross(lineAsVector);
                if (a > 0 && b > 0 && c > 0 && d > 0 || a < 0 && b < 0 && c < 0 && d < 0)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public bool CollidesWith(Ellipse ellipse)
        {
            if (CollidesWith(ellipse.Position)
            ) //If the center of the ellipse is inside the Bounding rectangle then there is a collision.
            {
                return true;
            }

            var testDistance = ellipse.Position - Center;
            var testX = testDistance.X;
            var testY = testDistance.Y;
            var topEdge = (TopLeft, TopRight);
            var leftEdge = (TopLeft, BottomLeft);
            var rightEdge = (TopRight, BottomRight);
            var bottomEdge = (BottomLeft, BottomRight);
            var stdform = ellipse.GetStandardForm();

            //Otherwise using these values we check which of up to two edges (if any) collide with the ellipse.
            if (testX <= 0 && testY <= 0) //if it's in the top left quadrant
            {
                if (ellipse.CollidesWith(TopLeft) || ellipse.CollidesWith(TopRight) || ellipse.CollidesWith(BottomLeft) || leftEdge.LineSegmentEllipseIntersection(stdform) || topEdge.LineSegmentEllipseIntersection(stdform))
                {
                    return true;
                }

                return false;
            }

            if (testX > 0 && testY <= 0) //if it's in the top right quadrant
            {
                if (ellipse.CollidesWith(TopLeft) || ellipse.CollidesWith(TopRight) || ellipse.CollidesWith(BottomRight) || rightEdge.LineSegmentEllipseIntersection(stdform) || topEdge.LineSegmentEllipseIntersection(stdform))
                {
                    return true;
                }

                return false;
            }

            if (testX <= 0 && testY > 0) //if it's in the bottom left quadrant
            {
                if (ellipse.CollidesWith(TopLeft) || ellipse.CollidesWith(BottomLeft) || ellipse.CollidesWith(BottomRight) || leftEdge.LineSegmentEllipseIntersection(stdform) || bottomEdge.LineSegmentEllipseIntersection(stdform))
                {
                    return true;
                }

                return false;
            }

            if (testX > 0 && testY > 0) //if it's in the bottom right quadrant
            {
                if (ellipse.CollidesWith(BottomLeft) || ellipse.CollidesWith(TopRight) || ellipse.CollidesWith(BottomRight) || rightEdge.LineSegmentEllipseIntersection(stdform) || bottomEdge.LineSegmentEllipseIntersection(stdform))
                {
                    return true;
                }

                return false;
            }

            return false; //not sure we'll ever reach here
        }

        #endregion
    }
}