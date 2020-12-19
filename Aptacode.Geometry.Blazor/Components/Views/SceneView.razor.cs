using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Components.ViewModels.Primitives;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Blazor.Components.Views
{
    public class SceneViewBase : ComponentBase
    {
        public SceneViewModel Scene { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var sceneBuilder = new SceneBuilder();
            sceneBuilder.SetWidth(100).SetHeight(100);
            sceneBuilder.AddPrimitive(Rectangle.Create(10, 10, 10, 10));
            sceneBuilder.AddPrimitive(Polygon.Create(10, 10, 20, 10, 40, 40, 30, 80, 10, 20));
            sceneBuilder.AddPrimitive(PolyLine.Create(80, 40, 70, 80, 60, 20));
            sceneBuilder.AddPrimitive(Ellipse.Create(80, 40, 20.0f));
            Scene = sceneBuilder.Build();
            
            await base.OnInitializedAsync();
        }
    }
}