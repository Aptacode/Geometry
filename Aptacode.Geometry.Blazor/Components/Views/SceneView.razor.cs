using System.Numerics;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Components.ViewModels.Primitives;
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
            Scene = new SceneViewModel();
            Scene.Components.Add(new RectangleViewModel(Rectangle.Create(new Vector2(10, 10), new Vector2(10, 10))));
            Scene.Components.Add(new PolygonViewModel(Polygon.Create(new Vector2(10, 10), new Vector2(20, 10),
                new Vector2(40, 40), new Vector2(30, 80), new Vector2(10, 20))));
            Scene.Components.Add(new PolylineViewModel(PolyLine.Create(new Vector2(80, 40), new Vector2(70, 80),
                new Vector2(60, 20))));
            Scene.Components.Add(new EllipseViewModel(new Ellipse(new Vector2(80, 40), 20.0f)));

            await base.OnInitializedAsync();
        }
    }
}