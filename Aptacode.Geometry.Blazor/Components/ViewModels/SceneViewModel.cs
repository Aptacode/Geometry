using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Utilities.Mvvm;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;
using Aptacode.Geometry.Collision.Rectangles;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class SceneViewModel : BindableBase, IAsyncDisposable
    {
        #region Ctor

        public SceneViewModel(Vector2 size)
        {
            Components = new List<ComponentViewModel>();
            Size = size;
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

        #region Redraw

        private DateTime _lastTick = DateTime.Now;

        public async Task RedrawAsync()
        {
            if (Ctx == null)
            {
                return;
            }

            var currentTime = DateTime.Now;
            var delta = currentTime - _lastTick;
            var frameRate = 1.0f / delta.TotalSeconds;
            _lastTick = currentTime;

            Console.WriteLine($"{frameRate}fps");
            await using var batch = await Ctx.CreateBatchAsync();
            await batch.FillStyleAsync(ComponentViewModel.DefaultFillColor);
            await batch.StrokeStyleAsync(ComponentViewModel.DefaultBorderColor);
            await batch.LineWidthAsync(ComponentViewModel.DefaultBorderThickness);
            await batch.ShadowBlurAsync(0.0f);

            await batch.SaveAsync();
            await batch.FillStyleAsync("White");
            await batch.StrokeStyleAsync("White");
            await batch.LineWidthAsync(0);

            var invalidatedItems
                = await InvalidateItems(batch);

            await batch.RestoreAsync();

            for (var i = 0; i < invalidatedItems.Count; i++)
            {
                var component = invalidatedItems[i];
                await batch.SaveAsync();

                await component.Draw(batch);

                await batch.RestoreAsync();
            }
        }

        public async Task<List<ComponentViewModel>> InvalidateItems(IContext2DWithoutGetters batch)
        {
            var validItems = new List<ComponentViewModel>();
            var invalidItems = new List<ComponentViewModel>();

            for (var i = 0; i < Components.Count; i++)
            {
                var component = Components[i];
                if (component.Invalidated)
                {
                    invalidItems.Add(component);
                }
                else
                {
                    validItems.Add(component);
                }
            }


            for (var invalidItemIndex = 0; invalidItemIndex < invalidItems.Count; invalidItemIndex++)
            {
                var invalidItem = invalidItems[invalidItemIndex];
                var thickness = invalidItem.BorderThickness;

                var invalidItemBorder = new Vector2(thickness);
                var oldBoundingRecWithBorder = BoundingRectangle.FromTwoPoints(
                    invalidItem.OldBoundingRectangle.TopLeft - 4 * invalidItemBorder,
                    invalidItem.OldBoundingRectangle.BottomRight + 8 * invalidItemBorder);
                var newBoundingRecWithBorder = BoundingRectangle.FromTwoPoints(
                    invalidItem.BoundingRectangle.TopLeft - 4 * invalidItemBorder,
                    invalidItem.BoundingRectangle.BottomRight + 8 * invalidItemBorder);


                for (var validItemIndex = 0; validItemIndex < validItems.Count;)
                {
                    var validComponent = validItems[validItemIndex];
                    var validThickness = validComponent.BorderThickness;

                    var validItemBorder = new Vector2(validThickness);

                    var newValidBoundingRect = BoundingRectangle.FromTwoPoints(
                        validComponent.BoundingRectangle.TopLeft - 4 * validItemBorder,
                        validComponent.BoundingRectangle.BottomRight + 8 * validItemBorder);

                    if (oldBoundingRecWithBorder.CollidesWith(newValidBoundingRect) ||
                        newBoundingRecWithBorder.CollidesWith(newValidBoundingRect)
                    )
                    {
                        validComponent.Invalidated = true;
                        validItems.RemoveAt(validItemIndex);
                        invalidItems.Add(validComponent);
                    }
                    else
                    {
                        validItemIndex++;
                    }
                }

                await Invalidate(batch, invalidItem.OldBoundingRectangle, thickness);
                await Invalidate(batch, invalidItem.BoundingRectangle, thickness);
            }

            return invalidItems;
        }

        public async Task Invalidate(IContext2DWithoutGetters batch, BoundingRectangle rectangle, float border)
        {
            await batch.ClearRectAsync((int) rectangle.TopLeft.X - 4 * border, (int) rectangle.TopLeft.Y - 4 * border,
                (int) rectangle.Size.X + 8 * border, (int) rectangle.Size.Y + 8 * border);
        }

        #endregion

        #region Properties

        public List<ComponentViewModel> Components { get; set; }

        public Vector2 Size { get; set; }
        public Context2D Ctx { get; set; }

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
    }
}