using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public sealed class PolylineViewModel : ComponentViewModel
    {
        public PolylineViewModel(PolyLine polyLine) : base(polyLine)
        {
            Primitive = polyLine;
        }

        #region Canvas

        public override async Task Draw(IContext2DWithoutGetters ctx)
        {
            await ctx.BeginPathAsync();
            await ctx.MoveToAsync(Vertices[0], Vertices[1]);
            for (var i = 2; i < Vertices.Length; i += 2)
            {
                await ctx.LineToAsync(Vertices[i], Vertices[i + 1]);
            }

            await ctx.FillStyleAsync(FillColorName);
            await ctx.StrokeStyleAsync(BorderColorName);
            await ctx.LineWidthAsync(BorderThickness);
            await ctx.StrokeAsync();
            await base.Draw(ctx);
        }

        #endregion

        #region Properties

        public new PolyLine Primitive
        {
            get => (PolyLine) _primitive;
            set
            {
                _primitive = value;
                Vertices = Primitive.Vertices.Vertices.ToIntArray();
                Invalidated = true;
            }
        }

        public int[] Vertices { get; set; }

        #endregion

        #region Transformation

        public override void Translate(Vector2 delta)
        {
            base.Translate(delta);
            Vertices = Primitive.Vertices.Vertices.ToIntArray();
        }

        #endregion
    }
}