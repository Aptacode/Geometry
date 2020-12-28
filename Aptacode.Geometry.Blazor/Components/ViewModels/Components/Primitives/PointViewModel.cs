using System;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public class PointViewModel : ComponentViewModel
    {
        public PointViewModel(Point point)
        {
            Point = point;
            BoundingRectangle = Children.ToBoundingRectangle().Combine(Point.BoundingRectangle).AddMargin(Margin);
        }

        public Point Point { get; }
        public override async Task CustomDraw(IContext2DWithoutGetters ctx)
        {
            await ctx.BeginPathAsync();

            await ctx.EllipseAsync((int)Point.Position.X, (int)Point.Position.Y, 1, 1, 0, 0, 2 * Math.PI);
            await ctx.FillAsync(FillRule.NonZero);
            await ctx.StrokeAsync();
        }

        #region Transformations

        public override void Translate(Vector2 delta)
        {
            Point.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Point.Scale(delta);

            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Point.Rotate(theta);

            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Point.Rotate(rotationCenter, theta);

            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Point.Skew(delta);
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public override void UpdateBoundingRectangle()
        {
            BoundingRectangle = Children.ToBoundingRectangle().Combine(Point.BoundingRectangle).AddMargin(Margin);
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector) =>
            component.CollidesWith(Point, collisionDetector) || base.CollidesWith(component, collisionDetector);

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector) =>
            primitive.CollidesWith(Point, collisionDetector) || base.CollidesWith(primitive, collisionDetector);

        #endregion
    }
}