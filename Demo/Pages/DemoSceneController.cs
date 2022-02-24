using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Aptacode.AppFramework.Components;
using Aptacode.AppFramework.Components.Controls;
using Aptacode.AppFramework.Components.Primitives;
using Aptacode.AppFramework.Extensions;
using Aptacode.AppFramework.Scene;
using Aptacode.AppFramework.Scene.Events;
using Aptacode.AppFramework.Utilities;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;

namespace Aptacode.Geometry.Demo.Pages;

public class DemoSceneController : SceneController
{
    private readonly ComponentBuilder _componentBuilder = new();

    public readonly List<ComponentViewModel> SelectedComponents = new();

    public DemoSceneController(Vector2 size) : base(size)
    {
        ShowGrid = true;
        UserInteractionController.OnMouseEvent += UserInteractionControllerOnOnMouseEvent;
        UserInteractionController.OnKeyboardEvent += UserInteractionControllerOnOnKeyboardEvent;
        ComponentCreationMode = ComponentType.None;

        GeometryScene = new Scene(size);
        DragBox = new DragBox(Polygon.Rectangle.FromTwoPoints(Vector2.Zero, size));
        DragBox.FillColor = Color.Transparent;

        GeometryScene.Add(DragBox);

        AreaSelection = new SelectionComponent();
        GeometryScene.Add(AreaSelection);
        Scenes.Add(GeometryScene);
    }

    public Scene GeometryScene { get; set; }
    public DragBox DragBox { get; set; }
    public SelectionComponent AreaSelection { get; set; }
    public ComponentType ComponentCreationMode { get; set; }

    private void UserInteractionControllerOnOnKeyboardEvent(object? sender, KeyboardEvent e)
    {
        switch (e)
        {
            case KeyDownEvent mouseMove:
                UserInteractionControllerOnOnKeyDown(this, mouseMove.Key);
                break;
            case KeyUpEvent mouseUp:
                UserInteractionControllerOnOnKeyUp(this, mouseUp.Key);
                break;
        }
    }

    private void UserInteractionControllerOnOnMouseEvent(object? sender, MouseEvent e)
    {
        switch (e)
        {
            case MouseDownEvent mouseDown:
                UserInteractionControllerOnOnMouseDown(this, mouseDown.Position);
                break;
            case MouseClickEvent mouseDown:
                UserInteractionControllerOnMouseClicked(this, mouseDown.Position);
                break;
        }
    }

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
            var collidingComponents = GeometryScene.Components.CollidingWith(position).ToList();
            if (collidingComponents.Any())
            {
                if (!collidingComponents.Any(c => SelectedComponents.Contains(c)))
                {
                    SelectedComponents.Clear();
                    foreach (var componentViewModel in collidingComponents.ToArray())
                    {
                        componentViewModel.BorderColor = Color.Green;
                        GeometryScene.BringToFront(componentViewModel);
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

    private void UserInteractionControllerOnMouseClicked(object? sender, Vector2 position)
    {
        var mouseClickEvent = new MouseClickEvent(position);
        foreach (var componentViewModel in GeometryScene.Components) componentViewModel.Handle(mouseClickEvent);
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
                    .SetBase(Ellipse.Create(_vertices.Last().X, _vertices.Last().Y, 2, 2, 0).ToViewModel())
                    .SetMargin(0.0f);

                break;
            case ComponentType.Group:
            default:
                return;
        }

        var newComponent = _componentBuilder
            .SetFillColor(Color.Orange)
            .Build();

        newComponent.OnMouseClick += (sender, mouseClickEvent) =>
        {
            Console.WriteLine("Clicked: " + mouseClickEvent.Position);
        };

        DragBox.Add(newComponent);
        _vertices.Clear();
    }

    #endregion
}