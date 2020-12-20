using System.Linq;
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

            await ctx.StrokeAsync();
        }

        #endregion

        #region Properties

        public new PolyLine Primitive
        {
            get => (PolyLine) _primitive;
            set
            {
                _primitive = value;
                Vertices = value.Vertices.Vertices.ToIntScale();
            }
        }

        public int[] Vertices { get; set; }

        #endregion

        #region Transformation

        public override void Translate(Vector2 delta)
        {
            Primitive = Primitive.Translate(delta);
        }

        public override void Rotate(float theta)
        {
            Primitive = Primitive.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Primitive = Primitive.Rotate(rotationCenter, theta);
        }

        public override void Scale(Vector2 delta)
        {
            Primitive = Primitive.Scale(delta);
        }

        public override void Skew(Vector2 delta)
        {
            Primitive = Primitive.Skew(delta);
        }

        #endregion
    }
}