using System;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Microsoft.JSInterop;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public class EllipseViewModel : ComponentViewModel
    {
        private Ellipse _ellipse;

        #region Ctor

        public EllipseViewModel(Ellipse ellipse)
        {
            Ellipse = ellipse;
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(EllipseBorder.BoundingRectangle);
        }

        #endregion

        public Ellipse Ellipse
        {
            get => _ellipse;
            set
            {
                _ellipse = value;
                EllipseBorder = new Ellipse(_ellipse.Position, _ellipse.Radii + new Vector2(Margin, Margin),
                    _ellipse.Rotation);
            }
        }

        public Ellipse EllipseBorder { get; set; }

        public override async Task CustomDraw(IJSUnmarshalledRuntime ctx)
        {
            ctx.beginPath();

            ctx.ellipse((int) Ellipse.Position.X, (int) Ellipse.Position.Y, (int) Ellipse.Radii.X,
                (int) Ellipse.Radii.Y, Ellipse.Rotation, 0, 2.0f * (float) Math.PI);
            ctx.Fill();
            ctx.Stroke();
        }

        #region Transformations

        public override void Translate(Vector2 delta)
        {
            Ellipse.Translate(delta);
            EllipseBorder.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Ellipse.Scale(delta);
            EllipseBorder.Scale(delta);
            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Ellipse.Rotate(theta);
            EllipseBorder.Rotate(theta);
            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Ellipse.Rotate(rotationCenter, theta);
            EllipseBorder.Rotate(rotationCenter, theta);
            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Ellipse.Skew(delta);
            EllipseBorder.Skew(delta);
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public override BoundingRectangle UpdateBoundingRectangle()
        {
            BoundingRectangle = base.UpdateBoundingRectangle().Combine(EllipseBorder.BoundingRectangle);
            return BoundingRectangle;
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector)
        {
            return component.CollidesWith(EllipseBorder, collisionDetector) ||
                   base.CollidesWith(component, collisionDetector);
        }

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector)
        {
            return primitive.CollidesWith(EllipseBorder, collisionDetector) ||
                   base.CollidesWith(primitive, collisionDetector);
        }

        #endregion
    }
}