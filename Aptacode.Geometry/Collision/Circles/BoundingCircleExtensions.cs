using System.Numerics;

namespace Aptacode.Geometry.Collision.Circles
{
    public static class BoundingCircleExtensions
    {
        public static bool Contains(this BoundingCircle circle, Vector2 point) =>
            (point - circle.Center).Length() <= circle.Radius;

        public static BoundingCircle Translate(this BoundingCircle boundingCircle, Vector2 delta) =>
            new(boundingCircle.Center + delta, boundingCircle.Radius);

        public static BoundingCircle Rotate(this BoundingCircle boundingCircle, Vector2 rotationCenter, float theta)
        {
            var rotationMatrix = Matrix3x2.CreateRotation(theta, rotationCenter);
            return new BoundingCircle(Vector2.Transform(boundingCircle.Center, rotationMatrix), boundingCircle.Radius);
        }
    }
}