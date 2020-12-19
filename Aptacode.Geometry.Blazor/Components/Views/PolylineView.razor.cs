using System;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels.Primitives;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Blazor.Components.Views
{
    public class PolylineViewBase : ComponentBase
    {
        #region Properties

        [Parameter] public PolylineViewModel ViewModel { get; set; }

        #endregion

        protected override async Task OnInitializedAsync()
        {
            ViewModel.OnRedraw += ViewModelOnOnRedraw;
            await base.OnInitializedAsync();
        }

        private void ViewModelOnOnRedraw(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }
    }
}