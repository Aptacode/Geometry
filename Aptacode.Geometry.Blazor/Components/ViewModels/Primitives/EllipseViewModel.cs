using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class EllipseViewModel : ComponentViewModel
    {
        public EllipseViewModel(Ellipse ellipse) : base(ellipse)
        {
            Primitive = ellipse;
            OnRedraw();
        }


        #region ComponentViewModel

        protected override void OnRedraw()
        {
            Position = Primitive.Position * Constants.Scale;
            Radius = Primitive.Radius * Constants.Scale;
            base.OnRedraw();
        }

        #endregion

        #region Properties

        protected new Ellipse _primitive;

        public new Ellipse Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }

        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        #endregion
    }
}