using System.Numerics;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class RectangleViewModel : ComponentViewModel
    {

        public RectangleViewModel(Rectangle rectangle) : base(rectangle)
        {
            Primitive = rectangle;
            OnRedraw();
        }

        #region Properties

        protected new Rectangle _primitive;
        public new Rectangle Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        #endregion


        #region ComponentViewModel

        protected override void OnRedraw()
        {
            Position = Primitive.TopLeft * Constants.Scale;
            Size = Primitive.Size * Constants.Scale;
            base.OnRedraw();
        }

        #endregion
    }
}
