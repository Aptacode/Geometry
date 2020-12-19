using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Components.ViewModels.Primitives;
using Aptacode.Geometry.Composites;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class ViewModelFactory
    {
        public ComponentViewModel ToViewModel(Primitive primitive)
        {
            switch (primitive)
            {
                case Rectangle rectangle:
                    return new RectangleViewModel(rectangle);
                case Point point:
                    return new PointViewModel(point);
                case Ellipse ellipse:
                    return new EllipseViewModel(ellipse);
                case PolyLine polyLine:
                    return new PolylineViewModel(polyLine);
                case Polygon polygon:
                    return new PolygonViewModel(polygon);
                case PrimitiveGroup primitiveGroup:
                    return new PrimitiveGroupViewModel(primitiveGroup);
                default:
                    break;
            }

            return null;
        }
    }
}