using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Components.ViewModels.Primitives;
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
            Scene.Components.Add(new RectangleViewModel(Rectangle.Create(new Vector2(0,0), new Vector2(10,10))));
            
            await base.OnInitializedAsync();
        }
    }
}
