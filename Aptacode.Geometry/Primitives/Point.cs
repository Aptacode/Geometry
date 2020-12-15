using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;

namespace Aptacode.Geometry.Primitives
{
    public record Point(Vector2 Position) : Primitive(new[] {Position})
    {
        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Transformations

        public override Point Translate(Vector2 delta) => new(Position + delta)
            {BoundingCircle = BoundingCircle.Translate(delta)};

        public override Point Rotate(float delta) => this;

        public override Point Scale(Vector2 delta) => this;

        public override Point Skew(Vector2 delta) => this;

        #endregion
    }
}