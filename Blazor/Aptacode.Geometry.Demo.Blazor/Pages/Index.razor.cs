using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public class IndexBase : ComponentBase
    {
        public DemoSceneController SceneController { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var sceneBuilder = new SceneBuilder();

            //Scene
            sceneBuilder.SetWidth(1000).SetHeight(1000);
            //var componentBuilder = new ComponentBuilder();

            ////Polygon
            //var polygon = componentBuilder
            //    .SetBase(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20).ToViewModel()).SetFillColor(Color.Orange)
            //    .SetBorderThickness(1).Build();

            //polygon.Scale(new Vector2(4.0f));
            //polygon.Rotate((float)Math.PI);
            //polygon.Translate(new Vector2(200, 0));
            //sceneBuilder.AddComponent(polygon);

            ////Polyline
            //var polyLine = componentBuilder.SetBase(PolyLine.Create(80, 40, 70, 80, 60, 20).ToViewModel())
            //    .SetFillColor(Color.Blue).SetBorderThickness(1).Build();

            //polyLine.Scale(new Vector2(2.0f));
            //polyLine.Rotate(0.5f);
            //sceneBuilder.AddComponent(polyLine);

            ////Rectangles
            //sceneBuilder.AddComponent(
            //    componentBuilder.SetBase(Rectangle.Create(400, 100, 75, 75).ToViewModel())
            //        .SetText("Blue Rectangle")
            //        .SetFillColor(Color.Blue).SetBorderThickness(1).Build());

            //sceneBuilder.AddComponent(componentBuilder.SetBase(Rectangle.Create(100, 250, 100, 100).ToViewModel())
            //    .SetFillColor(Color.Red).SetBorderThickness(1).Build());

            ////Ellipse Group
            //var child1 = componentBuilder.SetBase(Ellipse.Create(300, 40, 15, 15, 0.0f).ToViewModel())
            //    .SetFillColor(Color.Green).SetBorderThickness(1).Build();

            //var child2 = componentBuilder.SetBase(Ellipse.Create(300, 180, 15, 10, (float)Math.PI).ToViewModel())
            //    .SetFillColor(Color.Green).SetBorderThickness(1).Build();

            //var group = componentBuilder.AddChild(child1).AddChild(child2).SetText("Group").Build();

            //sceneBuilder.AddComponent(group);

            ////Component Group
            //sceneBuilder.AddComponent(componentBuilder
            //        .AddChild(componentBuilder.SetBase(Ellipse.Create(400, 200, 15, 15, 0.0f).ToViewModel())
            //            .SetFillColor(Color.White).SetBorderThickness(1).Build())
            //        .AddChild(componentBuilder.SetBase(Rectangle.Create(400, 200, 30, 10).ToViewModel())
            //            .SetFillColor(Color.White).SetBorderThickness(1).Build())
            //        .SetFillColor(Color.Green)
            //    .SetText("Group")
            //    .SetBorderThickness(1).Build());

            //Scene
            var scene = sceneBuilder.Build();
            SceneController = new DemoSceneController(scene);

            await base.OnInitializedAsync();
        }
    }
}