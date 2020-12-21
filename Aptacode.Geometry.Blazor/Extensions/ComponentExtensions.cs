using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Collision;

namespace Aptacode.Geometry.Blazor.Extensions
{
    public static class ComponentExtensions
    {
        #region CollisionDetection

        public static IEnumerable<ComponentViewModel> CollidingWith(this IEnumerable<ComponentViewModel> components,
            Vector2 point, CollisionDetector collisionDetector)
        {
            return components.Where(c => c.CollisionDetectionEnabled && c.CollidesWith(point, collisionDetector));
        }

        public static IEnumerable<ComponentViewModel> CollidingWith(this IEnumerable<ComponentViewModel> components,
            ComponentViewModel component, CollisionDetector collisionDetector)
        {
            return components.Where(c => c.CollisionDetectionEnabled && c.CollidesWith(component, collisionDetector));
        }

        #endregion
    }
}