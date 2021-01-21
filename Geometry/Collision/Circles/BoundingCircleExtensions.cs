using System;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision.Circles
{
    public static class BoundingCircleExtensions
    {
        public static bool Contains(this BoundingCircle circle, Vector2 point)
        {
            return (point - circle.Center).Length() <= circle.Radius;
        }

        public static BoundingCircle Translate(this BoundingCircle boundingCircle, Vector2 delta)
        {
            return new(boundingCircle.Center + delta, boundingCircle.Radius);
        }

        public static BoundingCircle Scale(this BoundingCircle boundingCircle, float delta)
        {
            return new(boundingCircle.Center, boundingCircle.Radius * delta);
        }

        public static BoundingCircle Skew(this BoundingCircle boundingCircle, Vector2 delta)
        {
            return new(boundingCircle.Center + delta, boundingCircle.Radius);
        }

        public static BoundingCircle Rotate(this BoundingCircle boundingCircle, Vector2 rotationCenter, float theta)
        {
            var rotationMatrix = Matrix3x2.CreateRotation(theta, rotationCenter);
            return new BoundingCircle(Vector2.Transform(boundingCircle.Center, rotationMatrix), boundingCircle.Radius);
        }

        public static BoundingCircle MinimumBoundingCircle(this Primitive p)
        {
            return p switch
            {
                Point point => new BoundingCircle(point.Position, 0.0f),
                Ellipse ellipse => ellipse.Radii.X >= ellipse.Radii.Y
                    ? BoundingCircle.FromTwoPoints(ellipse.EllipseVertices[0],
                        ellipse.EllipseVertices[1])
                    : BoundingCircle.FromTwoPoints(ellipse.EllipseVertices[2],
                        ellipse.EllipseVertices[3]),
                _ => WelzlAlgorithm.Compute(new Span<Vector2>(p.Vertices.Vertices),
                    new Span<Vector2>(new[] {Vector2.Zero, Vector2.Zero, Vector2.Zero}), p.Vertices.Length, 0)
            };
        }
    }
}