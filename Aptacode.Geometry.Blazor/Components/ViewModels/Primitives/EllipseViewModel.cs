using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Primitives;
using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class EllipseViewModel : ComponentViewModel
    {
        public EllipseViewModel(Ellipse ellipse) : base(ellipse)
        {
            Primitive = ellipse;
        }

        #region Canvas

        public override async Task Draw(IContext2DWithoutGetters ctx)
        {
            await ctx.BeginPathAsync();

            await ctx.EllipseAsync(Position.X, Position.Y, (int) Radius.X, (int) Radius.Y, 0, 0, 360);

            await ctx.FillStyleAsync(FillColorName);

            await ctx.StrokeStyleAsync(BorderColorName);

            await ctx.LineWidthAsync(BorderThickness);

            await ctx.FillAsync(FillRule.NonZero);
            await ctx.StrokeAsync();
            
            await base.Draw(ctx);
        }

        #endregion

        #region Properties

        public new Ellipse Primitive
        {
            get => (Ellipse) _primitive;
            set
            {
                _primitive = value;
                Invalidated = true;
            }
        }

        public (int X, int Y) Position => ((int)Primitive.Position.X, (int)Primitive.Position.Y);

        public Vector2 Radius => Primitive.Radii;

        #endregion

    }
}