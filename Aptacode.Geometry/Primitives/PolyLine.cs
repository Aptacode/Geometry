using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Primitives
{
    public record PolyLine(Vector2[] points) : Primitive(points)
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