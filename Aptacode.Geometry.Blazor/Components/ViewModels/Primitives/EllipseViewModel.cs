using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Utilities;
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

            await ctx.EllipseAsync(Position.X, Position.Y, (int) Radii.X, (int) Radii.Y, (double)Rotation, 0, 360);

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

        public new Ellipse Primitive
        {
            get => (Ellipse) _primitive;
            set
            {
                _primitive = value;
                Position = ((int) value.Position.X, (int) value.Position.Y);
                Radii = (value.Radii.X, value.Radii.Y);
                Rotation = value.Rotation;
                Invalidated = true;
            }
        }

        public (int X, int Y) Position { get; set; }
        public (float X, float Y) Radii { get; set; }
        public float Rotation { get; set; }

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