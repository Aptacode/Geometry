using System.Drawing;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class ComponentBuilder
    {
        public ComponentBuilder SetBorderThickness(int borderThickness)
        {
            _borderThickness = borderThickness;
            return this;
        }

        public ComponentBuilder SetBorderColor(Color borderColor)
        {
            _borderColor = borderColor;
            return this;
        }

        public ComponentBuilder SetFillColor(Color fillColor)
        {
            _fillColor = fillColor;
            return this;
        }

        public ComponentBuilder SetPrimitive(Primitive primitive)
        {
            _primitive = primitive;
            return this;
        }

        public ComponentViewModel Build()
        {
            var component = _viewModelFactory.ToViewModel(_primitive);
            component.BorderColor = _borderColor;
            component.FillColor = _fillColor;
            component.BorderThickness = _borderThickness;

            Reset();
            return component;
        }

        public void Reset()
        {
            _borderColor = Color.Black;
            _fillColor = Color.White;
            _borderThickness = 1;
        }

        #region Properties

        private readonly ComponentViewModelFactory _viewModelFactory = new();
        private Color _fillColor = Color.White;
        private Color _borderColor = Color.Black;
        private int _borderThickness = 1;
        private Primitive _primitive;

        #endregion


        #region Ctor

        public ComponentBuilder(ComponentViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public ComponentBuilder() { }

        #endregion
    }
}