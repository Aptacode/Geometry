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
    public class EllipseViewModel : ComponentViewModel
    {
        #region Ctor

        public EllipseViewModel(Ellipse ellipse)
        {
            Ellipse = ellipse;
            BoundingRectangle = Children.ToBoundingRectangle().Combine(Ellipse.BoundingRectangle).AddMargin(Margin);
        }

        #endregion


        public Ellipse Ellipse { get; }

        public override async Task Draw(IContext2DWithoutGetters ctx)
        {
            OldBoundingRectangle = BoundingRectangle;
            Invalidated = false;

            if (!IsShown)
            {
                return;
            }

            await ConfigureDraw(ctx);
            await ctx.BeginPathAsync();

            await ctx.EllipseAsync((int)Ellipse.Position.X, (int)Ellipse.Position.Y, (int)Ellipse.Radii.X,
                (int)Ellipse.Radii.Y, Ellipse.Rotation, 0, 2 * Math.PI);
            await ctx.FillAsync(FillRule.NonZero);
            await ctx.StrokeAsync();
            foreach (var child in Children)
            {
                await child.Draw(ctx);
            }

            if (!string.IsNullOrEmpty(Text))
            {
                await ctx.TextAlignAsync(TextAlign.Center);
                await ctx.FillStyleAsync("black");
                await ctx.FillTextAsync(Text, BoundingRectangle.Center.X, BoundingRectangle.Center.Y);
            }
        }

        #region Transformations

        public override void Translate(Vector2 delta)
        {
            Ellipse.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Ellipse.Scale(delta);

            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Ellipse.Rotate(theta);

            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Ellipse.Rotate(rotationCenter, theta);

            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Ellipse.Skew(delta);
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public override void UpdateBoundingRectangle()
        {
            BoundingRectangle = Children.ToBoundingRectangle().Combine(Ellipse.BoundingRectangle).AddMargin(Margin);
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector) =>
            component.CollidesWith(Ellipse, collisionDetector) || base.CollidesWith(component, collisionDetector);

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector) =>
            primitive.CollidesWith(Ellipse, collisionDetector) || base.CollidesWith(primitive, collisionDetector);

        #endregion
    }
}