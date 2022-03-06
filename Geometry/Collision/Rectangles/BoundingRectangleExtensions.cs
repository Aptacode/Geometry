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
            rectangle.BottomLeft + delta,
            rectangle.TopRight + delta);
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
            if (boundingRectangle.BottomLeft.X < minX) minX = boundingRectangle.BottomLeft.X;

            if (boundingRectangle.TopRight.Y < minY) minY = boundingRectangle.TopRight.Y;

            if (boundingRectangle.TopRight.X > maxX) maxX = boundingRectangle.TopRight.X;

            if (boundingRectangle.BottomLeft.Y > maxY) maxY = boundingRectangle.BottomLeft.Y;
        }

        return new BoundingRectangle(new Vector2(minX, minY), new Vector2(maxX, maxY));
    }

    #endregion
}