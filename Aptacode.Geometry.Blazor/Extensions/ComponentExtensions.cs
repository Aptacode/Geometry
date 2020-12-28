using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives.Extensions;

namespace Aptacode.Geometry.Blazor.Extensions
{
    public static class ComponentExtensions
    {
        public static BoundingRectangle Combine(this BoundingRectangle rectangle1, BoundingRectangle rectangle2)
        {
            var minX = rectangle1.TopLeft.X < rectangle2.TopLeft.X ? rectangle1.TopLeft.X : rectangle2.TopLeft.X;
            var minY = rectangle1.TopLeft.Y < rectangle2.TopLeft.Y ? rectangle1.TopLeft.Y : rectangle2.TopLeft.Y;
            var maxX = rectangle1.BottomRight.X > rectangle2.BottomRight.X
                ? rectangle1.BottomRight.X
                : rectangle2.BottomRight.X;
            var maxY = rectangle1.BottomRight.Y > rectangle2.BottomRight.Y
                ? rectangle1.BottomRight.Y
                : rectangle2.BottomRight.Y;

            return BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        public static BoundingRectangle ToBoundingRectangle(this IEnumerable<ComponentViewModel> components)
        {
            var maxX = 0.0f;
            var maxY = 0.0f;
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            foreach (var component in components)
            {
                var boundingRectangle = component.BoundingRectangle;
                if (boundingRectangle.TopLeft.X < minX)
                {
                    minX = boundingRectangle.TopLeft.X;
                }

                if (boundingRectangle.TopLeft.Y < minY)
                {
                    minY = boundingRectangle.TopLeft.Y;
                }

                if (boundingRectangle.BottomRight.X > maxX)
                {
                    maxX = boundingRectangle.BottomRight.X;
                }

                if (boundingRectangle.BottomRight.Y > maxY)
                {
                    maxY = boundingRectangle.BottomRight.Y;
                }
            }

            return BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        #region CollisionDetection

        public static IEnumerable<ComponentViewModel> CollidingWith(this IEnumerable<ComponentViewModel> components,
            Vector2 point, CollisionDetector collisionDetector)
        {
            return components.Where(c =>
                c.CollisionDetectionEnabled && c.CollidesWith(point.ToPoint(), collisionDetector));
        }

        public static IEnumerable<ComponentViewModel> CollidingWith(this IEnumerable<ComponentViewModel> components,
            ComponentViewModel component, CollisionDetector collisionDetector)
        {
            return components.Where(c => c.CollisionDetectionEnabled && c.CollidesWith(component, collisionDetector));
        }

        #endregion
    }
}