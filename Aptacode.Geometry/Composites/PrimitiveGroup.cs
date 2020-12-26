using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Composites
{
    public record PrimitiveGroup : Primitive
    {
        #region Properties

        public IEnumerable<Primitive> Children { get; init; }

        #endregion

        #region Collision Detection

        public override bool CollidesWith(Primitive p, CollisionDetector detector) => detector.CollidesWith(this, p);

        #endregion

        #region Construction

        public PrimitiveGroup(IEnumerable<Primitive> children) : base(children.Select(v => v.Vertices).Aggregate())
        {
            Children = children;
        }

        public PrimitiveGroup(IEnumerable<Primitive> children, BoundingCircle boundingCircle,
            BoundingRectangle boundingRectangle)
            : base(children.Select(v => v.Vertices).Aggregate(), boundingCircle, boundingRectangle)
        {
            Children = children;
        }

        #endregion

        #region Transformations

        public override PrimitiveGroup Translate(Vector2 delta) => new(Children.Select(c => c.Translate(delta)));

        public override PrimitiveGroup Rotate(float theta) =>
            new(Children.Select(c => c.Rotate(BoundingCircle.Center, theta)));

        public override PrimitiveGroup Rotate(Vector2 rotationCenter, float theta) =>
            new(Children.Select(c => c.Rotate(rotationCenter, theta)));

        public override PrimitiveGroup Scale(Vector2 delta) => new(Children.Select(c => c.Scale(delta)));
        public override PrimitiveGroup Skew(Vector2 delta) => new(Children.Select(c => c.Skew(delta)));

        #endregion
    }
}