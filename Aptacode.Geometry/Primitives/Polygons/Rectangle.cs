using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Vertices;
using Microsoft.VisualBasic;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public record Rectangle : Polygon
    {
        #region Construction

        public Rectangle(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft) : base(
            VertexArray.Create(topLeft, topRight, bottomRight, bottomLeft))
        {
            Size = BottomRight - TopLeft;
        }

        protected Rectangle(VertexArray vertices, BoundingCircle? boundingCircle)
            : base(vertices, boundingCircle) { }

        public Rectangle(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft,
            BoundingCircle? boundingCircle) : base(
            VertexArray.Create(topLeft, topRight, bottomRight, bottomLeft), boundingCircle)
        {
            Size = BottomRight - TopLeft;
        }

        public static readonly Rectangle Zero = Create(Vector2.Zero, Vector2.Zero);

        public static Rectangle Create(Vector2 position, Vector2 size) =>
            new(position,
                new Vector2(position.X + size.X, position.Y),
                position + size,
                new Vector2(position.X, position.Y + size.Y));

        public static Rectangle Create(float x, float y, float width, float height) =>
            new(new Vector2(x, y),
                new Vector2(x+ width, y),
                new Vector2(x + width, y + height),
                new Vector2(x, y + height));

        #endregion

        #region Properties

        public Vector2 Position => TopLeft;
        public Vector2 Size { get; init; }
        public float Width => Size.X;
        public float Height => Size.Y;
        public Vector2 TopLeft => Vertices[0];
        public Vector2 TopRight => Vertices[1];
        public Vector2 BottomRight => Vertices[2];
        public Vector2 BottomLeft => Vertices[3];

        #endregion

        #region Transformations

        public override Rectangle Translate(Vector2 delta) =>
            new(Vertices.Translate(delta),
                _boundingCircle?.Translate(delta));

        public override Rectangle Rotate(Vector2 rotationCenter, float theta) =>
            new(Vertices.Rotate(rotationCenter, theta), _boundingCircle?.Rotate(rotationCenter, theta));

        public override Rectangle Rotate(float theta) =>
            new(Vertices.Rotate(BoundingCircle.Center, theta), _boundingCircle);

        public override Rectangle Scale(Vector2 delta) => new(Vertices.Scale(BoundingCircle.Center, delta), null);

        public override Rectangle Skew(Vector2 delta) => new(Vertices.Skew(delta), null);

        #endregion
    }
}