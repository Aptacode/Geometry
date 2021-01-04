using System;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Aptacode.Geometry.Blazor.Components.Views
{
    public class SceneViewBase : ComponentBase, IAsyncDisposable
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }

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
                await JsRuntime.InvokeVoidAsync("registerCanvas", SceneCanvas);
                await ViewModel.RedrawAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        #region Properties

        [Parameter] public SceneViewModel ViewModel { get; set; }

        public ElementReference SceneCanvas { get; set; }

        public string Style { get; set; } =
            $"-moz-transform: scale({Scale.Value}); -moz-transform-origin: 0 0; zoom: {Scale.Value};";

        #endregion
    }
}