using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Utilities;
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
        }

        #region Canvas

        public override async Task Draw(IContext2DWithoutGetters ctx)
        {
            await ctx.BeginPathAsync();
            await ctx.EllipseAsync(Position.X, Position.Y, Radius, Radius, 0, 0, 360);
            await ctx.FillStyleAsync(FillColor.ToKnownColor().ToString());
            await ctx.FillAsync(FillRule.NonZero);
            await ctx.StrokeAsync();
        }

        #endregion

        #region Properties

        public new Point Primitive
        {
            get => (Point) _primitive;
            set
            {
                _primitive = value;
                Position = value.Position.ToScale();
                Radius = 1.0f.ToScale();
            }
        }

        public Vector2 Position { get; set; }
        public float Radius { get; set; }

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