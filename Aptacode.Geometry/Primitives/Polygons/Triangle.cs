using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public record Triangle : Polygon
    {
        #region Construction

        public Triangle(Vector2 p1, Vector2 p2, Vector2 p3) : base(VertexArray.Create(p1, p2, p3))
        {
            if (p1 == p2 || p1 == p3 || p2 == p3)
            {
                throw new ArgumentException("A triangle must have three distinct points");
            }
        }

        protected Triangle(VertexArray vertices, BoundingCircle? boundingCircle)
            : base(vertices, boundingCircle) { }

        public Triangle(Vector2 p1, Vector2 p2, Vector2 p3, BoundingCircle? boundingCircle) : base(
            VertexArray.Create(p1, p2, p3), boundingCircle) { }

        public static readonly Triangle Zero = Create(Vector2.Zero, Vector2.Zero, Vector2.Zero);

        public static Triangle Create(Vector2 p1, Vector2 p2, Vector2 p3) => new(p1, p2, p3);

        #endregion

        #region Properties

        public Vector2 P1 => Vertices[0];
        public Vector2 P2 => Vertices[1];
        public Vector2 P3 => Vertices[2];

        public override Triangle Translate(Vector2 delta) =>
            new(Vertices.Translate(delta), _boundingCircle?.Translate(delta));

        public override Triangle Rotate(float theta) =>
            new(Vertices.Rotate(BoundingCircle.Center, theta), _boundingCircle);

        public override Triangle Rotate(Vector2 rotationCenter, float theta) =>
            new(Vertices.Rotate(rotationCenter, theta), _boundingCircle?.Rotate(rotationCenter, theta));

        public override Triangle Scale(Vector2 delta) => new(Vertices.Scale(BoundingCircle.Center, delta), null);

        public override Triangle Skew(Vector2 delta) => new(Vertices.Skew(delta), null);

        #endregion
    }
}