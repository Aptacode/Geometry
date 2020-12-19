using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Composites;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public sealed class PrimitiveGroupViewModel : ComponentViewModel
    {
        public PrimitiveGroupViewModel(PrimitiveGroup primitiveGroup) : base(primitiveGroup)
        {
            Children = new List<ComponentViewModel>();
            Primitive = primitiveGroup;
        }

        #region ComponentViewModel

        protected override void Redraw()
        {
            Children.Clear();
            var viewModelFactory = new ViewModelFactory();
            foreach (var child in Primitive.Children)
            {
                Children.Add(viewModelFactory.ToViewModel(child));
            }

            base.Redraw();
        }

        #endregion

        #region Properties

        public new PrimitiveGroup Primitive
        {
            get => (PrimitiveGroup) _primitive;
            set
            {
                SetProperty(ref _primitive, value);
                Redraw();
            }
        }

        public List<ComponentViewModel> Children { get; set; }

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