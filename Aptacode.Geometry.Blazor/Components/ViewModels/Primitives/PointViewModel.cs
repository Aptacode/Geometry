using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PointViewModel : ComponentViewModel
    {

        public PointViewModel(Point point) : base(point)
        {
            Primitive = point;
            OnRedraw();
        }

        #region Properties

        protected new Point _primitive;
        public new Point Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }

        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        
        #endregion

        #region ComponentViewModel

        protected override void OnRedraw()
        {
            Position = Primitive.Position * Constants.Scale;
            Radius = Constants.Scale;
            base.OnRedraw();
        }

        #endregion

    }
}
