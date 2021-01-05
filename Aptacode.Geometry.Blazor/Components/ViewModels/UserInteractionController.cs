using System;
using System.Numerics;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public class UserInteractionController
    {
        #region State

        public bool ConnectionPointSelected { get; set; }
        public Vector2 LastMousePosition { get; set; }
        public Vector2 MouseDownPosition { get; set; }
        public DateTime FirstMouseDownTime { get; set; }
        public DateTime SecondMouseDownTime { get; set; }

        #endregion

        #region Interaction

        #region Mouse

        public void MouseClickDown()
        {
            if (DateTime.Now - FirstMouseDownTime > TimeSpan.FromMilliseconds(300))
            {
                FirstMouseDownTime = DateTime.Now;
            }
            else
            {
                SecondMouseDownTime = DateTime.Now;
            }
        }

        public void MouseClickRelease(Vector2 position)
        {
            if (DateTime.Now - SecondMouseDownTime < TimeSpan.FromMilliseconds(150))
            {
                OnMouseDoubleClicked?.Invoke(this, position);
            }
            else if (DateTime.Now - FirstMouseDownTime < TimeSpan.FromMilliseconds(150))
            {
                OnMouseClicked?.Invoke(this, position);
            }
        }

        public void MouseDown(Vector2 position)
        {
            MouseDownPosition = position;
            MouseClickDown();
            OnMouseDown?.Invoke(this, position);
            LastMousePosition = position;
        }

        public void MouseUp(Vector2 position)
        {
            OnMouseUp?.Invoke(this, position);
            LastMousePosition = position;

            MouseClickRelease(position);
        }

        public void MouseMove(Vector2 position)
        {
            if (Math.Abs(LastMousePosition.X - position.X) <= Constants.Tolerance &&
                Math.Abs(LastMousePosition.Y - position.Y) <= Constants.Tolerance)
            {
                return;
            }

            OnMouseMoved?.Invoke(this, position);
            LastMousePosition = position;
        }

        #endregion

        #region Keyboard

        public string? CurrentKey;
        public bool ControlPressed => CurrentKey == "Control";

        public bool IsPressed(string key)
        {
            return string.Equals(CurrentKey, key, StringComparison.OrdinalIgnoreCase);
        }

        public bool NothingPressed => string.IsNullOrEmpty(CurrentKey);

        public void KeyDown(string key)
        {
            CurrentKey = key;
            OnKeyDown?.Invoke(this, CurrentKey);
        }

        public void KeyUp(string key)
        {
            if (ControlPressed)
            {
                CurrentKey = null;
            }

            CurrentKey = null;
            OnKeyUp?.Invoke(this, CurrentKey);
        }

        #endregion

        #endregion

        #region Events

        public event EventHandler<Vector2> OnMouseDown;
        public event EventHandler<Vector2> OnMouseMoved;
        public event EventHandler<Vector2> OnMouseUp;
        public event EventHandler<Vector2> OnMouseClicked;
        public event EventHandler<Vector2> OnMouseDoubleClicked;
        public event EventHandler<string> OnKeyDown;
        public event EventHandler<string> OnKeyUp;

        #endregion
    }
}