using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Collision.Rectangles
{
    public static class BoundingRectangleExtensions
    {
        public static bool CollidesWith(this BoundingRectangle p1, BoundingRectangle p2)
        {
            var l1 = p1.TopLeft;
            var r1 = p1.BottomRight;
            var l2 = p2.TopLeft;
            var r2 = p2.BottomRight;

            // If one rectangle is on left side of other 
            if (l1.X > r2.X || l2.X > r1.X)
            {
                return false;
            }

            // If one rectangle is above other 
            if (l1.Y > r2.Y || l2.Y > r1.Y)
            {
                return false;
            }

            return true;
        }

        public static bool Contains(this BoundingRectangle rectangle, Vector2 point) =>
            rectangle.TopLeft.X <= point.X &&
            rectangle.TopLeft.Y <= point.Y &&
            rectangle.BottomRight.X >= point.X &&
            rectangle.BottomRight.Y >= point.Y;

        public static BoundingRectangle Translate(this BoundingRectangle rectangle, Vector2 delta) =>
            new(
                rectangle.TopLeft + delta,
                rectangle.TopRight + delta,
                rectangle.BottomRight + delta,
                rectangle.BottomLeft + delta);

        public static BoundingRectangle Scale(this BoundingRectangle rectangle, Vector2 center, Vector2 delta) =>
            new(
                rectangle.TopLeft + delta,
                rectangle.TopRight + delta,
                rectangle.BottomRight + delta,
                rectangle.BottomLeft + delta);

        public static BoundingRectangle Skew(this BoundingRectangle rectangle, Vector2 delta) =>
            new(
                rectangle.TopLeft + delta,
                rectangle.TopRight + delta,
                rectangle.BottomRight + delta,
                rectangle.BottomLeft + delta);

        public static BoundingRectangle Rotate(this BoundingRectangle rectangle, Vector2 rotationCenter, float theta)
        {
            var rotationMatrix = Matrix3x2.CreateRotation(theta, rotationCenter);
            return new BoundingRectangle(
                Vector2.Transform(rectangle.TopLeft, rotationMatrix),
                Vector2.Transform(rectangle.TopRight, rotationMatrix),
                Vector2.Transform(rectangle.BottomRight, rotationMatrix),
                Vector2.Transform(rectangle.BottomLeft, rotationMatrix));
        }

        public static BoundingRectangle MinimumBoundingRectangle(this Primitive p)
        {
            return p switch
            {
                Point point => new BoundingRectangle(point.Position, point.Position, point.Position, point.Position),
                Ellipse ellipse => BoundingRectangle.FromVertexArray(ellipse.EllipseExtrema),
                Rectangle rectangle => new BoundingRectangle(rectangle.TopLeft, rectangle.TopRight,
                    rectangle.BottomRight, rectangle.BottomLeft),
                _ => p.Vertices.ToBoundingRectangle()
            };
        }

        #region Creation

        public static BoundingRectangle AddMargin(this BoundingRectangle boundingRectangle, float margin)
        {
            return BoundingRectangle.FromTwoPoints(
                new Vector2(boundingRectangle.TopLeft.X - margin, boundingRectangle.TopLeft.Y - margin), 
                new Vector2(boundingRectangle.BottomRight.X + margin, boundingRectangle.BottomRight.Y + margin));
        }

        public static BoundingRectangle ToBoundingRectangle(this IEnumerable<Primitive> primitives)
        {
            var maxX = 0.0f;
            var maxY = 0.0f;
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            foreach (var primitive in primitives)
            {
                var boundingRectangle = primitive.BoundingRectangle;
                if (boundingRectangle.TopLeft.X < minX)
                {
                    minX = boundingRectangle.TopLeft.X;
                }

                if (boundingRectangle.TopLeft.Y < minY)
                {
                    minY = boundingRectangle.TopLeft.Y;
                }

                if (boundingRectangle.BottomRight.X > maxX)
                {
                    maxX = boundingRectangle.BottomRight.X;
                }

                if (boundingRectangle.BottomRight.Y > maxY)
                {
                    maxY = boundingRectangle.BottomRight.Y;
                }
            }

            return BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        public static BoundingRectangle ToBoundingRectangle(this VertexArray vertexArray)
        {
            var maxX = 0.0f;
            var maxY = 0.0f;
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            for (var i = 0; i < vertexArray.Length; i++)
            {
                var vertex = vertexArray[i];
                if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }

                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }
            }

            return BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        #endregion
    }
}