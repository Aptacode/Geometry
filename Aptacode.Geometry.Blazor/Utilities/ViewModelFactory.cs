﻿using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Components.ViewModels.Primitives;
using Aptacode.Geometry.Composites;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class ViewModelFactory
    {
        public ComponentViewModel ToViewModel(Primitive primitive)
        {
            switch (primitive)
            {
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
            }

            return null;
        }
    }
}