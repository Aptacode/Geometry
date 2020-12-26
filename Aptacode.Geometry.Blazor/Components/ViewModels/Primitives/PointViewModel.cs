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
            _oldPrimitive = _primitive;
        }

        #endregion

        #region Properties

        public new Point Primitive
        {
            get => (Point) _primitive;
            set
            {
                _primitive = value;
                Position = ((int) value.Position.X, (int) value.Position.Y);
                Radius = 1;
                Invalidated = true;
            }
        }

        public (int X, int Y) Position { get; set; }
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