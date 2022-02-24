using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Collision.Rectangles;

public static class BoundingRectangleExtensions
{
    #region Transformation

    public static BoundingRectangle Translate(this BoundingRectangle rectangle, Vector2 delta)
    {
        return new BoundingRectangle(
            rectangle.TopLeft + delta,
            rectangle.TopRight + delta,
            rectangle.BottomRight + delta,
            rectangle.BottomLeft + delta);
    }

    public static BoundingRectangle Scale(this BoundingRectangle rectangle, Vector2 center, Vector2 delta)
    {
        return new BoundingRectangle(
            rectangle.TopLeft + delta,
            rectangle.TopRight + delta,
            rectangle.BottomRight + delta,
            rectangle.BottomLeft + delta);
    }

    public static BoundingRectangle Skew(this BoundingRectangle rectangle, Vector2 delta)
    {
        return new BoundingRectangle(
            rectangle.TopLeft + delta,
            rectangle.TopRight + delta,
            rectangle.BottomRight + delta,
            rectangle.BottomLeft + delta);
    }

    public static BoundingRectangle Rotate(this BoundingRectangle rectangle, Vector2 rotationCenter, float theta)
    {
        var rotationMatrix = Matrix3x2.CreateRotation(theta, rotationCenter);
        return new BoundingRectangle(
            Vector2.Transform(rectangle.TopLeft, rotationMatrix),
            Vector2.Transform(rectangle.TopRight, rotationMatrix),
            Vector2.Transform(rectangle.BottomRight, rotationMatrix),
            Vector2.Transform(rectangle.BottomLeft, rotationMatrix));
    }

    #endregion

    #region Creation

    public static BoundingRectangle ToBoundingRectangle(this IEnumerable<Primitive> primitives)
    {
        var maxX = 0.0f;
        var maxY = 0.0f;
        var minX = float.MaxValue;
        var minY = float.MaxValue;
        foreach (var primitive in primitives)
        {
            var boundingRectangle = primitive.BoundingRectangle;
            if (boundingRectangle.TopLeft.X < minX) minX = boundingRectangle.TopLeft.X;

            if (boundingRectangle.TopLeft.Y < minY) minY = boundingRectangle.TopLeft.Y;

            if (boundingRectangle.BottomRight.X > maxX) maxX = boundingRectangle.BottomRight.X;

            if (boundingRectangle.BottomRight.Y > maxY) maxY = boundingRectangle.BottomRight.Y;
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
            if (vertex.X > maxX) maxX = vertex.X;

            if (vertex.Y > maxY) maxY = vertex.Y;

            if (vertex.X < minX) minX = vertex.X;

            if (vertex.Y < minY) minY = vertex.Y;
        }

        return BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
    }

    #endregion
}