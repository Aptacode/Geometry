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
    public class PolylineViewModel : ComponentViewModel
    {
        public PolylineViewModel(PolyLine polyLine)
        {
            PolyLine = polyLine;
            BoundingRectangle = Children.ToBoundingRectangle().Combine(PolyLine.BoundingRectangle).AddMargin(Margin);
        }

        public PolyLine PolyLine { get; }

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
            await ctx.MoveToAsync((int)PolyLine.Vertices[0].X, (int)PolyLine.Vertices[0].Y);
            for (var i = 1; i < PolyLine.Vertices.Length; i++)
            {
                var vertex = PolyLine.Vertices[i];
                await ctx.LineToAsync((int)vertex.X, (int)vertex.Y);
            }

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
            PolyLine.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            PolyLine.Scale(delta);

            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            PolyLine.Rotate(theta);

            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            PolyLine.Rotate(rotationCenter, theta);

            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            PolyLine.Skew(delta);
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public override void UpdateBoundingRectangle()
        {
            BoundingRectangle = Children.ToBoundingRectangle().Combine(PolyLine.BoundingRectangle).AddMargin(Margin);
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector) =>
            component.CollidesWith(PolyLine, collisionDetector) || base.CollidesWith(component, collisionDetector);

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector) =>
            primitive.CollidesWith(PolyLine, collisionDetector) || base.CollidesWith(primitive, collisionDetector);

        #endregion
    }
}