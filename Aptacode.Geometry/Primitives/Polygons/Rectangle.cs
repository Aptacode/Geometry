using System.Numerics;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public class Rectangle : Polygon
    {
        #region Construction

        public static Rectangle Create(Vector2 position, Vector2 size) =>
            new Rectangle(position,
                new Vector2(position.X + size.X, position.Y + size.Y),
                position + size,
                new Vector2(position.X, position.Y + size.Y));

        public Rectangle(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight) : base(topLeft,
            topRight, bottomLeft, bottomRight)
        {
            UpdateBoundingCircle();
        }

        #endregion

        #region Properties

        public Vector2 TopLeft => Vertices[0];
        public Vector2 TopRight => Vertices[1];
        public Vector2 BottomRight => Vertices[2];
        public Vector2 BottomLeft => Vertices[3];

        public Vector2 Position => Vertices[0];
        public Vector2 Size => Vertices[2] - Vertices[0];

        #endregion

        #region Collision Detection

        public sealed override void UpdateBoundingCircle()
        {
            var (p1, p2) = FurthestPoints();
            Center = (p1 + p2) * 0.5f;
            Radius = (p1 - p2).Length() / 2.0f;
        }

        #endregion


    }
}