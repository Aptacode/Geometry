using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;

namespace Aptacode.Geometry.Primitives
{
    public record PolyLine(IEnumerable<Vector2> Points) : Primitive(Points)
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

        #endregion

        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Transformations

        public override PolyLine Translate(Vector2 delta)
        {
            return new(Vertices.Select(v => v + delta).ToArray())
                {BoundingCircle = BoundingCircle.Translate(delta)};
        }

        public override PolyLine Rotate(float delta) => this;

        public override PolyLine Scale(Vector2 delta) => this;

        public override PolyLine Skew(Vector2 delta) => this;

        #endregion
    }
}