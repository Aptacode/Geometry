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
using Rectangle = Aptacode.Geometry.Primitives.Polygons.Rectangle;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public class DemoSceneController : SceneControllerViewModel
    {
        private readonly ComponentBuilder _componentBuilder = new();

        public readonly List<ComponentViewModel> SelectedComponents = new();

        public DemoSceneController(SceneViewModel scene) : base(scene)
        {
            UserInteractionController.OnKeyDown += UserInteractionControllerOnOnKeyDown;
            UserInteractionController.OnKeyUp += UserInteractionControllerOnOnKeyUp;
            UserInteractionController.OnMouseDown += UserInteractionControllerOnOnMouseDown;
            UserInteractionController.OnMouseUp += UserInteractionControllerOnOnMouseUp;
            UserInteractionController.OnMouseMoved += UserInteractionControllerOnOnMouseMoved;
            UserInteractionController.OnMouseClicked += UserInteractionControllerOnMouseClicked;

            ComponentCreationMode = ComponentType.None;

            AreaSelection = new SelectionComponent();

            Scene.Components.Add(AreaSelection);
        }

        public SelectionComponent AreaSelection { get; set; }
        public ComponentType ComponentCreationMode { get; set; }

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
            else
            {
                var collidingComponents = Scene.Components.CollidingWith(position, CollisionDetector).ToList();
                if (collidingComponents.Any())
                {
                    if (!collidingComponents.Any(c => SelectedComponents.Contains(c)))
                    {
                        SelectedComponents.Clear();
                        foreach (var componentViewModel in collidingComponents.ToArray())
                        {
                            componentViewModel.BorderColor = Color.Green;
                            Scene.BringToFront(componentViewModel);
                            SelectedComponents.Add(componentViewModel);
                        }
                    }
                }
                else
                {
                    SelectedComponents.Clear();
                    AreaSelection.MouseDown(position);
                }
            }
        }

        private void UserInteractionControllerOnOnMouseMoved(object? sender, Vector2 position)
        {
            AreaSelection.MouseMove(position);

            if (SelectedComponents.Any() && UserInteractionController.IsMouseDown)
            {
                var delta = position - UserInteractionController.LastMousePosition;
                foreach (var componentViewModel in SelectedComponents.ToArray())
                {
                    Translate(componentViewModel, delta, SelectedComponents, new CancellationTokenSource());
                }
            }
        }

        private void UserInteractionControllerOnOnMouseUp(object? sender, Vector2 position)
        {
            if (AreaSelection.SelectionMade())
            {
                SelectedComponents.Clear();
                var collidingComponents = Scene.Components.CollidingWith(AreaSelection, CollisionDetector);
                AreaSelection.Rectangle = Rectangle.Create(Vector2.Zero, Vector2.Zero);

                if (collidingComponents.Any())
                {
                    foreach (var componentViewModel in collidingComponents.ToArray())
                    {
                        if (componentViewModel == AreaSelection)
                        {
                            continue;
                        }

                        componentViewModel.BorderColor = Color.Green;
                        Scene.BringToFront(componentViewModel);
                        SelectedComponents.Add(componentViewModel);
                    }
                }
            }
            else
            {
                foreach (var componentViewModel in SelectedComponents)
                {
                    componentViewModel.BorderColor = Color.Black;
                }

                SelectedComponents.Clear();
            }
        }

        private void UserInteractionControllerOnMouseClicked(object? sender, Vector2 position)
        {
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

        #region Component Creation

        private readonly List<Vector2> _vertices = new();

        public void AddVertex(Vector2 vertex)
        {
            _vertices.Add(vertex);
        }

        public void EndComponent()
        {
            switch (ComponentCreationMode)
            {
                case ComponentType.Point:
                    _componentBuilder
                        .SetBase(_vertices.Last().ToPoint().ToViewModel())
                        .SetMargin(1.0f);
                    break;
                case ComponentType.Line:
                    _componentBuilder
                        .SetBase(PolyLine.Create(_vertices.ToArray()).ToViewModel())
                        .SetMargin(1.0f);
                    break;
                case ComponentType.Polygon:
                    _componentBuilder
                        .SetBase(Polygon.Create(_vertices.ToArray()).ToViewModel())
                        .SetMargin(0.0f);
                    break;
                case ComponentType.Ellipse:
                    _componentBuilder
                        .SetBase(Ellipse.Create(_vertices.Last().X, _vertices.Last().Y, 2,2, 0).ToViewModel())
                        .SetMargin(0.0f);

                    break;
                case ComponentType.Group:
                default:
                    return;
            }

            var newComponent = _componentBuilder
                .SetFillColor(Color.Orange)
                .SetBorderThickness(1).Build();

            Scene.Components.Add(newComponent);
            _vertices.Clear();
        }

        #endregion
    }
}