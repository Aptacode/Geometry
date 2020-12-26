using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Utilities.Mvvm;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Primitives;
using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class SceneViewModel : BindableBase, IAsyncDisposable
    {
        private readonly CollisionDetector _collisionDetector = new CoarseCollisionDetector();

        public SceneViewModel(Vector2 size, IEnumerable<ComponentViewModel> components)
        {
            Components = components.ToArray();
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

            foreach (var component in Components.Where(c => c.Invalidated))
            {
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

            for (var i = 0; i < Components.Length; i++)
            {
                var component = Components[i];
                if (component.Invalidated)
                {
                    invalidItems.Add(component);
                    if (component._oldPrimitive != component.Primitive)
                    {
                        //Invalidate the old primitive's bounding circle if it changed
                        await Invalidate(batch, component._oldPrimitive.BoundingCircle, component.BorderThickness,
                            margin);
                    }
                }
                else
                {
                    validItems.Add(component);
                }
            }


            for (var invalidItemIndex = 0; invalidItemIndex < invalidItems.Count; invalidItemIndex++)
            {
                var invalidItem = invalidItems[invalidItemIndex];
                var oldradiusplusbuffer = invalidItem._oldPrimitive.BoundingCircle.Radius +
                                          invalidItem.BorderThickness + margin;
                var newradiusplusbuffer =
                    invalidItem.Primitive.BoundingCircle.Radius + invalidItem.BorderThickness + margin;
                var oldBoundingCircle = new Ellipse(invalidItem._oldPrimitive.BoundingCircle.Center,
                    new Vector2(oldradiusplusbuffer, oldradiusplusbuffer), 0.0f);
                var newBoundingCircle = new Ellipse(invalidItem.Primitive.BoundingCircle.Center,
                    new Vector2(newradiusplusbuffer, newradiusplusbuffer), 0.0f);

                for (var validItemIndex = 0; validItemIndex < validItems.Count; validItemIndex++)
                {
                    var validComponent = validItems[validItemIndex];
                    var validradiusplusbuffer = validComponent.Primitive.BoundingCircle.Radius +
                                                validComponent.BorderThickness + margin;
                    var validComponentBoundingCircle = new Ellipse(validComponent.Primitive.BoundingCircle.Center,
                        new Vector2(validradiusplusbuffer, validradiusplusbuffer), 0.0f);

                    if (oldBoundingCircle.CollidesWith(validComponentBoundingCircle, _collisionDetector) ||
                        newBoundingCircle.CollidesWith(validComponentBoundingCircle, _collisionDetector))
                    {
                        validComponent.Invalidated = true;
                        validItems.RemoveAt(validItemIndex);
                        invalidItems.Add(validComponent);
                    }
                }

                await Invalidate(batch, invalidItem.Primitive.BoundingCircle, invalidItem.BorderThickness, margin);
            }

            return invalidItems;
        }

        public async Task Invalidate(IContext2DWithoutGetters batch, BoundingCircle circle, int border, int margin)
        {
            var totalRadius = (int) (circle.Radius + border + margin);
            await batch.BeginPathAsync();
            //await batch.ClearRectAsync((int)circle.Center.X - totalRadius,
            //    (int)circle.Center.Y - totalRadius, totalRadius * 2,
            //    totalRadius * 2);
            await batch.EllipseAsync((int) circle.Center.X, (int) circle.Center.Y, totalRadius, totalRadius, 0, 0, 360);
            await batch.FillAsync(FillRule.NonZero);
        }

        #endregion

        #region Properties

        public ComponentViewModel[] Components { get; set; }

        public Vector2 Size { get; set; }
        public Context2D Ctx { get; set; }

        #endregion

        #region Layering

        public void BringToFront(ComponentViewModel componentViewModel)
        {
            //if (!Components.Remove(componentViewModel))
            //{
            //    return;
            //}

            //Components.Add(componentViewModel);
        }

        public void SendToBack(ComponentViewModel componentViewModel)
        {
            //if (!Components.Remove(componentViewModel))
            //{
            //    return;
            //}

            //Components.Insert(0, componentViewModel);
        }

        public void BringForward(ComponentViewModel componentViewModel)
        {
            //var index = Components.IndexOf(componentViewModel);
            //if (index == Components.Count - 1)
            //{
            //    return;
            //}

            //Components.RemoveAt(index);
            //Components.Insert(index + 1, componentViewModel);
        }

        public void SendBackward(ComponentViewModel componentViewModel)
        {
            //var index = Components.IndexOf(componentViewModel);
            //if (index == 0)
            //{
            //    return;
            //}

            //Components.RemoveAt(index);
            //Components.Insert(index - 1, componentViewModel);
        }

        #endregion
    }
}