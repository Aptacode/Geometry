using System.Numerics;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class EllipseViewModel : ComponentViewModel
    {
        public EllipseViewModel(Ellipse ellipse) : base(ellipse)
        {
            Primitive = ellipse;
        }

        #region ComponentViewModel

        protected override void Redraw()
        {
            Position = Primitive.Position.ToScale();
            Radius = Primitive.Radius.ToScale();
            base.Redraw();
        }

        #endregion

        #region Properties

        public new Ellipse Primitive
        {
            get => (Ellipse) _primitive;
            set
            {
                _primitive = value;
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