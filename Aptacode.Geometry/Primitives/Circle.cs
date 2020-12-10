using System.Numerics;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Primitives
{
    public class Circle : Primitive
    {
        #region Construction

        public Circle(Vector2 position, float radius) : base(position)
        {
            Radius = radius;
        }

        #endregion

        #region Properties

        public Vector2 Position
        {
            get => Vertices[0];
            set => Vertices[0] = value;
        }

        public float Radius { get; set; }

        #endregion

        #region Collision Detection

        public override bool CollidesWith(Point p, ICollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Polygon p, ICollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(PolyLine p, ICollisionDetector detector) => detector.CollidesWith(p, this);
        public override bool CollidesWith(Circle p, ICollisionDetector detector) => detector.CollidesWith(p, this);

        public override Vector2 GetCenter() => Position;
        public override float GetRadius() => Radius;

        #endregion

        #region Transformations

        public override void Rotate(Vector2 delta) { }

        public override void Scale(Vector2 delta) { }

        public override void Skew(Vector2 delta) { }

        #endregion
    }
}