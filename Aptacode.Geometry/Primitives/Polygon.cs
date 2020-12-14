using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Primitives
{
    public class Polygon : Primitive
    {
        #region Construction

        protected Polygon(Vector2 p1, Vector2 p2, Vector2 p3, params Vector2[] points) : base(new Vector2[3]
        {
            p1, p2, p3
        }.Concat(points).ToArray()) { }

        #endregion

        #region Properties

        public IEnumerable<(Vector2 p1, Vector2 p2)> Edges()
        {
            var edges = Vertices.Zip(Vertices.Skip(1), (a, b) => (a, b)).ToList();
            edges.Add((Vertices.Last(), Vertices[0]));
            return edges;
        }

        #endregion

        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        public override bool CollidesWith(Point p, CollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Polygon p, CollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(PolyLine p, CollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Circle p, CollisionDetector detector) => detector.CollidesWith(p, this);

        #endregion

        #region Transformations

        public override void Rotate(Vector2 delta) { }

        public override void Scale(Vector2 delta) { }

        public override void Skew(Vector2 delta) { }

        #endregion
    }
}