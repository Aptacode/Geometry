using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Primitives
{
    public class PolyLine : Primitive
    {
        #region Properties

        public virtual IEnumerable<(Vector2 p1, Vector2 p2)> LineSegments()
        {
            return Vertices.Zip(Vertices.Skip(1), (a, b) => (a, b));
        }

        #endregion

        #region Construction

        public static PolyLine Create(Vector2 p1, Vector2 p2, params Vector2[] points)
        {
            var allPoints = new List<Vector2>
            {
                p1, p2
            };

            allPoints.AddRange(points);

            return new PolyLine(allPoints.ToArray());
        }


        protected PolyLine(params Vector2[] points) : base(points) { }

        #endregion

        #region Collision Detection

        public override bool CollidesWith(Point p, ICollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Polygon p, ICollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(PolyLine p, ICollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Circle p, ICollisionDetector detector) => detector.CollidesWith(p, this);

        public (Vector2 p1, Vector2 p2) FurthestPoints()
        {
            //Todo Implement Planar case
            //https://en.wikipedia.org/wiki/Closest_pair_of_points_problem
            var maxDistance = 0.0f;
            var p1 = Vector2.Zero;
            var p2 = Vector2.Zero;
            for (var i = 0; i < Vertices.Length - 1; i++)
            {
                for (var j = i + 1; j < Vertices.Length - 1; j++)
                {
                    var p = Vertices[i];
                    var q = Vertices[i];
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

        public override Vector2 GetCenter()
        {
            var (p1, p2) = FurthestPoints();
            return (p1 + p2) * 0.5f;
        }

        public override float GetRadius()
        {
            var (p1, p2) = FurthestPoints();

            return (p1 - p2).Length() / 2.0f;
        }

        #endregion

        #region Transformations

        public override void Rotate(Vector2 delta) { }

        public override void Scale(Vector2 delta) { }

        public override void Skew(Vector2 delta) { }

        #endregion
    }
}