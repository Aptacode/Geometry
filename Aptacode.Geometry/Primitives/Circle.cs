using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;

namespace Aptacode.Geometry.Primitives
{
    public record Circle(Vector2 Position, float Radius) : Primitive(new[] {Position})
    {
        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Transformations

        public override Circle Translate(Vector2 delta) => new(Position + delta, Radius)
            {BoundingCircle = BoundingCircle.Translate(delta)};

        public override Circle Rotate(float delta) => this;

        public override Circle Scale(Vector2 delta) => this;

        public override Circle Skew(Vector2 delta) => this;

        #endregion
    }
}