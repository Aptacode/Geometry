using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Polygon : Primitive
    {
        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Construction

        public Polygon(VertexArray vertices) : base(vertices) { }

        public Polygon(VertexArray vertices, BoundingCircle? boundingCircle)
            : base(vertices, boundingCircle) { }

        public static Polygon Create(params float[] points)
        {
            if (points.Length < 3)
            {
                return Zero;
            }

            var vertices = new List<Vector2>();
            for (var i = 0; i < points.Length; i += 2)
            {
                vertices.Add(new Vector2(points[i], points[i + 1]));
            }

            return new Polygon(VertexArray.Create(vertices));
        }

        public static readonly Polygon Zero = Create(Vector2.Zero, Vector2.Zero, Vector2.Zero);

        public static Polygon Create(Vector2 p1, Vector2 p2, Vector2 p3, params Vector2[] points) =>
            new(VertexArray.Create(p1, p2, p3, points));

        #endregion

        #region Properties

        private IEnumerable<(Vector2 p1, Vector2 p2)> CalculateEdges()
        {
            var edges = Vertices.Zip(Vertices.Skip(1), (a, b) => (a, b)).ToList();
            edges.Add((Vertices.Last(), Vertices.First()));
            return edges;
        }

        private IEnumerable<(Vector2 p1, Vector2 p2)> _edges;
        public IEnumerable<(Vector2 p1, Vector2 p2)> Edges => _edges ??= CalculateEdges();

        #endregion

        #region Transformations

        public override Polygon Translate(Vector2 delta) =>
            new(Vertices.Translate(delta), _boundingCircle?.Translate(delta));

        public override Polygon Rotate(float theta) =>
            new(Vertices.Rotate(BoundingCircle.Center, theta), _boundingCircle);

        public override Polygon Rotate(Vector2 rotationCenter, float theta) =>
            new(Vertices.Rotate(rotationCenter, theta), _boundingCircle?.Rotate(rotationCenter, theta));

        public override Polygon Scale(Vector2 delta) => new(Vertices.Scale(BoundingCircle.Center, delta));

        public override Polygon Skew(Vector2 delta) => new(Vertices.Skew(delta));

        #endregion
    }
}