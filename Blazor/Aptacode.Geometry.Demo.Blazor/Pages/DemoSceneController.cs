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
using Aptacode.Geometry.Primitives.Polygons;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public class DemoSceneController : SceneControllerViewModel
    {
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

        public readonly List<ComponentViewModel> SelectedComponents = new List<ComponentViewModel>();
        public SelectionComponent AreaSelection { get; set; }
        
        private readonly ComponentBuilder componentBuilder = new();
        public ComponentType ComponentCreationMode { get; set; }
        private void UserInteractionControllerOnOnMouseDown(object? sender, Vector2 position)
        {
            Console.WriteLine("Mouse down");

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
                    Console.WriteLine(SelectedComponents.Count() + "Collide");
                    if (!collidingComponents.Any(c => SelectedComponents.Contains(c)))
                    {
                        Console.WriteLine(SelectedComponents.Count() + "new selection");
                        SelectedComponents.Clear();
                        foreach (var componentViewModel in collidingComponents.ToArray())
                        {
                            componentViewModel.BorderColor = Color.Green;
                            Scene.BringToFront(componentViewModel);
                            SelectedComponents.Add(componentViewModel);
                        }
                    }
                    else
                    {
                        Console.WriteLine(SelectedComponents.Count() + "selection");
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
            Console.WriteLine("Mouse up");
            if (AreaSelection.SelectionMade())
            {
                SelectedComponents.Clear();
                Console.WriteLine("Selection" + AreaSelection.MarginPrimitive.BoundingRectangle.TopLeft+ " " + AreaSelection.MarginPrimitive.BoundingRectangle.Size);


                var collidingComponents = Scene.Components.CollidingWith(AreaSelection, CollisionDetector);
                Console.WriteLine("Selection" + collidingComponents.Count());
                AreaSelection.Rectangle = Primitives.Polygons.Rectangle.Create(Vector2.Zero, Vector2.Zero);

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

        #region Component Creation

        private readonly List<Vector2> _vertices = new List<Vector2>();

        public void AddVertex(Vector2 vertex)
        {
            _vertices.Add(vertex);
        }

        public void EndComponent()
        {
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
    }
}