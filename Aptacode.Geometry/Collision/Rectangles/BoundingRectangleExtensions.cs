using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

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
            rectangle.BottomRight.Y >= point.X;

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
                Ellipse ellipse => BoundingRectangle.FromTwoPoints(ellipse.Position - ellipse.Radii,
                    ellipse.Position + ellipse.Radii),
                Rectangle rectangle => new BoundingRectangle(rectangle.TopLeft, rectangle.TopRight,
                    rectangle.BottomRight, rectangle.BottomLeft),
                _ => BoundingRectangle.FromVertexArray(p.Vertices)
            };
        }
    }
}