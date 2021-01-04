using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public static class PrimitiveViewModelExtensions
    {
        public static EllipseViewModel ToViewModel(this Ellipse ellipse)
        {
            return new(ellipse);
        }

        public static PolygonViewModel ToViewModel(this Polygon polygon)
        {
            return new(polygon);
        }

        public static PolylineViewModel ToViewModel(this PolyLine polyline)
        {
            return new(polyline);
        }

        public static PointViewModel ToViewModel(this Point point)
        {
            return new(point);
        }
    }
}