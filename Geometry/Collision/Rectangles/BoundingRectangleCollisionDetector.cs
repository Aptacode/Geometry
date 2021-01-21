using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;
using Aptacode.Geometry.Utilities;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Collision.Rectangles
{
    public static class BoundingRectangleCollisionDetector
    {
        #region Collision

        public static bool CollidesWith(this BoundingRectangle p1, BoundingRectangle p2)
        {
            // If one rectangle is on left side of other 
            if (p1.TopLeft.X > p2.BottomRight.X || p2.TopLeft.X > p1.BottomRight.X)
            {
                return false;
            }

            // If one rectangle is above other 
            if (p1.TopLeft.Y > p2.BottomRight.Y || p2.TopLeft.Y > p1.BottomRight.Y)
            {
                return false;
            }

            return true;
        }

        public static bool Contains(this BoundingRectangle rectangle, Vector2 point)
        {
            return rectangle.TopLeft.X <= point.X &&
                   rectangle.TopLeft.Y <= point.Y &&
                   rectangle.BottomRight.X >= point.X &&
                   rectangle.BottomRight.Y >= point.Y;
        }

        public static bool CollidesWith(this BoundingRectangle rectangle, Point point)
        {
            return rectangle.TopLeft.X <= point.Position.X &&
                   rectangle.TopLeft.Y <= point.Position.Y &&
                   rectangle.BottomRight.X >= point.Position.X &&
                   rectangle.BottomRight.Y >= point.Position.Y;
        }

        public static bool CollidesWith(this BoundingRectangle rectangle, PolyLine polyLine)
        {
            var topLeft = rectangle.TopLeft;
            var topRight = rectangle.TopRight;
            var bottomRight = rectangle.BottomRight;
            var bottomLeft = rectangle.BottomLeft;
            for (var i = 0; i < polyLine.LineSegments.Length; i++)
            {
                var lineSeg = polyLine.LineSegments[i];
                var lineAsVector = lineSeg.Item2 - lineSeg.Item1;
                var a = (topLeft - lineSeg.Item1).VectorCross(lineAsVector);
                var b = (topRight - lineSeg.Item1).VectorCross(lineAsVector);
                var c = (bottomRight - lineSeg.Item1).VectorCross(lineAsVector);
                var d = (bottomLeft - lineSeg.Item2).VectorCross(lineAsVector);
                if ((a > 0 && b > 0 && c > 0 && d > 0) || (a < 0 && b < 0 && c < 0 && d < 0))
                {
                    continue;
                }
                return true;
            }
            return false;
        }


        public static bool CollidesWith(this BoundingRectangle rectangle, Polygon polygon)
        {
            var topLeft = rectangle.TopLeft;
            var topRight = rectangle.TopRight;
            var bottomRight = rectangle.BottomRight;
            var bottomLeft = rectangle.BottomLeft;
            for (var i = 0; i < polygon.Edges.Length; i++)
            {
                var edge = polygon.Edges[i];
                var lineAsVector = edge.Item2 - edge.Item1;
                var a = (topLeft - edge.Item1).VectorCross(lineAsVector);
                var b = (topRight - edge.Item1).VectorCross(lineAsVector);
                var c = (bottomRight - edge.Item1).VectorCross(lineAsVector);
                var d = (bottomLeft - edge.Item2).VectorCross(lineAsVector);
                if ((a > 0 && b > 0 && c > 0 && d > 0) || (a < 0 && b < 0 && c < 0 && d < 0))
                {
                    continue;
                }
                return true;
            }
            return false;
        }

        public static bool CollidesWith(this BoundingRectangle rectangle, Ellipse ellipse)
        {
            if (rectangle.CollidesWith(ellipse.Position)
            ) //If the center of the ellipse is inside the Bounding rectangle then there is a collision.
            {
                return true;
            }

            var testDistance = ellipse.Position - rectangle.Center;
            var testX = testDistance.X;
            var testY = testDistance.Y;
            var topEdge = (rectangle.TopLeft, rectangle.TopRight);
            var leftEdge = (rectangle.TopLeft, rectangle.BottomLeft);
            var rightEdge = (rectangle.TopRight, rectangle.BottomRight);
            var bottomEdge = (rectangle.BottomLeft, rectangle.BottomRight);
            var stdform = ellipse.GetStandardForm();

            //Otherwise using these values we check which of up to two edges (if any) collide with the ellipse.
            if (testX <= 0 && testY <= 0) //if it's in the top left quadrant
            {
                if (ellipse.CollidesWith(rectangle.TopLeft) || ellipse.CollidesWith(rectangle.TopRight) || ellipse.CollidesWith(rectangle.BottomLeft) || Helpers.LineSegmentEllipseIntersection(leftEdge, stdform) || Helpers.LineSegmentEllipseIntersection(topEdge, stdform))
                {
                    return true;
                }
                return false;
            }

            if (testX > 0 && testY <= 0) //if it's in the top right quadrant
            {
                if (ellipse.CollidesWith(rectangle.TopLeft) || ellipse.CollidesWith(rectangle.TopRight) || ellipse.CollidesWith(rectangle.BottomRight) || Helpers.LineSegmentEllipseIntersection(rightEdge, stdform) || Helpers.LineSegmentEllipseIntersection(topEdge, stdform))
                {
                    return true;
                }
                return false;
            }
            if (testX <= 0 && testY > 0) //if it's in the bottom left quadrant
            {
                if (ellipse.CollidesWith(rectangle.TopLeft) || ellipse.CollidesWith(rectangle.BottomLeft) || ellipse.CollidesWith(rectangle.BottomRight) || Helpers.LineSegmentEllipseIntersection(leftEdge, stdform) || Helpers.LineSegmentEllipseIntersection(bottomEdge, stdform))
                {
                    return true;
                }
                return false;
            }
            if (testX > 0 && testY > 0) //if it's in the bottom right quadrant
            {
                if (ellipse.CollidesWith(rectangle.BottomLeft) || ellipse.CollidesWith(rectangle.TopRight) || ellipse.CollidesWith(rectangle.BottomRight) || Helpers.LineSegmentEllipseIntersection(rightEdge, stdform) || Helpers.LineSegmentEllipseIntersection(bottomEdge, stdform))
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