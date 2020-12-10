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

        public override bool CollidesWith(Point p, ICollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Polygon p, ICollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(PolyLine p, ICollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Circle p, ICollisionDetector detector) => detector.CollidesWith(p, this);

        public override Vector2 GetCenter() => Position;

        public override float GetRadius() => 0.0f;

        #endregion

        #region Transformations

        public override void Rotate(Vector2 delta) { }

        public override void Scale(Vector2 delta) { }

        public override void Skew(Vector2 delta) { }

        #endregion
    }
}