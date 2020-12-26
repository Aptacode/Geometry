using System.Drawing;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;
using Microsoft.AspNetCore.Components;
using Rectangle = Aptacode.Geometry.Primitives.Polygons.Rectangle;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public class IndexBase : ComponentBase
    {
        public DemoSceneController SceneController { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var sceneBuilder = new SceneBuilder();
            var componentBuilder = new ComponentBuilder();

            sceneBuilder.SetWidth(200).SetHeight(100);
            sceneBuilder.AddComponent(componentBuilder.SetPrimitive(Rectangle.Create(100, 50, 40, 40))
                .SetFillColor(Color.Red).SetBorderThickness(2).Build());
            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());            sceneBuilder.AddComponent(componentBuilder
                .SetPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(2).Build());
            sceneBuilder.AddComponent(componentBuilder.SetPrimitive(PolyLine.Create(80, 40, 70, 80, 60, 20))
                .SetFillColor(Color.Blue).SetBorderThickness(2).Build());
            sceneBuilder.AddComponent(componentBuilder.SetPrimitive(Ellipse.Create(80, 80, 10.0f))
                .SetFillColor(Color.Orange).SetBorderThickness(2).Build());
            var scene = sceneBuilder.Build();

            SceneController = new DemoSceneController(scene);

            await base.OnInitializedAsync();
        }
    }
}