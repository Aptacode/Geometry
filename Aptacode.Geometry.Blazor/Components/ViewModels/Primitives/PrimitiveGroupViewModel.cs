using Aptacode.Geometry.Composites;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class PrimitiveGroupViewModel : ComponentViewModel
    {

        public PrimitiveGroupViewModel(PrimitiveGroup primitiveGroup) : base(primitiveGroup)
        {
            Primitive = primitiveGroup;
        }
        
        protected new PrimitiveGroup _primitive;
        public new PrimitiveGroup Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }
    }
}
