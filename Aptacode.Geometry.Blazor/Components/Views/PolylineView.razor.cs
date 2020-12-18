using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Aptacode.Geometry.Blazor.Components.ViewModels.Primitives;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Blazor.Components.Views
{
    public class PolylineViewBase : ComponentBase
    {
        #region Properties

        [Parameter]
        public PolylineViewModel ViewModel { get; set; }

        #endregion


        protected override async Task OnInitializedAsync()
        {
            ViewModel.PropertyChanged += Item_PropertyChanged;
            Refresh();
            await base.OnInitializedAsync();
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Refresh();
            InvokeAsync(StateHasChanged);
        }

        public void Refresh()
        {
            
        }

    }
}
