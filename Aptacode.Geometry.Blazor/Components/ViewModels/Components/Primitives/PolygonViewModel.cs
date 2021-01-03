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
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(Polygon.BoundingRectangle).AddMargin(Margin);
        }

        public Polygon Polygon { get; set; }

        public override async Task CustomDraw(IContext2DWithoutGetters ctx)
        {
            await ctx.BeginPathAsync();

            await ctx.MoveToAsync((int) Polygon.Vertices[0].X, (int) Polygon.Vertices[0].Y);
            for (var i = 1; i < Polygon.Vertices.Length; i++)
            {
                var vertex = Polygon.Vertices[i];
                await ctx.LineToAsync((int) vertex.X, (int) vertex.Y);
            }

            await ctx.ClosePathAsync();
            await ctx.FillAsync(FillRule.NonZero);
            await ctx.StrokeAsync();
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

        public override BoundingRectangle UpdateBoundingRectangle()
        {
            BoundingRectangle = base.UpdateBoundingRectangle().Combine(Polygon.BoundingRectangle);
            return BoundingRectangle;
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector) =>
            component.CollidesWith(Polygon, collisionDetector) || base.CollidesWith(component, collisionDetector);

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector) =>
            primitive.CollidesWith(Polygon, collisionDetector) || base.CollidesWith(primitive, collisionDetector);

        #endregion
    }
}