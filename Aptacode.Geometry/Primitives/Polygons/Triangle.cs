using System.Numerics;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public class Triangle : Polygon
    {
        #region Collision Detection

        public sealed override void UpdateBoundingCircle()
        {
            var (p1, p2) = FurthestPoints();
            Center = (p1 + p2) * 0.5f;
            Radius = (p1 - p2).Length() / 2.0f;
        }

        #endregion

        #region Construction

        public static Triangle Create(Vector2 p1, Vector2 p2, Vector2 p3) => new Triangle(p1, p2, p3);

        public Triangle(Vector2 p1, Vector2 p2, Vector2 p3) : base(p1, p2, p3)
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
    }
}