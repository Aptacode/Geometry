using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PolylineViewModel : ComponentViewModel
    {

        public PolylineViewModel(PolyLine polyLine) : base(polyLine)
        {
            Primitive = polyLine;
        }
        
        protected new PolyLine _primitive;
        public new PolyLine Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }
    }
}
