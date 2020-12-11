using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Primitives
{
    public abstract class Polygon : Primitive
    {
        #region Properties

        public IEnumerable<(Vector2 p1, Vector2 p2)> Edges()
        {
            var edges = Vertices.Zip(Vertices.Skip(1), (a, b) => (a, b)).ToList();
            edges.Add((Vertices.Last(), Vertices[0]));
            return edges;
        }

        #endregion

        #region Construction

        protected Polygon(params Vector2[] points) : base(points) { }

        #endregion

        #region Collision Detection
        public override bool CollidesWith(Primitive p, CollisionDetector detector)
        {
            return detector.CollidesWith(this, p);
        }

        public override bool CollidesWith(Point p, CollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Polygon p, CollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(PolyLine p, CollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Circle p, CollisionDetector detector) => detector.CollidesWith(p, this);


        public (Vector2 p1, Vector2 p2) FurthestPoints()
        {
            //Todo Implement Planar case
            //https://en.wikipedia.org/wiki/Closest_pair_of_points_problem
            var maxDistance = 0.0f;
            var p1 = Vector2.Zero;
            var p2 = Vector2.Zero;
            for (var i = 0; i < Vertices.Length - 1; i++)
            {
                for (var j = i + 1; j < Vertices.Length; j++)
                {
                    var p = Vertices[i];
                    var q = Vertices[j];
                    var length = (p - q).Length();
                    if (length > maxDistance)
                    {
                        maxDistance = length;
                        p1 = p;
                        p2 = q;
                    }
                }
            }

            return (p1, p2);
        }

        #endregion

        #region Transformations

        public override void Rotate(Vector2 delta) { }

        public override void Scale(Vector2 delta) { }

        public override void Skew(Vector2 delta) { }

        #endregion
    }
}