using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public record Triangle : Polygon
    {
        #region Construction

        public Triangle(Vector2 p1, Vector2 p2, Vector2 p3) : base(VertexArray.Create(p1, p2, p3)) { }

        public Triangle(Vector2 p1, Vector2 p2, Vector2 p3, BoundingCircle boundingCircle,
            (Vector2 p1, Vector2 p2)[] edges) : base(VertexArray.Create(p1, p2, p3), boundingCircle, edges) { }

        public static readonly Triangle Zero = Create(Vector2.Zero, Vector2.Zero, Vector2.Zero);

        public static Triangle Create(Vector2 p1, Vector2 p2, Vector2 p3) => new(p1, p2, p3);

        #endregion

        #region Properties

        public Vector2 P1 => Vertices[0];
        public Vector2 P2 => Vertices[1];
        public Vector2 P3 => Vertices[2];

        public override Triangle Translate(Vector2 delta) =>
            new(P1 + delta, P2 + delta, P3 + delta, BoundingCircle.Translate(delta),
                Edges.Select(l => (l.p1 + delta, l.p2 + delta)).ToArray());

        public override Triangle Rotate(float delta) => this;

        public override Triangle Scale(Vector2 delta) => this;

        public override Triangle Skew(Vector2 delta) => this;

        #endregion
    }
}