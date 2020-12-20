using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class ComponentBuilder
    {
        private ComponentViewModelFactory _viewModelFactory = new();
        private Color _fillColor = Color.White;
        private Color _borderColor = Color.Black;
        private Primitive _primitive;

        #region Ctor

        public ComponentBuilder(ComponentViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public ComponentBuilder()
        {
            
        }
        
        #endregion


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

            Reset();
            return component;
        }

        public void Reset()
        {
            _borderColor = Color.Black;
            _fillColor = Color.White;
        }
    }
}