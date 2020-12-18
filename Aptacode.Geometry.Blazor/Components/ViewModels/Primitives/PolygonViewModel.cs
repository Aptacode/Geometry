using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PolygonViewModel : ComponentViewModel
    {

        public PolygonViewModel(Polygon polygon) : base(polygon)
        {
            Primitive = polygon;
        }
        
        protected new Polygon _primitive;
        public new Polygon Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }
    }
}
