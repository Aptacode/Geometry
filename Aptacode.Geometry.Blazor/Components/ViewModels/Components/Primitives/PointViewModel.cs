using System;
using System.Numerics;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Aptacode.BlazorCanvas;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public class PointViewModel : ComponentViewModel
    {
        #region Ctor

        public PointViewModel(Point point)
        {
            Point = point;
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(BoundingPrimitive.BoundingRectangle);
        }

        #endregion

        #region Canvas

        public override async Task CustomDraw(BlazorCanvasInterop ctx)
        {
            ctx.BeginPath();
            ctx.Ellipse((int) Point.Position.X * SceneScale.Value, (int) Point.Position.Y * SceneScale.Value, 1 * SceneScale.Value, 1 * SceneScale.Value, 0, 0, 2 * (float) Math.PI);
            ctx.Fill();
            ctx.Stroke();
        }

        #endregion

        #region Props

        private Point _point;

        public Point Point
        {
            get => _point;
            set
            {
                _point = value;
                UpdateMargin();
                Invalidated = true;
            }
        }

        public override void UpdateMargin()
        {
            if (_point == null)
            {
                return;
            }

            BoundingPrimitive = new Ellipse(_point.Position, new Vector2(Margin, Margin), 0);
        }

        #endregion

        #region Transformations

        public override void Translate(Vector2 delta)
        {
            Point.Translate(delta);
            BoundingPrimitive.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Point.Scale(delta);
            BoundingPrimitive.Scale(delta);
            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Point.Rotate(theta);
            BoundingPrimitive.Rotate(theta);
            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Point.Rotate(rotationCenter, theta);
            BoundingPrimitive.Rotate(rotationCenter, theta);
            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Point.Skew(delta);
            BoundingPrimitive.Skew(delta);
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public override BoundingRectangle UpdateBoundingRectangle()
        {
            BoundingRectangle = base.UpdateBoundingRectangle().Combine(BoundingPrimitive.BoundingRectangle);
            return BoundingRectangle;
        }


        public override bool CollidesWith(ComponentViewModel component)
        {
            return base.CollidesWith(component) || component.CollidesWith(Point);
        }
        public override bool CollidesWith(Primitive component)
        {
            return base.CollidesWith(component) || Point.CollidesWith(component);
        }

        public override bool CollidesWith(Vector2 point)
        {
            return base.CollidesWith(point) || Point.Contains(point);
        }

        #endregion
    }
}