using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Composites;
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Primitives
{
    public sealed class PrimitiveGroupViewModel : ComponentViewModel
    {

        public PrimitiveGroupViewModel(PrimitiveGroup primitiveGroup) : base(primitiveGroup)
        {
            Children = new List<ComponentViewModel>();
            Primitive = primitiveGroup;
            OnRedraw();
        }

        #region Properties

        private new PrimitiveGroup _primitive;
        public new PrimitiveGroup Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }

        public List<ComponentViewModel> Children { get; set; }

        #endregion
        #region ComponentViewModel

        protected override void OnRedraw()
        {
            Children.Clear();
            var viewModelFactory = new ViewModelFactory();
            foreach (var child in Primitive.Children)
            {
                Children.Add(viewModelFactory.ToViewModel(child));
            }
            base.OnRedraw();
        }

        #endregion
    }
}
