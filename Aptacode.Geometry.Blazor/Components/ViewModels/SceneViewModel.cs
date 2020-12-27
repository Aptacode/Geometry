using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Utilities.Mvvm;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class SceneViewModel : BindableBase, IAsyncDisposable
    {
        private readonly CollisionDetector _collisionDetector = new BoundingRectangleCollisionDetector();

        public SceneViewModel(Vector2 size, IEnumerable<ComponentViewModel> components)
        {
            Components = components.ToList();
            Size = size;
        }

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

        private DateTime lastTick = DateTime.Now;

        public async Task RedrawAsync()
        {
            if (Ctx == null)
            {
                return;
            }

            var currentTime = DateTime.Now;
            var delta = currentTime - lastTick;
            var frameRate = 1.0f / delta.TotalSeconds;
            lastTick = currentTime;

            Console.WriteLine($"{frameRate}fps");
            await using var batch = await Ctx.CreateBatchAsync();
            await batch.FillStyleAsync(ComponentViewModel.DefaultFillColor);
            await batch.StrokeStyleAsync(ComponentViewModel.DefaultBorderColor);
            await batch.LineWidthAsync(ComponentViewModel.DefaultBorderThickness);

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
            const int margin = 4;

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
                var invalidItemBorder = new Vector2(invalidItem.BorderThickness);
                var oldBoundingRecWithBorder = BoundingRectangle.FromTwoPoints(
                    invalidItem._oldBoundingRectangle.TopLeft - (4 * invalidItemBorder),
                    invalidItem._oldBoundingRectangle.BottomRight + (8 * invalidItemBorder));
                var newBoundingRecWithBorder = BoundingRectangle.FromTwoPoints(
                    invalidItem.Primitive.BoundingRectangle.TopLeft - (4 * invalidItemBorder),
                    invalidItem.Primitive.BoundingRectangle.BottomRight + (8 * invalidItemBorder));


                for (var validItemIndex = 0; validItemIndex < validItems.Count;)
                {
                    var validComponent = validItems[validItemIndex];
                    var validItemBorder = new Vector2(validComponent.BorderThickness);
                    
                    var newValidBoundingRect = BoundingRectangle.FromTwoPoints(
                        validComponent.Primitive.BoundingRectangle.TopLeft - (4 * validItemBorder),
                        validComponent.Primitive.BoundingRectangle.BottomRight + (8 * validItemBorder));
                    
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

                await Invalidate(batch, invalidItem._oldBoundingRectangle, invalidItem.BorderThickness);
                await Invalidate(batch, invalidItem.Primitive.BoundingRectangle, invalidItem.BorderThickness);
            }

            return invalidItems;
        }

        public async Task Invalidate(IContext2DWithoutGetters batch, BoundingRectangle rectangle, int border)
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