using System.Numerics;

namespace Aptacode.Geometry.Collision.Rectangles;

public static class BoundingRectangleExtensions
{
    #region Transformation

    public static BoundingRectangle Translate(this BoundingRectangle rectangle, Vector2 delta)
    {
        return new BoundingRectangle
        {
            BottomLeft = rectangle.BottomLeft + delta,
            TopRight = rectangle.BottomLeft + delta
        };
    }

    #endregion
}