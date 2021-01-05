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
                MarginPolygon = new Polygon(_polygon.Vertices.ToConvexHull(Margin));
            }
        }

        public Polygon MarginPolygon { get; set; }

        #endregion

        #region Canvas

        public PolygonViewModel(Polygon polygon)
        {
            Polygon = polygon;
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(MarginPolygon.BoundingRectangle);
        }

        #endregion

        #region Transformations

        public override void Translate(Vector2 delta)
        {
            Polygon.Translate(delta);
            MarginPolygon.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Polygon.Scale(delta);
            MarginPolygon = new Polygon(_polygon.Vertices.ToConvexHull(Margin));

            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Polygon.Rotate(theta);
            MarginPolygon = new Polygon(_polygon.Vertices.ToConvexHull(Margin));

            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Polygon.Rotate(rotationCenter, theta);
            MarginPolygon = new Polygon(_polygon.Vertices.ToConvexHull(Margin));

            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Polygon.Skew(delta);
            MarginPolygon = new Polygon(_polygon.Vertices.ToConvexHull(Margin));
            base.Skew(delta);
        }

        #endregion

        #region Collision

        public override BoundingRectangle UpdateBoundingRectangle()
        {
            BoundingRectangle = base.UpdateBoundingRectangle().Combine(MarginPolygon.BoundingRectangle);
            return BoundingRectangle;
        }

        public override bool CollidesWith(ComponentViewModel component, CollisionDetector collisionDetector)
        {
            return component.CollidesWith(MarginPolygon, collisionDetector) ||
                   base.CollidesWith(component, collisionDetector);
        }

        public override bool CollidesWith(Primitive primitive, CollisionDetector collisionDetector)
        {
            return primitive.CollidesWith(MarginPolygon, collisionDetector) ||
                   base.CollidesWith(primitive, collisionDetector);
        }

        #endregion
    }
}