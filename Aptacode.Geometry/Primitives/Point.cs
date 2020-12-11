using System.Numerics;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Primitives
{
    public class Point : Primitive
    {
        #region Construction

        public Point(Vector2 position) : base(position) { }

        #endregion

        #region Properties

        public Vector2 Position
        {
            get => Vertices[0];
            set => Vertices[0] = value;
        }

        #endregion

        #region Collision Detection

        public override void UpdateBoundingCircle()
        {
            Center = Position;
            Radius = 0.0f;
        }

        public override bool CollidesWith(Primitive p, CollisionDetector detector)
        {
            return detector.CollidesWith(this, p);
        }
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