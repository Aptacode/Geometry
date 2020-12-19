using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public class IndexBase : ComponentBase
    {
        public DemoSceneController SceneController { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var sceneBuilder = new SceneBuilder();

            sceneBuilder.SetWidth(100).SetHeight(100);
            sceneBuilder.AddPrimitive(Rectangle.Create(10, 10, 10, 10));
            sceneBuilder.AddPrimitive(Polygon.Create(20, 20, 20, 25, 25, 25, 30, 35, 25, 20));
            sceneBuilder.AddPrimitive(PolyLine.Create(80, 40, 70, 80, 60, 20));
            sceneBuilder.AddPrimitive(Ellipse.Create(80, 80, 10.0f));
            var scene = sceneBuilder.Build();

            SceneController = new DemoSceneController(scene);

            await base.OnInitializedAsync();
        }
    }
}