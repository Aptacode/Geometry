using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives;
using Aptacode.Geometry.Blazor.Extensions;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Point = Aptacode.Geometry.Primitives.Point;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public enum ComponentType
    {
        None, Point, Line, Polygon, Ellipse, Group
    }
    public class DemoSceneController : SceneControllerViewModel
    {
        public DemoSceneController(SceneViewModel scene) : base(scene)
        {
            UserInteractionController.OnKeyDown += UserInteractionControllerOnOnKeyDown;
            UserInteractionController.OnKeyUp += UserInteractionControllerOnOnKeyUp;
            UserInteractionController.OnMouseDown += UserInteractionControllerOnOnMouseDown;
            UserInteractionController.OnMouseUp += UserInteractionControllerOnOnMouseUp;
            UserInteractionController.OnMouseMoved += UserInteractionControllerOnOnMouseMoved;
            ComponentCreationMode = ComponentType.None;
        }

        public ComponentViewModel SelectedComponent { get; set; }

        public ComponentType ComponentCreationMode { get; set; }

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

        #region Component Creation

        private readonly List<Vector2> _vertices = new List<Vector2>();

        public void AddVertex(Vector2 vertex)
        {
            _vertices.Add(vertex);
        }

        public void EndComponent()
        {
            var componentBuilder = new ComponentBuilder();
            switch (ComponentCreationMode)
            {
                case ComponentType.Point:
                    componentBuilder
                        .SetBase(_vertices.Last().ToPoint().ToViewModel());
                    break;
                case ComponentType.Line:
                    componentBuilder
                        .SetBase(PolyLine.Create(_vertices.ToArray()).ToViewModel());
                    break;
                case ComponentType.Polygon:
                    componentBuilder
                        .SetBase(Polygon.Create(_vertices.ToArray()).ToViewModel());
                    break;
                case ComponentType.Ellipse:
                    componentBuilder
                        .SetBase(Ellipse.Create(_vertices.Last().X, _vertices.Last().Y, 20,20,0).ToViewModel());
                    break;
                case ComponentType.Group:
                default:
                    return;
            }

            var newComponent = componentBuilder
                .SetMargin(0.0f)
                .SetFillColor(Color.Orange)
                .SetBorderThickness(1).Build();

            Scene.Components.Add(newComponent);
            _vertices.Clear();
        }
        #endregion

        private void UserInteractionControllerOnOnMouseDown(object? sender, Vector2 position)
        {
            if (ComponentCreationMode != ComponentType.None)
            {
                switch (ComponentCreationMode)
                {
                    case ComponentType.Point:
                        AddVertex(position);
                        EndComponent();
                        break;
                    case ComponentType.Line:
                        AddVertex(position);
                        break;
                    case ComponentType.Polygon:
                        AddVertex(position);
                        break;
                    case ComponentType.Ellipse:
                        AddVertex(position);
                        EndComponent();
                        break;
                    case ComponentType.Group:

                        break;
                }
            }


            if (ComponentCreationMode == ComponentType.None)
            {
                SelectedComponent = null;

                foreach (var componentViewModel in Scene.Components.CollidingWith(position, CollisionDetector))
                {
                    SelectedComponent = componentViewModel;
                    componentViewModel.BorderColor = Color.Green;
                }

                Scene.BringToFront(SelectedComponent);
            }
        }

        private void UserInteractionControllerOnOnKeyUp(object? sender, string key)
        {
            switch (ComponentCreationMode)
            {
                case ComponentType.Line:
                    EndComponent();
                    break;
                case ComponentType.Polygon:
                    EndComponent();
                    break;
            }

            ComponentCreationMode = ComponentType.None;
        }

        private void UserInteractionControllerOnOnKeyDown(object? sender, string e)
        {
            ComponentCreationMode = KeyPressedToComponentType(e);
        }


        private ComponentType KeyPressedToComponentType(string key)
        {
            switch (key?.ToLower())
            {
                case "a":
                    return ComponentType.Point;
                case "s":
                    return ComponentType.Line;
                case "d":
                    return ComponentType.Polygon;
                case "f":
                    return ComponentType.Ellipse;
                case "g":
                    return ComponentType.Group;
                default:
                    return ComponentType.None;
            }
        }
    }
}