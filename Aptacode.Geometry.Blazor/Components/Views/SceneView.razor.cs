using System;
using System.Threading.Tasks;
using Aptacode.BlazorCanvas;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Utilities;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Blazor.Components.Views
{
    public class SceneViewBase : ComponentBase, IAsyncDisposable
    {
        #region Lifecycle

        public async ValueTask DisposeAsync()
        {
            await ViewModel.DisposeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await BlazorCanvasInterop.Register(Canvas);
                await ViewModel.RedrawAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        #endregion

        #region Properties

        [Inject] public BlazorCanvasInterop BlazorCanvasInterop { get; set; }

        [Parameter] public SceneViewModel ViewModel { get; set; }

        public ElementReference Canvas { get; set; }

        public string Style { get; set; } =
            $"position: absolute; "; //-moz-transform: scale({SceneScale.Value}); -moz-transform-origin: 0 0; zoom: {SceneScale.Value};";

        #endregion
    }
}