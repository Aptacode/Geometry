﻿using System;
using System.Drawing;
using System.Numerics;
using Aptacode.Geometry.Blazor.Components.ViewModels.Components.Primitives;
using Rectangle = Aptacode.Geometry.Primitives.Polygons.Rectangle;

namespace Aptacode.Geometry.Demo.Blazor.Pages
{
    public class SelectionComponent : RectangleViewModel
    {
        public SelectionComponent() : base(Rectangle.Create(Vector2.Zero, Vector2.Zero))
        {
            FillColor = Color.White;
            BorderColor = Color.Green;
            IsShown = false;
            CollisionDetectionEnabled = true;
        }

        #region Props

        private bool _isMoving = false;
        private Vector2 _mouseDownPosition = Vector2.Zero;

        #endregion

        #region Methods

        public void MouseDown(Vector2 position)
        {
            if (!_isMoving)
            {
                IsShown = true;
                _isMoving = true;
                _mouseDownPosition = position;
            }
        }

        public void MouseMove(Vector2 position)
        {
            if (!_isMoving)
            {
                return;
            }

            var delta = position - _mouseDownPosition;
            var x = position.X >= _mouseDownPosition.X ? _mouseDownPosition.X : position.X;
            var y = position.Y >= _mouseDownPosition.Y ? _mouseDownPosition.Y : position.Y;
            var width = Math.Abs(delta.X);
            var height = Math.Abs(delta.Y);
            Rectangle = Rectangle.Create(new Vector2(x, y), new Vector2(width, height));
        }

        public bool SelectionMade()
        {
            if (!_isMoving)
            {
                return false;
            }
            _isMoving = false;
            IsShown = false;
            Invalidated = true;
            return true;
        }

        #endregion

    }
}