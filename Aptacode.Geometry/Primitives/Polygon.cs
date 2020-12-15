using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;

namespace Aptacode.Geometry.Primitives
{
    public record Polygon(IEnumerable<Vector2> Points) : Primitive(Points)
    {
        #region Construction

        public static readonly Polygon Zero = Create(Vector2.Zero, Vector2.Zero, Vector2.Zero);
        
        public static Polygon Create(Vector2 p1, Vector2 p2, Vector2 p3, params Vector2[] points) => new(new []
        {
            p1, p2, p3
        }.Concat(points));
        
        #endregion

        #region Properties

        private IEnumerable<(Vector2 p1, Vector2 p2)> CalculateEdges()
        {
            var edges = Vertices.Zip(Vertices.Skip(1), (a, b) => (a, b)).ToList();
            edges.Add((Vertices.Last(), Vertices.First()));
            _edges = edges;
            return edges;
        }

        private IEnumerable<(Vector2 p1, Vector2 p2)>? _edges;

        public IEnumerable<(Vector2 p1, Vector2 p2)> Edges
        {
            get => _edges ?? CalculateEdges();
            init => _edges = value;
        } 

        #endregion

        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Transformations

        public override Polygon Translate(Vector2 delta)
        {
            return new(Vertices.Select(v => v + delta).ToArray())
            {
                BoundingCircle = BoundingCircle.Translate(delta),
                Edges = Edges.Select(l => (l.p1 + delta, l.p2 + delta))
            };
        }

        public override Polygon Rotate(float delta) => this;

        public override Polygon Scale(Vector2 delta) => this;

        public override Polygon Skew(Vector2 delta) => this;

        #endregion
    }
}