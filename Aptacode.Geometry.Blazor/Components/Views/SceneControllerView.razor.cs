using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Aptacode.Geometry.Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Aptacode.Geometry.Blazor.Components.Views
{
    public class SceneControllerViewBase : ComponentBase
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsRuntime.InvokeVoidAsync("SetFocusToElement", Container);
            }
        }

        public void MouseDown(MouseEventArgs e)
        {
            ViewModel.UserInteractionController.MouseDown(e.FromScale());
        }

        public void MouseUp(MouseEventArgs e)
        {
            ViewModel.UserInteractionController.MouseUp(e.FromScale());
        }

        public void MouseOut(MouseEventArgs e) { }

        public void MouseMove(MouseEventArgs e)
        {
            ViewModel.UserInteractionController.MouseMove(e.FromScale());
        }

        public void KeyDown(KeyboardEventArgs e)
        {
            ViewModel.UserInteractionController.KeyDown(e.Key);
        }

        public void KeyUp(KeyboardEventArgs e)
        {
            ViewModel.UserInteractionController.KeyUp(e.Key);
        }

        #region Properties

        [Parameter] public SceneControllerViewModel ViewModel { get; set; }

        [Inject] private IJSRuntime JsRuntime { get; set; }

        protected ElementReference Container;

        #endregion
    }
}