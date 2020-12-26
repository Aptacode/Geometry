using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Primitives;
using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PointViewModel : ComponentViewModel
    {
        public PointViewModel(Point point) : base(point)
        {
            Primitive = point;
            Radius = 1;
        }

        #region Canvas

        public override async Task Draw(IContext2DWithoutGetters ctx)
        {
            await ctx.BeginPathAsync();

            await ctx.EllipseAsync(Position.X, Position.Y, (int) Radius, (int) Radius, 0, 0, 360);

            await ctx.FillStyleAsync(FillColorName);
            await ctx.StrokeStyleAsync(BorderColorName);


            await ctx.LineWidthAsync(BorderThickness);

            await ctx.FillAsync(FillRule.NonZero);
            await ctx.StrokeAsync();
            Invalidated = false;
        }

        #endregion

        #region Properties

        public new Point Primitive
        {
            get => (Point) _primitive;
            set
            {
                _primitive = value;
                Invalidated = true;
            }
        }

        public (int X, int Y) Position => ((int) Primitive.Position.X, (int) Primitive.Position.Y);
        public float Radius { get; set; }

        #endregion
    }
}