using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Aptacode.BlazorCanvas;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components;
using Aptacode.Geometry.Blazor.Utilities;
using Aptacode.Geometry.Collision.Rectangles;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class SceneRenderer
    {
        #region Ctor

        public SceneRenderer(BlazorCanvasInterop canvas, Scene scene)
        {
            _canvas = canvas;
            _scene = scene;
        }

        #endregion
        
        #region Props

        private readonly BlazorCanvasInterop _canvas;
        private readonly Scene _scene;

        #endregion

        #region Redraw

        public async Task Redraw()
        {
            _canvas.FillStyle(ComponentViewModel.DefaultFillColor);
            _canvas.StrokeStyle(ComponentViewModel.DefaultBorderColor);
            _canvas.LineWidth(ComponentViewModel.DefaultBorderThickness);

            var invalidatedItems = await InvalidateItems();

            for (var i = 0; i < invalidatedItems.Count; i++)
            {
                var component = invalidatedItems[i];
                await component.Draw(_canvas);
            }
        }

        public async Task<List<ComponentViewModel>> InvalidateItems()
        {
            var validItems = new List<ComponentViewModel>();
            var invalidItems = new List<ComponentViewModel>();

            for (var i = 0; i < _scene.Components.Count(); i++)
            {
                var component = _scene.Components.ElementAt(i);
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
            _canvas.ClearRect((rectangle.TopLeft.X - 4 * border) * SceneScale.Value, (rectangle.TopLeft.Y - 4 * border) * SceneScale.Value,
                (rectangle.Size.X + 8 * border) * SceneScale.Value, (rectangle.Size.Y + 8 * border) * SceneScale.Value);
        }

        #endregion


    }
}
