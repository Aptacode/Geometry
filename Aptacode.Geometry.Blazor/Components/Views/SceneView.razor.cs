using System;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Utilities;
using Excubo.Blazor.Canvas;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Blazor.Components.Views
{
    public class SceneViewBase : ComponentBase, IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            await ViewModel.DisposeAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ViewModel.Ctx = await SceneCanvas.GetContext2DAsync();
                await ViewModel.RedrawAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        #region Properties

        [Parameter] public SceneViewModel ViewModel { get; set; }
        public Canvas SceneCanvas { get; set; }

        public string Style { get; set; } =
            $"-moz-transform: scale({Scale.Value}); -moz-transform-origin: 0 0; zoom: {Scale.Value};";

        #endregion
    }
}