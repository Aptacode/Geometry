using System.Numerics;

namespace Aptacode.Geometry.Collision.Circles
{
    public static class BoundingCircleExtensions
    {
        public static BoundingCircle Translate(this BoundingCircle boundingCircle, Vector2 delta) =>
            new(boundingCircle.Center + delta, boundingCircle.Radius);
        public static BoundingCircle Rotate(this BoundingCircle boundingCircle, Vector2 rotationCenter, float theta) =>
            new(Vector2.Transform(boundingCircle.Center, Matrix3x2.CreateRotation(theta, rotationCenter)), boundingCircle.Radius);
        public static bool Contains(this BoundingCircle circle, Vector2 point) =>
            (point - circle.Center).Length() <= circle.Radius;
    }
}