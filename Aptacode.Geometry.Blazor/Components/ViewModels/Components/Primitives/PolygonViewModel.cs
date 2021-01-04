﻿using System.Numerics;
using System.Threading.Tasks;
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
        private Polygon _polygon;

        public PolygonViewModel(Polygon polygon)
        {
            Polygon = polygon;
            OldBoundingRectangle = BoundingRectangle =
                Children.ToBoundingRectangle().Combine(ConvexHull.BoundingRectangle);
        }

        public Polygon Polygon
        {
            get => _polygon;
            set
            {
                _polygon = value;
                ConvexHull = new Polygon(_polygon.Vertices.ToConvexHull(Margin));
            }
        }

        public Polygon ConvexHull { get; set; }

        public override async Task CustomDraw(IJSUnmarshalledRuntime ctx)
        {
            ctx.beginPath();

            ctx.moveTo((int) Polygon.Vertices[0].X, (int) Polygon.Vertices[0].Y);
            for (var i = 1; i < Polygon.Vertices.Length; i++)
            {
                var vertex = Polygon.Vertices[i];
                ctx.lineTo((int) vertex.X, (int) vertex.Y);
            }

            ctx.closePath();
            ctx.Fill();
            ctx.Stroke();
        }

        #region Transformations

        public override void Translate(Vector2 delta)
        {
            Polygon.Translate(delta);
            ConvexHull.Translate(delta);
            base.Translate(delta);
        }

        public override void Scale(Vector2 delta)
        {
            Polygon.Scale(delta);
            ConvexHull = new Polygon(_polygon.Vertices.ToConvexHull(Margin));

            base.Scale(delta);
        }

        public override void Rotate(float theta)
        {
            Polygon.Rotate(theta);
            ConvexHull = new Polygon(_polygon.Vertices.ToConvexHull(Margin));

            base.Rotate(theta);
        }

        public override void Rotate(Vector2 rotationCenter, float theta)
        {
            Polygon.Rotate(rotationCenter, theta);
            ConvexHull = new Polygon(_polygon.Vertices.ToConvexHull(Margin));

            base.Rotate(rotationCenter, theta);
        }

        public override void Skew(Vector2 delta)
        {
            Polygon.Skew(delta);
            ConvexHull = new Polygon(_polygon.Vertices.ToConvexHull(Margin));
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