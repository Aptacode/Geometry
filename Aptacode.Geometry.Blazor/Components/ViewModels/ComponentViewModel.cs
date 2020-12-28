using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;
using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class ComponentViewModel
    {
        #region Ctor

        public ComponentViewModel()
        {
            Id = Guid.NewGuid();
            Primitives = new List<Primitive>();
            Children = new List<ComponentViewModel>();

            CollisionDetectionEnabled = true;
            BorderColor = Color.Black;
            FillColor = Color.Black;
            BorderThickness = DefaultBorderThickness;
            Margin = DefaultMargin;
            Invalidated = true;
            IsShown = true;

            UpdateBoundingRectangle();
            OldBoundingRectangle = BoundingRectangle;
        }

        #endregion

        #region Canvas

        public async Task Draw(IContext2DWithoutGetters ctx)
        {
            OldBoundingRectangle = BoundingRectangle;
            Invalidated = false;

            if (!IsShown)
            {
                return;
            }

            await ctx.FillStyleAsync(FillColorName);

            await ctx.StrokeStyleAsync(BorderColorName);

            await ctx.LineWidthAsync(BorderThickness);

            foreach (var primitive in Primitives)
            {
                await primitive.Draw(ctx);
            }

            foreach (var child in Children)
            {
                await child.Draw(ctx);
            }

            if (!string.IsNullOrEmpty(Text))
            {
                await ctx.TextAlignAsync(TextAlign.Center);
                await ctx.FillStyleAsync("black");
                await ctx.FillTextAsync(Text, BoundingRectangle.Center.X, BoundingRectangle.Center.Y);
            }
        }

        #endregion

        #region Children

        public List<Primitive> Primitives { get; }
        public List<ComponentViewModel> Children { get; }

        public void UpdateBoundingRectangle()
        {
            var primitiveBoundingRectangle = Primitives.ToBoundingRectangle().AddMargin(Margin);
            var childrenBoundingRectangle = Children.ToBoundingRectangle().AddMargin(Margin);

            BoundingRectangle = primitiveBoundingRectangle.Combine(childrenBoundingRectangle);
        }

        public void Add(Primitive primitive)
        {
            Primitives.Add(primitive);
            UpdateBoundingRectangle();
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
        public static readonly int DefaultBorderThickness = 1;
        public static readonly float DefaultMargin = 0.0f;

        #endregion

        #region Properties

        public Guid Id { get; init; }

        public BoundingRectangle OldBoundingRectangle { get; private set; }
        public BoundingRectangle BoundingRectangle { get; private set; }

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

        public string BorderColorName { get; set; }

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

        public bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector)
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

            foreach (var primitive in Primitives)
            {
                if (component.CollidesWith(primitive, collisionDetector))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector)
        {
            if (!primitive.BoundingRectangle.CollidesWith(BoundingRectangle))
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

            foreach (var p in Primitives)
            {
                if (p.CollidesWith(primitive, collisionDetector))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CollidesWith(Vector2 point, CollisionDetector collisionDetector)
        {
            if (!BoundingRectangle.Contains(point))
            {
                return false;
            }

            var p = point.ToPoint();

            foreach (var child in Children)
            {
                if (child.CollidesWith(point, collisionDetector))
                {
                    return true;
                }
            }

            foreach (var primitive in Primitives)
            {
                if (primitive.CollidesWith(p, collisionDetector))
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
            foreach (var primitive in Primitives)
            {
                primitive.Translate(delta);
            }

            foreach (var child in Children)
            {
                child.Translate(delta);
            }

            UpdateBoundingRectangle();

            Invalidated = true;
        }

        public virtual void Rotate(float theta)
        {
            foreach (var primitive in Primitives)
            {
                primitive.Rotate(theta);
            }

            foreach (var child in Children)
            {
                child.Rotate(theta);
            }

            UpdateBoundingRectangle();
            Invalidated = true;
        }

        public virtual void Rotate(Vector2 rotationCenter, float theta)
        {
            foreach (var primitive in Primitives)
            {
                primitive.Rotate(rotationCenter, theta);
            }

            foreach (var child in Children)
            {
                child.Rotate(rotationCenter, theta);
            }

            UpdateBoundingRectangle();
            Invalidated = true;
        }

        public virtual void Scale(Vector2 delta)
        {
            foreach (var primitive in Primitives)
            {
                primitive.Scale(delta);
            }

            foreach (var child in Children)
            {
                child.Scale(delta);
            }

            UpdateBoundingRectangle();
            Invalidated = true;
        }

        public virtual void Skew(Vector2 delta)
        {
            foreach (var primitive in Primitives)
            {
                primitive.Skew(delta);
            }

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