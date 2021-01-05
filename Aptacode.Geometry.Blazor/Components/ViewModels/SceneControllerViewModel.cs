using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Utilities.Mvvm;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;
using Aptacode.Geometry.Collision;
using Microsoft.JSInterop;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class SceneControllerViewModel : BindableBase
    {
        public SceneControllerViewModel(SceneViewModel scene)
        {
            Scene = scene;
            CollisionDetector = new HybridCollisionDetector();
            UserInteractionController = new UserInteractionController();
        }

        #region Movement

        public void Translate(
            ComponentViewModel component,
            Vector2 delta,
            List<ComponentViewModel> movingComponents,
            CancellationTokenSource cancellationToken)
        {
            var unselectedItems = Scene.Components.Except(movingComponents).Where(c => c.CollisionDetectionEnabled);

            component.Translate(delta);

            var collidingItems = unselectedItems
                .Where(i => i.CollidesWith(component, CollisionDetector)).ToList();

            movingComponents.AddRange(collidingItems);

            foreach (var collidingItem in collidingItems)
            {
                Translate(collidingItem, delta, movingComponents, cancellationToken);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                component.Translate(-delta);
            }
        }

        #endregion

        #region Events

        public virtual async Task Tick()
        {
            await Scene.RedrawAsync();
        }

        #endregion

        public void SetRunTime(IJSUnmarshalledRuntime jsRuntime)
        {
            Scene.JSUnmarshalledRuntime = jsRuntime;
        }

        #region Properties

        public SceneViewModel Scene { get; set; }
        public UserInteractionController UserInteractionController { get; set; }
        public CollisionDetector CollisionDetector { get; set; }

        public string Cursor { get; set; }

        #endregion
    }
}