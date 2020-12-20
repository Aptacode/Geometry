using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Extensions;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public class DemoSceneController : SceneControllerViewModel
    {
        public DemoSceneController(SceneViewModel scene) : base(scene)
        {
            UserInteractionController.OnMouseDown += UserInteractionControllerOnOnMouseDown;
            UserInteractionController.OnMouseUp += UserInteractionControllerOnOnMouseUp;
            UserInteractionController.OnMouseMoved += UserInteractionControllerOnOnMouseMoved;
            Start();
        }

        public ComponentViewModel SelectedComponent { get; set; }

        public bool Running { get; set; }
        public void Start()
        {
            var lastTick = DateTime.Now;
            new TaskFactory().StartNew(async () =>
            {
                Running = true;
                while (Running)
                {
                    await Task.Delay(1);
                    var currentTime = DateTime.Now;
                    var delta = currentTime - lastTick;
                    lastTick = currentTime;
                    
                    await Scene.RedrawAsync();
                    var frameRate = 1.0f / delta.TotalSeconds;
                    Console.WriteLine($"{frameRate}fps");
                }
            });
        }
        

        private void UserInteractionControllerOnOnMouseMoved(object? sender, Vector2 e)
        {
            if (SelectedComponent == null)
            {
                return;
            }

            var delta = e - UserInteractionController.LastMousePosition;

            Translate(SelectedComponent, delta, new List<ComponentViewModel> {SelectedComponent},
                new CancellationTokenSource());
        }

        private void UserInteractionControllerOnOnMouseUp(object? sender, Vector2 e)
        {
            foreach (var componentViewModel in Scene.Components)
            {
                componentViewModel.BorderColor = Color.Black;
            }

            SelectedComponent = null;
        }

        private void UserInteractionControllerOnOnMouseDown(object? sender, Vector2 e)
        {
            SelectedComponent = null;

            foreach (var componentViewModel in Scene.Components.CollidingWith(e, CollisionDetector))
            {
                SelectedComponent = componentViewModel;
                componentViewModel.BorderColor = Color.Green;
            }

            Scene.BringToFront(SelectedComponent);
        }
    }
}