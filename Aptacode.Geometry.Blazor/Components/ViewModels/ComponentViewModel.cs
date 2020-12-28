using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class ComponentViewModel
    {
        public ComponentViewModel(IEnumerable<Primitive> primitive)
        {
            Id = Guid.NewGuid();
            Primitives = primitive.ToList();
            OldBoundingRectangle = BoundingRectangle = Primitives.ToBoundingRectangle();
            CollisionDetectionEnabled = true;
            BorderColor = Color.Black;
            FillColor = Color.Black;
            BorderThickness = DefaultBorderThickness;
            Invalidated = true;
        }

        #region Canvas

        public async Task Draw(IContext2DWithoutGetters ctx)
        {
            await ctx.FillStyleAsync(FillColorName);

            await ctx.StrokeStyleAsync(BorderColorName);

            await ctx.LineWidthAsync(BorderThickness);

            foreach (var primitive in Primitives)
            {
                await primitive.Draw(ctx);
            }

            OldBoundingRectangle = BoundingRectangle;
            BoundingRectangle = Primitives.ToBoundingRectangle();
            Invalidated = false;
        }

        #endregion

        #region Properties

        public static readonly string DefaultBorderColor = Color.Black.ToKnownColor().ToString();
        public static readonly string DefaultFillColor = Color.Black.ToKnownColor().ToString();
        public static readonly int DefaultBorderThickness = 1;

        public Guid Id { get; init; }

        public BoundingRectangle OldBoundingRectangle { get; private set; }
        public BoundingRectangle BoundingRectangle { get; private set; }
        public List<Primitive> Primitives { get; }

        public float Margin { get; set; }

        public bool IsShown { get; set; }

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
            //ToDo check Bounding rectangle collision first
            foreach (var primitive in Primitives)
            {
                foreach (var componentPrimitive in component.Primitives)
                {
                    if (primitive.CollidesWith(componentPrimitive, collisionDetector))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CollidesWith(Vector2 point, CollisionDetector collisionDetector)
        {
            var p = point.ToPoint();
            //ToDo check Bounding rectangle collision first
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

            Invalidated = true;
        }

        public virtual void Rotate(float theta)
        {
            foreach (var primitive in Primitives)
            {
                primitive.Rotate(theta);
            }

            Invalidated = true;
        }

        public virtual void Rotate(Vector2 rotationCenter, float theta)
        {
            foreach (var primitive in Primitives)
            {
                primitive.Rotate(rotationCenter, theta);
            }

            Invalidated = true;
        }

        public virtual void Scale(Vector2 delta)
        {
            foreach (var primitive in Primitives)
            {
                primitive.Scale(delta);
            }

            Invalidated = true;
        }

        public virtual void Skew(Vector2 delta)
        {
            foreach (var primitive in Primitives)
            {
                primitive.Skew(delta);
            }

            Invalidated = true;
        }

        #endregion
    }
}