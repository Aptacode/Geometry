using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Components.ViewModels.Primitives;
using Aptacode.Geometry.Composites;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class ComponentViewModelFactory
    {
        public virtual ComponentViewModel ToViewModel(Primitive primitive)
        {
            return primitive switch
            {
                Point point => new PointViewModel(point),
                Ellipse ellipse => new EllipseViewModel(ellipse),
                PolyLine polyLine => new PolylineViewModel(polyLine),
                Polygon polygon => new PolygonViewModel(polygon),
                PrimitiveGroup primitiveGroup => new PrimitiveGroupViewModel(primitiveGroup),
                _ => null
            };
        }
    }
}