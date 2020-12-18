using System;
using System.Drawing;
using Aptacode.CSharp.Common.Utilities.Mvvm;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public abstract class ComponentViewModel : BindableBase
    {
        protected ComponentViewModel(Primitive primitive)
        {
            Primitive = primitive;
        }

        public event EventHandler Redraw;

        protected virtual void OnRedraw()
        {
            Redraw?.Invoke(this, EventArgs.Empty);
        }

        #region Properties

        private readonly Color _fillColor = Color.Gray;
        private Color _borderColor = Color.Black;
        private float _borderThickness = 1;
        private bool _isShown = true;
        private float _margin;

        protected Primitive _primitive;

        public Primitive Primitive
        {
            get => _primitive;
            set => SetProperty(ref _primitive, value);
        }

        public float Margin
        {
            get => _margin;
            set => SetProperty(ref _margin, value);
        }

        public bool IsShown
        {
            get => _isShown;
            set => SetProperty(ref _isShown, value);
        }

        public Color BorderColor
        {
            get => _borderColor;
            set => SetProperty(ref _borderColor, value);
        }

        public Color FillColor
        {
            get => _fillColor;
            set => SetProperty(ref _borderColor, value);
        }

        public float BorderThickness
        {
            get => _borderThickness;
            set => SetProperty(ref _borderThickness, value);
        }

        #endregion
    }
}