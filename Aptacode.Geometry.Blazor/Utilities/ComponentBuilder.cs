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
        public ComponentBuilder SetText(string text)
        {
            _text = text;
            return this;
        }

        public ComponentBuilder SetFillColor(Color fillColor)
        {
            _fillColor = fillColor;
            return this;
        }

        public ComponentBuilder SetMargin(float margin)
        {
            _margin = margin;
            return this;
        }

        public ComponentBuilder AddPrimitive(Primitive primitive)
        {
            _primitives.Add(primitive);
            return this;
        }
        public ComponentBuilder AddChild(ComponentViewModel child)
        {
            _children.Add(child);
            return this;
        }

        public ComponentViewModel Build()
        {
            var component = new ComponentViewModel()
            {
                BorderColor = _borderColor,
                FillColor = _fillColor,
                BorderThickness = _borderThickness,
                Text = _text,
                Margin = _margin
            };

            component.Primitives.AddRange(_primitives);
            component.Children.AddRange(_children);
            component.UpdateBoundingRectangle();
            
            Reset();
            return component;
        }

        public void Reset()
        {
            _primitives.Clear();
            _children.Clear();
            _borderColor = Color.Black;
            _fillColor = Color.White;
            _borderThickness = 1;
            _margin = 0.0f;
            _text = "";
        }

        #region Properties

        private Color _fillColor = Color.White;
        private Color _borderColor = Color.Black;
        private int _borderThickness = 1;
        private float _margin = 0.0f;
        private string _text = "";
        private readonly List<Primitive> _primitives = new();
        private readonly List<ComponentViewModel> _children = new();

        #endregion
    }
}