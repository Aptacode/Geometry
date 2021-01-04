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
    public class PolylineViewModel : ComponentViewModel
    {
        private PolyLine _polyLine;

        public PolylineViewModel(PolyLine polyLine)
        {
            PolyLine = polyLine;
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(ConvexHull.BoundingRectangle);
        }

        public PolyLine PolyLine
        {
            get => _polyLine;
            set
            {
                _polyLine = value;
                ConvexHull = new Polygon(_polyLine.Vertices.ToConvexHull(Margin));
            }
        }

        public Polygon ConvexHull { get; set; }

        public override async Task CustomDraw(IJSUnmarshalledRuntime ctx)
        {
            ctx.beginPath();
            ctx.moveTo(PolyLine.Vertices[0].X, PolyLine.Vertices[0].Y);
            for (var i = 1; i < PolyLine.Vertices.Length; i++)
            {
                var vertex = PolyLine.Vertices[i];
                ctx.lineTo(vertex.X, vertex.Y);
            }

            ctx.Stroke();
        }

        #region Transformations

        public override void Translate(Vector2 delta)
        {
            PolyLine.Translate(delta);
            ConvexHull.Translate(delta);

            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            PolyLine.Scale(delta);
            ConvexHull = new Polygon(_polyLine.Vertices.ToConvexHull(Margin));

            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            PolyLine.Rotate(theta);
            ConvexHull = new Polygon(_polyLine.Vertices.ToConvexHull(Margin));

            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            PolyLine.Rotate(rotationCenter, theta);
            ConvexHull = new Polygon(_polyLine.Vertices.ToConvexHull(Margin));

            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            PolyLine.Skew(delta);
            ConvexHull = new Polygon(_polyLine.Vertices.ToConvexHull(Margin));

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