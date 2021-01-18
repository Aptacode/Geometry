using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public record Rectangle : Polygon
    {
        #region Construction

        public Rectangle(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft) : base(
            VertexArray.Create(topLeft, topRight, bottomRight, bottomLeft))
        {
        }

        protected Rectangle(VertexArray vertices)
            : base(vertices)
        {
        }

        public static readonly Rectangle Zero = Create(Vector2.Zero, Vector2.Zero);

        public static Rectangle Create(Vector2 position, Vector2 size)
        {
            return new(position,
                new Vector2(position.X + size.X, position.Y),
                position + size,
                new Vector2(position.X, position.Y + size.Y));
        }

        public static Rectangle Create(float x, float y, float width, float height)
        {
            return new(new Vector2(x, y),
                new Vector2(x + width, y),
                new Vector2(x + width, y + height),
                new Vector2(x, y + height));
        }

        #endregion

        #region Properties

        public Vector2 Position
        {
            get => Vertices[0];
            set => Vertices[0] = value;
        }

        public Vector2 Size => BottomRight - TopLeft;
        public float Width => Size.X;
        public float Height => Size.Y;

        public Vector2 TopLeft
        {
            get => Vertices[0];
            set => Vertices[0] = value;
        }

        public Vector2 TopRight
        {
            get => Vertices[1];
            set => Vertices[1] = value;
        }

        public Vector2 BottomRight
        {
            get => Vertices[2];
            set => Vertices[2] = value;
        }

        public Vector2 BottomLeft
        {
            get => Vertices[3];
            set => Vertices[3] = value;
        }

        #endregion

        #region Collision Detection

        public override BoundingRectangle MinimumBoundingRectangle()
        {
            return new(TopLeft, TopRight, BottomRight, BottomLeft);
        }

        #endregion
    }
}