using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.BlazorCanvas;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives
{
    public class PolygonViewModel : ComponentViewModel
    {
        #region Canvas

        public PolygonViewModel(Polygon polygon)
        {
            Polygon = polygon;
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(BoundingPrimitive.BoundingRectangle);
        }

        #endregion

        #region Ctor

        public override async Task CustomDraw(BlazorCanvasInterop ctx)
        {
            var vertices = new Vector2[Polygon.Vertices.Length];
            for (var i = 0; i < Polygon.Vertices.Length; i++)
            {
                vertices[i] = Polygon.Vertices[i] * SceneScale.Value;
            }
            ctx.Polygon(vertices);
            
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
                Invalidated = true;
            }
        }

        public override void UpdateMargin()
        {
            if (_polygon == null)
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

        public override bool CollidesWith(ComponentViewModel component)
        {
            return base.CollidesWith(component) && component.CollidesWith(Polygon);
        }
        public override bool CollidesWith(Primitive component)
        {
            return base.CollidesWith(component) && Polygon.CollidesWith(component);
        }

        public override bool CollidesWith(Vector2 point)
        {
            return base.CollidesWith(point) && Polygon.Contains(point);
        }

        #endregion
    }
}