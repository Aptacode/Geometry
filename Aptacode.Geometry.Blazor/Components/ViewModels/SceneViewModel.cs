using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.BlazorCanvas;
using Aptacode.CSharp.Common.Utilities.Mvvm;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;
using Aptacode.Geometry.Collision.Rectangles;
using Microsoft.JSInterop;

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

        public BlazorCanvasInterop JSUnmarshalledRuntime { get; set; }

        #region Disposable

        public async ValueTask DisposeAsync()
        {
        }

        #endregion

        #region Redraw

        private DateTime _lastTick = DateTime.Now;

        public async Task RedrawAsync()
        {
            if (JSUnmarshalledRuntime == null)
            {
                return;
            }

            var currentTime = DateTime.Now;
            var delta = currentTime - _lastTick;
            var frameRate = 1.0f / delta.TotalSeconds;
            _lastTick = currentTime;

            //Console.WriteLine($"{frameRate}fps");
            JSUnmarshalledRuntime.FillStyle(ComponentViewModel.DefaultFillColor);
            JSUnmarshalledRuntime.StrokeStyle(ComponentViewModel.DefaultBorderColor);
            JSUnmarshalledRuntime.LineWidth(ComponentViewModel.DefaultBorderThickness);

            var invalidatedItems
                = await InvalidateItems();

            for (var i = 0; i < invalidatedItems.Count; i++)
            {
                var component = invalidatedItems[i];
                await component.Draw(JSUnmarshalledRuntime);
            }
        }

        public async Task<List<ComponentViewModel>> InvalidateItems()
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

                await Invalidate(invalidItem.OldBoundingRectangle, thickness);
                await Invalidate(invalidItem.BoundingRectangle, thickness);
            }

            return invalidItems;
        }

        public async Task Invalidate(BoundingRectangle rectangle, float border)
        {
            JSUnmarshalledRuntime.ClearRect(rectangle.TopLeft.X - 4 * border, rectangle.TopLeft.Y - 4 * border,
                rectangle.Size.X + 8 * border, rectangle.Size.Y + 8 * border);
        }

        #endregion

        #region Properties

        public List<ComponentViewModel> Components { get; set; }

        public Vector2 Size { get; set; }

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