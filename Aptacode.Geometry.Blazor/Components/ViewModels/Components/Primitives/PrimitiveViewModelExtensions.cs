using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public static class PrimitiveViewModelExtensions
    {
        public static EllipseViewModel ToViewModel(this Ellipse ellipse) => new(ellipse);

        public static PolygonViewModel ToViewModel(this Polygon polygon) => new(polygon);

        public static PolylineViewModel ToViewModel(this PolyLine polyline) => new(polyline);

        public static PointViewModel ToViewModel(this Point point) => new(point);
    }
}