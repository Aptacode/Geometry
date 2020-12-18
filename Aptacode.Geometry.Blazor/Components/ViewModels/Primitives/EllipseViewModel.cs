using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class EllipseViewModel : ComponentViewModel
    {

        public EllipseViewModel(Ellipse ellipse) : base(ellipse)
        {
            Primitive = ellipse;
        }
        
        protected new Ellipse _primitive;
        public new Ellipse Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }
    }
}
