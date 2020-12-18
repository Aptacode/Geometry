using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PointViewModel : ComponentViewModel
    {

        public PointViewModel(Point point) : base(point)
        {
            Primitive = point;
        }
        
        protected new Point _primitive;
        public new Point Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }
    }
}
