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
    public class PolygonViewModel : ComponentViewModel
    {
        public PolygonViewModel(Polygon polygon)
        {
            Polygon = polygon;
            BoundingRectangle = Children.ToBoundingRectangle().Combine(Polygon.BoundingRectangle).AddMargin(Margin);
        }

        public Polygon Polygon { get; }

        public override async Task Draw(IContext2DWithoutGetters ctx)
        {
            OldBoundingRectangle = BoundingRectangle;
            Invalidated = false;

            if (!IsShown)
            {
                return;
            }

            await ConfigureDraw(ctx);

            await Polygon.Draw(ctx);

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
            Polygon.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Polygon.Scale(delta);

            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Polygon.Rotate(theta);

            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Polygon.Rotate(rotationCenter, theta);

            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Polygon.Skew(delta);
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public sealed override void UpdateBoundingRectangle()
        {
            BoundingRectangle = Children.ToBoundingRectangle().Combine(Polygon.BoundingRectangle).AddMargin(Margin);
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector) =>
            component.CollidesWith(Polygon, collisionDetector) || base.CollidesWith(component, collisionDetector);

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector) =>
            primitive.CollidesWith(Polygon, collisionDetector) || base.CollidesWith(primitive, collisionDetector);

        #endregion
    }
}