using System;
using System.Drawing;
using System.Numerics;
using Aptacode.CSharp.Common.Utilities.Mvvm;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public abstract class ComponentViewModel : BindableBase
    {
        protected ComponentViewModel(Primitive primitive)
        {
            Id = Guid.NewGuid();
            _primitive = primitive;
            CollisionDetectionEnabled = true;
        }

        #region Events

        public event EventHandler OnRedraw;

        protected virtual void Redraw()
        {
            OnRedraw?.Invoke(this, EventArgs.Empty);
        }

        #endregion


        #region Properties

        private readonly Color _fillColor = Color.Gray;
        private Color _borderColor = Color.Black;
        private float _borderThickness = 1;
        private bool _isShown = true;
        private float _margin;

        public Guid Id { get; init; }

        protected Primitive _primitive;

        public Primitive Primitive => _primitive;

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

        #region CollisionDetection

        public bool CollisionDetectionEnabled { get; set; }

        public bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector) =>
            Primitive.CollidesWith(component.Primitive, collisionDetector);

        public bool CollidesWith(Vector2 point, CollisionDetector collisionDetector) =>
            Primitive.CollidesWith(point.ToPoint(), collisionDetector);

        #endregion

        #region Transformation

        public abstract void Translate(Vector2 delta);

        public abstract void Rotate(float theta);

        public abstract void Rotate(Vector2 rotationCenter, float theta);

        public abstract void Scale(Vector2 delta);

        public abstract void Skew(Vector2 delta);

        #endregion
    }
}