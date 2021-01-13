using Aptacode.Geometry.Blazor.Components.ViewModels.Components.Events;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public class ButtonViewModel : RectangleViewModel
    {
        #region Ctor

        public ButtonViewModel(Rectangle rectangle) : base(rectangle)
        {
        }

        #endregion

        #region Props



        #endregion

        #region Events

        public override bool HandleMouseEvent(BaseMouseEvent mouseEvent)
        {
            return base.HandleMouseEvent(mouseEvent);
        }

        #endregion
    }
}