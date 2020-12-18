using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class RectangleViewModel : ComponentViewModel
    {

        public RectangleViewModel(Rectangle rectangle) : base(rectangle)
        {
            Primitive = rectangle;
        }
        
        protected new Rectangle _primitive;
        public new Rectangle Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }
    }
}
