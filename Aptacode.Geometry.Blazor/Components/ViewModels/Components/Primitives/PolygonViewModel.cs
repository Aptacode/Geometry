using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.BlazorCanvas;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;
using Microsoft.JSInterop;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public class PolygonViewModel : ComponentViewModel
    {
        #region Ctor

        public override async Task CustomDraw(BlazorCanvasInterop ctx)
        {
            ctx.BeginPath();

            ctx.MoveTo((int)Polygon.Vertices[0].X, (int)Polygon.Vertices[0].Y);
            for (var i = 1; i < Polygon.Vertices.Length; i++)
            {
                var vertex = Polygon.Vertices[i];
                ctx.LineTo((int)vertex.X, (int)vertex.Y);
            }

            ctx.ClosePath();
            ctx.Fill();
            ctx.Stroke();
        }
        
        #endregion

        #region Props

        private Polygon _polygon;
        public Polygon Polygon
        {
            get => _polygon;
            set
            {
                _polygon = value;
                UpdateMargin();
            }
        }
        public override void UpdateMargin()
        {
            if(_polygon == null)
            {
                return;
            }
            
            if (Margin > Constants.Tolerance)
            {
                BoundingPrimitive = new Polygon(_polygon.Vertices.ToConvexHull(Margin));
            }
            else
            {
                BoundingPrimitive = Polygon.Create(_polygon.Vertices.Vertices.ToArray());
            }
        }
        #endregion

        #region Canvas

        public PolygonViewModel(Polygon polygon)
        {
            Polygon = polygon;
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(BoundingPrimitive.BoundingRectangle);
        }

        #endregion

        #region Transformations

        public override void Translate(Vector2 delta)
        {
            Polygon.Translate(delta);
            BoundingPrimitive.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Polygon.Scale(delta);
            UpdateMargin();

            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Polygon.Rotate(theta);
            UpdateMargin();

            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Polygon.Rotate(rotationCenter, theta);
            UpdateMargin();

            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Polygon.Skew(delta);
            UpdateMargin();
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public override BoundingRectangle UpdateBoundingRectangle()
        {
            BoundingRectangle = base.UpdateBoundingRectangle().Combine(BoundingPrimitive.BoundingRectangle);
            return BoundingRectangle;
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector)
        {
            return component.CollidesWith(BoundingPrimitive, collisionDetector) ||
                   base.CollidesWith(component, collisionDetector);
        }

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector)
        {
            return primitive.CollidesWith(BoundingPrimitive, collisionDetector) ||
                   base.CollidesWith(primitive, collisionDetector);
        }

        #endregion
    }
}