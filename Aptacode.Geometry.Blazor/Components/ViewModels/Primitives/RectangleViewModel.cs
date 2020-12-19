using System.Numerics;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public class RectangleViewModel : ComponentViewModel
    {
        public RectangleViewModel(Rectangle rectangle) : base(rectangle)
        {
            Primitive = rectangle;
        }


        #region ComponentViewModel

        protected override void Redraw()
        {
            Position = Primitive.TopLeft.ToScale();
            Size = Primitive.Size.ToScale();
            base.Redraw();
        }

        #endregion

        #region Properties

        public new Rectangle Primitive
        {
            get => (Rectangle) _primitive;
            set
            {
                _primitive = value;
                Redraw();
            }
        }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        #endregion

        #region Transformation

        public override void Translate(Vector2 delta)
        {
            Primitive = Primitive.Translate(delta);
        }

        public override void Rotate(float theta)
        {
            Primitive = Primitive.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Primitive = Primitive.Rotate(rotationCenter, theta);
        }

        public override void Scale(Vector2 delta)
        {
            Primitive = Primitive.Scale(delta);
        }

        public override void Skew(Vector2 delta)
        {
            Primitive = Primitive.Skew(delta);
        }

        #endregion
    }
}