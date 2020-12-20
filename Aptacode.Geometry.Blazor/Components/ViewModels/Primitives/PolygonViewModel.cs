using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;
using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PolygonViewModel : ComponentViewModel
    {
        public PolygonViewModel(Polygon polygon) : base(polygon)
        {
            Primitive = polygon;
        }

        #region Canvas

        public override async Task Draw(IContext2DWithoutGetters ctx)
        {
            await ctx.BeginPathAsync();
            var firstVertex = Vertices[0];
            await ctx.MoveToAsync(firstVertex.X, firstVertex.Y);
            for (var i = 1; i < Vertices.Length; i++)
            {
                var vertex = Vertices[i];
                await ctx.LineToAsync(vertex.X, vertex.Y);
            }

            await ctx.ClosePathAsync();

            await ctx.FillStyleAsync(FillColor.ToKnownColor().ToString());
            await ctx.FillAsync(FillRule.NonZero);
            await ctx.StrokeAsync();
        }

        #endregion

        #region Properties

        public new Polygon Primitive
        {
            get => (Polygon) _primitive;
            set
            {
                _primitive = value;
                Vertices = value.Vertices.Vertices.ToScale();
            }
        }

        public Vector2[] Vertices { get; set; }

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