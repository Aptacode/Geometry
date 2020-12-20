using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Utilities.Mvvm;
using Aptacode.Geometry.Blazor.Utilities;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class SceneViewModel : BindableBase, IAsyncDisposable
    {
        public SceneViewModel(Vector2 size, IEnumerable<ComponentViewModel> components)
        {
            Components = components.ToList();
            Size = size.ToScale();
        }

        #region Properties

        public List<ComponentViewModel> Components { get; set; }

        public Vector2 Size { get; set; }
        public Context2D Ctx { get; set; }

        #endregion

        #region Redraw

        public async Task RedrawAsync()
        {
            if (Ctx == null)
            {
                return;
            }

            await using var batch = await Ctx.CreateBatchAsync();
            await batch.ClearRectAsync(0, 0, Size.X, Size.Y);

            for(var i = 0; i < Components.Count; i++)
            {
                await Components[i].Draw(batch);
            }
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
        }

        public void SendToBack(ComponentViewModel componentViewModel)
        {
            if (!Components.Remove(componentViewModel))
            {
                return;
            }

            Components.Insert(0, componentViewModel);
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
        }

        #endregion

        #region Disposable

        public async ValueTask DisposeAsync()
        {
            if (Ctx != null)
            {
                await Ctx.DisposeAsync();
            }
        }

        #endregion
    }
}