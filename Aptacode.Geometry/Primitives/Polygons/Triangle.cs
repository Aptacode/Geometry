using System.Numerics;
using Aptacode.Geometry.Collision.Circles;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public record Triangle(Vector2 P1, Vector2 P2, Vector2 P3) : Polygon(new[] {P1, P2, P3})
    {
        #region Construction

        public static readonly Triangle Zero = Create(Vector2.Zero, Vector2.Zero, Vector2.Zero);

        public static Triangle Create(Vector2 p1, Vector2 p2, Vector2 p3) => new(p1, p2, p3);

        #endregion

        #region Properties

        public override Triangle Translate(Vector2 delta) =>
            new(P1 + delta, P2 + delta, P3 + delta)
                {BoundingCircle = BoundingCircle.Translate(delta)};

        public override Triangle Rotate(float delta) => this;

        public override Triangle Scale(Vector2 delta) => this;

        public override Triangle Skew(Vector2 delta) => this;

        #endregion
    }
}