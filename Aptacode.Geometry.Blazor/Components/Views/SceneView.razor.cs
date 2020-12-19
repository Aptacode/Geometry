using System;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Blazor.Components.Views
{
    public class SceneViewBase : ComponentBase
    {
        #region Properties

        [Parameter] public SceneViewModel ViewModel { get; set; }

        #endregion

        protected override async Task OnInitializedAsync()
        {
            ViewModel.OnRedraw += SceneOnOnRedraw;
            await base.OnInitializedAsync();
        }

        private void SceneOnOnRedraw(object? sender, EventArgs e)
        {
            StateHasChanged();
        }
    }
}