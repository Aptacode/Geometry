using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.BlazorCanvas;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components
{
    public class ComponentViewModel
    {
        #region Ctor

        public ComponentViewModel()
        {
            Id = Guid.NewGuid();
            CollisionDetectionEnabled = true;
            Margin = DefaultMargin;
            IsShown = true;
            BorderThickness = DefaultBorderThickness;
            BorderColor = Color.Black;
            FillColor = Color.White;
            OldBoundingRectangle = BoundingRectangle = _children.ToBoundingRectangle();
            Invalidated = true;
        }

        #endregion

        #region Canvas

        public virtual async Task CustomDraw(BlazorCanvasInterop ctx)
        {
        }
        public virtual async Task DrawText(BlazorCanvasInterop ctx)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                ctx.TextAlign("center");
                ctx.FillStyle("black");
                ctx.Font("10pt Calibri");
                ctx.WrapText(Text, BoundingRectangle.TopLeft.X * SceneScale.Value, BoundingRectangle.TopLeft.Y * SceneScale.Value, BoundingRectangle.Size.X * SceneScale.Value, BoundingRectangle.Size.Y * SceneScale.Value, 16);
            }
        }

        public virtual async Task Draw(BlazorCanvasInterop ctx)
        {
            OldBoundingRectangle = BoundingRectangle;
            Invalidated = false;

            if (!IsShown)
            {
                return;
            }

            ctx.FillStyle(FillColorName);

            ctx.StrokeStyle(BorderColorName);

            ctx.LineWidth(BorderThickness * SceneScale.Value);

            await CustomDraw(ctx);

            foreach (var child in _children)
            {
                await child.Draw(ctx);
            }

            await DrawText(ctx);
        }

        #endregion

        #region Prop
        
        private readonly List<ComponentViewModel> _children = new();
        public bool CollisionDetectionEnabled { get; set; }
        public bool Invalidated { get; set; }

        #endregion

        #region Children

        public IEnumerable<ComponentViewModel> Children => _children;

        public virtual BoundingRectangle UpdateBoundingRectangle()
        {
            BoundingRectangle = _children.ToBoundingRectangle();
            return BoundingRectangle;
        }

        public void Add(ComponentViewModel child)
        {
            _children.Add(child);
            UpdateBoundingRectangle();
            Invalidated = true;
        }

        public void AddRange(IEnumerable<ComponentViewModel> children)
        {
            foreach (var child in children)
            {
                _children.Add(child);
            }

            UpdateBoundingRectangle();
            Invalidated = true;
        }

        public void Remove(ComponentViewModel child)
        {
            _children.Add(child);
            UpdateBoundingRectangle();
            Invalidated = true;
        }

        #endregion

        #region Defaults

        public static readonly string DefaultBorderColor = Color.Black.ToKnownColor().ToString();
        public static readonly string DefaultFillColor = Color.Black.ToKnownColor().ToString();
        public static readonly float DefaultBorderThickness = 0.1f;
        public static readonly float DefaultMargin = 1.0f;

        #endregion

        #region Properties

        public Guid Id { get; init; }

        public BoundingRectangle OldBoundingRectangle { get; protected set; }
        public BoundingRectangle BoundingRectangle { get; protected set; }
        public Primitive BoundingPrimitive { get; set; }

        private float _margin;

        public float Margin
        {
            get => _margin;
            set
            {
                _margin = value;
                UpdateMargin();
                Invalidated = true;
            }
        }

        private bool _isShown;
        public bool IsShown
        {
            get => _isShown;
            set
            {
                _isShown = value;
                Invalidated = true;
            }
        }
        
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                Invalidated = true;
            }
        }

        private Color _borderColor;
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                BorderColorName = value.ToKnownColor().ToString();
                Invalidated = true;
            }
        }

        public string? BorderColorName { get; private set; }

        private Color _fillColor;
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                FillColorName = value.ToKnownColor().ToString();
                Invalidated = true;
            }
        }

        public string FillColorName { get; private set; }
        
        private float _borderThickness;
        public float BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = value;
                Invalidated = true;
            }
        }

        #endregion

        #region CollisionDetection

        public virtual void UpdateMargin()
        {
        }

        public virtual bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector)
        {
            if (!component.BoundingRectangle.CollidesWith(BoundingRectangle))
            {
                return false;
            }

            foreach (var child in Children)
            {
                if (child.CollidesWith(component, collisionDetector))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector)
        {
            if (!BoundingRectangle.CollidesWith(primitive.BoundingRectangle))
            {
                return false;
            }

            foreach (var child in Children)
            {
                if (child.CollidesWith(primitive, collisionDetector))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Transformation

        public virtual void Translate(Vector2 delta)
        {
            foreach (var child in Children)
            {
                child.Translate(delta);
            }

            UpdateBoundingRectangle();

            Invalidated = true;
        }

        public virtual void Rotate(float theta)
        {
            foreach (var child in Children)
            {
                child.Rotate(theta);
            }

            UpdateBoundingRectangle();
            Invalidated = true;
        }

        public virtual void Rotate(Vector2 rotationCenter, float theta)
        {
            foreach (var child in Children)
            {
                child.Rotate(rotationCenter, theta);
            }

            UpdateBoundingRectangle();
            Invalidated = true;
        }

        public virtual void Scale(Vector2 delta)
        {
            foreach (var child in Children)
            {
                child.Scale(delta);
            }

            UpdateBoundingRectangle();
            Invalidated = true;
        }

        public virtual void Skew(Vector2 delta)
        {
            foreach (var child in Children)
            {
                child.Skew(delta);
            }

            UpdateBoundingRectangle();
            Invalidated = true;
        }

        #endregion
    }
}