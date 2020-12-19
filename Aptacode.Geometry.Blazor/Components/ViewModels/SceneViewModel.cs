using System;
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

        #region Events

        public event EventHandler OnRedraw;

        protected virtual void Redraw()
        {
            OnRedraw?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Layering

        public void BringToFront(ComponentViewModel componentViewModel)
        {
            if (!Components.Remove(componentViewModel))
            {
                return;
            }

            Components.Add(componentViewModel);
            Redraw();
        }

        public void SendToBack(ComponentViewModel componentViewModel)
        {
            if (!Components.Remove(componentViewModel))
            {
                return;
            }

            Components.Insert(0, componentViewModel);
            Redraw();
        }

        public void BringForward(ComponentViewModel componentViewModel)
        {
            var index = Components.IndexOf(componentViewModel);
            if (index == Components.Count - 1)
            {
                return;
            }

            Components.RemoveAt(index);
            Components.Insert(index + 1, componentViewModel);
            Redraw();
        }

        public void SendBackward(ComponentViewModel componentViewModel)
        {
            var index = Components.IndexOf(componentViewModel);
            if (index == 0)
            {
                return;
            }

            Components.RemoveAt(index);
            Components.Insert(index - 1, componentViewModel);
            Redraw();
        }

        #endregion
    }
}