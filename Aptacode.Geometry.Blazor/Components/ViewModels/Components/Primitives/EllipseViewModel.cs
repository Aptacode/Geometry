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

        #region Ctor

        public EllipseViewModel(Ellipse ellipse)
        {
            Ellipse = ellipse;
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(MarginEllipse.BoundingRectangle);
        }

        #endregion

        #region Props

        private Ellipse _ellipse;
        public Ellipse Ellipse
        {
            get => _ellipse;
            set
            {
                _ellipse = value;
                MarginEllipse = new Ellipse(_ellipse.Position, _ellipse.Radii + new Vector2(Margin, Margin),
                    _ellipse.Rotation);
            }
        }

        public Ellipse MarginEllipse { get; set; }

        #endregion

        #region Canvase

        public override async Task CustomDraw(IJSUnmarshalledRuntime ctx)
        {
            ctx.beginPath();

            ctx.ellipse((int)Ellipse.Position.X, (int)Ellipse.Position.Y, (int)Ellipse.Radii.X,
                (int)Ellipse.Radii.Y, Ellipse.Rotation, 0, 2.0f * (float)Math.PI);
            ctx.Fill();
            ctx.Stroke();
        }

        #endregion


        #region Transformations

        public override void Translate(Vector2 delta)
        {
            Ellipse.Translate(delta);
            MarginEllipse.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Ellipse.Scale(delta);
            MarginEllipse.Scale(delta);
            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Ellipse.Rotate(theta);
            MarginEllipse.Rotate(theta);
            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Ellipse.Rotate(rotationCenter, theta);
            MarginEllipse.Rotate(rotationCenter, theta);
            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Ellipse.Skew(delta);
            MarginEllipse.Skew(delta);
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public override BoundingRectangle UpdateBoundingRectangle()
        {
            BoundingRectangle = base.UpdateBoundingRectangle().Combine(MarginEllipse.BoundingRectangle);
            return BoundingRectangle;
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector)
        {
            return component.CollidesWith(MarginEllipse, collisionDetector) ||
                   base.CollidesWith(component, collisionDetector);
        }

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector)
        {
            return primitive.CollidesWith(MarginEllipse, collisionDetector) ||
                   base.CollidesWith(primitive, collisionDetector);
        }

        #endregion
    }
}