using System.Collections.Generic;
using System.Numerics;
using Aptacode.CSharp.Common.Utilities.Mvvm;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class SceneViewModel : BindableBase
    {
        public SceneViewModel()
        {
            Components = new List<ComponentViewModel>();
        }

        #region Properties

        public List<ComponentViewModel> Components { get; set; }

        public Vector2 Size { get; set; }

        #endregion
    }
}