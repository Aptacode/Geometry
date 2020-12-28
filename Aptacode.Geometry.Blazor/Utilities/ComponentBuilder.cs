using System.Collections.Generic;
using System.Drawing;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class ComponentBuilder
    {
        #region Ctor

        #endregion

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

        public ComponentBuilder AddPrimitive(Primitive primitive)
        {
            _primitives.Add(primitive);
            return this;
        }

        public ComponentViewModel Build()
        {
            var component = new ComponentViewModel(_primitives)
            {
                BorderColor = _borderColor,
                FillColor = _fillColor,
                BorderThickness = _borderThickness
            };

            Reset();
            return component;
        }

        public void Reset()
        {
            _primitives.Clear();
            _borderColor = Color.Black;
            _fillColor = Color.White;
            _borderThickness = 1;
        }

        #region Properties

        private Color _fillColor = Color.White;
        private Color _borderColor = Color.Black;
        private int _borderThickness = 1;
        private readonly List<Primitive> _primitives = new();

        #endregion
    }
}