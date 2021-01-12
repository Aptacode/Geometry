using System.Collections.Generic;
using System.Drawing;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public class ComponentBuilder
    {
        public ComponentBuilder SetBorderThickness(float borderThickness)
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

        public ComponentBuilder SetBase(ComponentViewModel component)
        {
            _baseComponent = component;
            return this;
        }

        public ComponentBuilder AddChild(ComponentViewModel child)
        {
            _children.Add(child);
            return this;
        }

        public ComponentViewModel Build()
        {
            var component = _baseComponent;
            component.BorderColor = _borderColor;
            component.FillColor = _fillColor;
            component.BorderThickness = _borderThickness;
            component.Text = _text;
            component.Margin = _margin;

            component.AddRange(_children);
            component.UpdateBoundingRectangle();

            Reset();
            return component;
        }

        public void Reset()
        {
            _baseComponent = new ComponentViewModel();
            _children.Clear();
            _borderColor = Color.Black;
            _fillColor = Color.White;
            _borderThickness = ComponentViewModel.DefaultBorderThickness;
            _margin = ComponentViewModel.DefaultMargin;
            _text = "";
        }

        #region Ctor

        #endregion

        #region Properties

        private Color _fillColor = Color.White;
        private Color _borderColor = Color.Black;
        private float _borderThickness = ComponentViewModel.DefaultBorderThickness;
        private float _margin = ComponentViewModel.DefaultMargin;
        private string _text = "";
        private readonly List<ComponentViewModel> _children = new();
        private ComponentViewModel _baseComponent = new();

        #endregion
    }
}