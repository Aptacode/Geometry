using System.Numerics;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PointViewModel : ComponentViewModel
    {
        public PointViewModel(Point point) : base(point)
        {
            Primitive = point;
        }

        #region ComponentViewModel

        protected override void Redraw()
        {
            Position = Primitive.Position.ToScale();
            Radius = Utilities.Constants.Scale;
            base.Redraw();
        }

        #endregion

        #region Properties

        public new Point Primitive
        {
            get => (Point) _primitive;
            set
            {
                SetProperty(ref _primitive, value);
                Redraw();
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