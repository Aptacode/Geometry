using System;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
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

        #endregion
    }
}