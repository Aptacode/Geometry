using System.Numerics;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public record Rectangle(Vector2 TopLeft, Vector2 TopRight, Vector2 BottomRight, Vector2 BottomLeft) : Polygon(new[]
    {
        TopLeft, TopRight, BottomRight, BottomLeft
    })
    {
        #region Construction

        public static Rectangle Create(Vector2 position, Vector2 size) =>
            new(position,
                new Vector2(position.X + size.X, position.Y + size.Y),
                position + size,
                new Vector2(position.X, position.Y + size.Y));

        #endregion

        #region Properties
        
        public Vector2 Position => Vertices[0];
        public Vector2 Size => Vertices[2] - Vertices[0];

        #endregion
    }
}