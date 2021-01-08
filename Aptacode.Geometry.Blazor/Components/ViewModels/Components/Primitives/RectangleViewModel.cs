using System.Numerics;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public class RectangleViewModel : PolygonViewModel
    {
        #region Ctor
        public RectangleViewModel(Rectangle rectangle) : base(rectangle)
        {
            
        }

        #endregion

        #region Props

        public Rectangle Rectangle
        {
            get => (Rectangle)Polygon;
            set
            {
                Polygon = value;
                UpdateBoundingRectangle();
                Invalidated = true;
            }
        }
       
        #endregion

    }
}