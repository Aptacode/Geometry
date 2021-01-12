using System.Numerics;

namespace Aptacode.Geometry.Blazor.Components.ViewModels.Components.Events
{
    public abstract class BaseUIEvent
    {
        
    }
    
    public abstract class BaseMouseEvent : BaseUIEvent
    {

    }
    
    public class MouseDownEvent : BaseMouseEvent
    {
        public Vector2 Position { get; set; }

        public MouseDownEvent(Vector2 position)
        {
            Position = position;    
        }
    }

    public class MouseUpEvent : BaseUIEvent
    {
        public Vector2 Position { get; set; }

        public MouseUpEvent(Vector2 position)
        {
            Position = position;
        }
    }

    public class MouseClickEvent : BaseUIEvent
    {
        public Vector2 Position { get; set; }

        public MouseClickEvent(Vector2 position)
        {
            Position = position;
        }
    }


}
