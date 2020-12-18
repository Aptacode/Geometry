using System;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels.Primitives;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Blazor.Components.Views
{
    public class PointViewBase : ComponentBase
    {
        #region Properties

        [Parameter] public PointViewModel ViewModel { get; set; }

        #endregion

        protected override async Task OnInitializedAsync()
        {
            ViewModel.Redraw += ViewModelOnRedraw;
            await base.OnInitializedAsync();
        }

        private void ViewModelOnRedraw(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }
    }
}