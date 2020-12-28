using System;
using System.Drawing;
using System.Numerics;
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

            sceneBuilder.SetWidth(1000).SetHeight(1000);

            //Polygon
            var polygon = componentBuilder
                .AddPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20)).SetFillColor(Color.Green)
                .SetBorderThickness(1).Build();

            polygon.Scale(new Vector2(4.0f));
            polygon.Rotate((float) Math.PI);
            polygon.Translate(new Vector2(200, 0));
            sceneBuilder.AddComponent(polygon);

            //Polyline
            var polyLine = componentBuilder.AddPrimitive(PolyLine.Create(80, 40, 70, 80, 60, 20))
                .SetFillColor(Color.Blue).SetBorderThickness(1).Build();

            polyLine.Scale(new Vector2(2.0f));
            polyLine.Rotate(0.5f);
            sceneBuilder.AddComponent(polyLine);

            //Rectangles
            sceneBuilder.AddComponent(
                componentBuilder.AddPrimitive(Rectangle.Create(400, 100, 75, 75))
                    .SetText("Blue Rectangle")
                    .SetFillColor(Color.Blue).SetBorderThickness(1).Build());

            sceneBuilder.AddComponent(componentBuilder.AddPrimitive(Rectangle.Create(100, 250, 100, 100))
                .SetFillColor(Color.Red).SetBorderThickness(1).Build());

            //Ellipse


            var child1 = componentBuilder.AddPrimitive(Ellipse.Create(80, 80, 15, 15, 0.0f))
                .SetFillColor(Color.Orange).SetBorderThickness(1).Build();

            var child2 = componentBuilder.AddPrimitive(Ellipse.Create(300, 180, 15, 10, (float) Math.PI))
                .SetFillColor(Color.Green).SetBorderThickness(1).Build();

            var group = componentBuilder.AddChild(child1).AddChild(child2).Build();

            sceneBuilder.AddComponent(group);

            //PrimitiveGroup
            sceneBuilder.AddComponent(componentBuilder
                .AddPrimitive(Ellipse.Create(400, 200, 15, 15, 0.0f))
                .AddPrimitive(Rectangle.Create(400, 200, 30, 10))
                .SetFillColor(Color.Green)
                .SetText("Group")
                .SetBorderThickness(1).Build());

            var scene = sceneBuilder.Build();

            SceneController = new DemoSceneController(scene);

            await base.OnInitializedAsync();
        }
    }
}