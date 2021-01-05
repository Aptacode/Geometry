using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Microsoft.JSInterop;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components
{
    public class ComponentViewModel
    {
        #region Ctor

        public ComponentViewModel()
        {
            Id = Guid.NewGuid();
            Children = new List<ComponentViewModel>();
            CollisionDetectionEnabled = true;
            Margin = DefaultMargin;
            Invalidated = true;
            IsShown = true;
            BorderThickness = DefaultBorderThickness;
            BorderColor = Color.Black;
            FillColor = Color.Black;
            OldBoundingRectangle = BoundingRectangle = Children.ToBoundingRectangle();
        }

        #endregion

        #region Canvas

        public virtual async Task CustomDraw(IJSUnmarshalledRuntime ctx)
        {
        }

        public virtual async Task Draw(IJSUnmarshalledRuntime ctx)
        {
            OldBoundingRectangle = BoundingRectangle;
            Invalidated = false;

            if (!IsShown)
            {
                return;
            }

            ctx.fillStyle(FillColorName);

            ctx.strokeStyle(BorderColorName);

            ctx.lineWidth(BorderThickness);


            await CustomDraw(ctx);

            foreach (var child in Children)
            {
                await child.Draw(ctx);
            }

            if (!string.IsNullOrEmpty(Text))
            {
                ctx.textAlign("center");
                ctx.fillStyle("black");
                ctx.fillText(Text, BoundingRectangle.Center.X, BoundingRectangle.Center.Y);
            }
        }

        #endregion

        #region Children

        public List<ComponentViewModel> Children { get; }

        public virtual BoundingRectangle UpdateBoundingRectangle()
        {
            BoundingRectangle = Children.ToBoundingRectangle();
            return BoundingRectangle;
        }

        public void Add(ComponentViewModel child)
        {
            Children.Add(child);
            UpdateBoundingRectangle();
        }

        #endregion

        #region Defaults

        public static readonly string DefaultBorderColor = Color.Black.ToKnownColor().ToString();
        public static readonly string DefaultFillColor = Color.Black.ToKnownColor().ToString();
        public static readonly int DefaultBorderThickness = 5;
        public static readonly float DefaultMargin = 20.0f;

        #endregion

        #region Properties

        public Guid Id { get; init; }

        public BoundingRectangle OldBoundingRectangle { get; protected set; }
        public BoundingRectangle BoundingRectangle { get; protected set; }

        public float Margin { get; set; }

        public bool IsShown { get; set; }

        public string Text { get; set; }

        private Color _borderColor;

        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                BorderColorName = value.ToKnownColor().ToString();
            }
        }

        public string? BorderColorName { get; set; }

        private Color _fillColor;

        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                FillColorName = value.ToKnownColor().ToString();
            }
        }

        public string FillColorName { get; set; }

        public int BorderThickness { get; set; }

        #endregion

        #region CollisionDetection

        public bool CollisionDetectionEnabled { get; set; }
        public bool Invalidated { get; set; }

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