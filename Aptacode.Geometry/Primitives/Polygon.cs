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

        public Polygon(VertexArray vertices, BoundingCircle boundingCircle, IEnumerable<(Vector2 p1, Vector2 p2)> edges)
            : base(vertices, boundingCircle)
        {
            _edges = edges;
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

        public override Polygon Translate(Vector2 delta)
        {
            return new(
                Vertices.Translate(delta)); 
                //BoundingCircle.Translate(delta),
                //Edges.Select(l => (l.p1 + delta, l.p2 + delta)));
        }

        public override Polygon Rotate(float theta)
        {
            return new(
                Vertices.Rotate(BoundingCircle.Center, theta));
                //BoundingCircle.Rotate(BoundingCircle.Center, theta),
                //Edges.Select(l => (Vector2.Transform(l.p1, Matrix3x2.CreateRotation(theta, BoundingCircle.Center)), Vector2.Transform(l.p2, Matrix3x2.CreateRotation(theta, BoundingCircle.Center)))));
        }

        public override Polygon Scale(Vector2 delta) => this;

        public override Polygon Skew(Vector2 delta) => this;

        #endregion
    }
}