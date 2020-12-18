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
    public class PointViewBase : ComponentBase
    {
        #region Properties

        [Parameter]
        public PointViewModel ViewModel { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

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
            Position = ViewModel.Primitive.Position * Constants.Scale;
        }

    }
}
