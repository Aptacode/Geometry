using System;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;
using Microsoft.JSInterop;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public class PointViewModel : ComponentViewModel
    {
        private Point _point;

        public PointViewModel(Point point)
        {
            Point = point;
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(ConvexHull.BoundingRectangle);
        }

        public Point Point
        {
            get => _point;
            set
            {
                _point = value;
                ConvexHull = new Polygon(_point.Vertices.ToConvexHull(Margin));
            }
        }

        public Polygon ConvexHull { get; set; }

        public override async Task CustomDraw(IJSUnmarshalledRuntime ctx)
        {
            ctx.beginPath();
            ctx.ellipse((int) Point.Position.X, (int) Point.Position.Y, 1, 1, 0, 0, 2 * (float) Math.PI);
            ctx.Fill();
            ctx.Stroke();
        }

        #region Transformations

        public override void Translate(Vector2 delta)
        {
            Point.Translate(delta);
            ConvexHull.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Point.Scale(delta);
            ConvexHull.Scale(delta);
            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Point.Rotate(theta);
            ConvexHull.Rotate(theta);
            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Point.Rotate(rotationCenter, theta);
            ConvexHull.Rotate(rotationCenter, theta);
            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Point.Skew(delta);
            ConvexHull.Skew(delta);
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public override BoundingRectangle UpdateBoundingRectangle()
        {
            BoundingRectangle = base.UpdateBoundingRectangle().Combine(ConvexHull.BoundingRectangle);
            return BoundingRectangle;
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector)
        {
            return component.CollidesWith(ConvexHull, collisionDetector) ||
                   base.CollidesWith(component, collisionDetector);
        }

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector)
        {
            return primitive.CollidesWith(ConvexHull, collisionDetector) ||
                   base.CollidesWith(primitive, collisionDetector);
        }

        #endregion
    }
}