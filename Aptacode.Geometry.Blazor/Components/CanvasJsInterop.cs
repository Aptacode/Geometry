using Microsoft.JSInterop;

namespace Aptacode.Geometry.Blazor.Components
{
    public static class CanvasJsInteropExtensions
    {
        public static void Fill(this IJSUnmarshalledRuntime js)
        {
            js.InvokeUnmarshalled<object>("fill");
        }

        public static void Stroke(this IJSUnmarshalledRuntime js)
        {
            js.InvokeUnmarshalled<object>("stroke");
        }

        public static void beginPath(this IJSUnmarshalledRuntime js)
        {
            js.InvokeUnmarshalled<object>("beginPath");
        }

        public static void ellipse(this IJSUnmarshalledRuntime js, float x, float y, float radiusX, float radiusY,
            float rotation, float startAngle, float endAngle)
        {
            js.InvokeUnmarshalled<float[], object>("ellipse",
                new[] {x, y, radiusX, radiusY, rotation, startAngle, endAngle});
        }

        public static void clearRect(this IJSUnmarshalledRuntime js, float x, float y, float width, float height)
        {
            js.InvokeUnmarshalled<float[], object>("clearRect", new[] {x, y, width, height});
        }

        public static void closePath(this IJSUnmarshalledRuntime js)
        {
            js.InvokeUnmarshalled<object>("closePath");
        }

        public static void moveTo(this IJSUnmarshalledRuntime js, float x, float y)
        {
            js.InvokeUnmarshalled<float[], object>("moveTo", new[] {x, y});
        }

        public static void lineTo(this IJSUnmarshalledRuntime js, float x, float y)
        {
            js.InvokeUnmarshalled<float[], object>("lineTo", new[] {x, y});
        }

        public static void fillStyle(this IJSUnmarshalledRuntime js, string defaultFillColor)
        {
            js.InvokeUnmarshalled<string, object>("fillStyle", defaultFillColor);
        }

        public static void strokeStyle(this IJSUnmarshalledRuntime js, string defaultBorderColor)
        {
            js.InvokeUnmarshalled<string, object>("strokeStyle", defaultBorderColor);
        }

        public static void lineWidth(this IJSUnmarshalledRuntime js, float defaultBorderThickness)
        {
            js.InvokeUnmarshalled<float[], object>("lineWidth", new[] {defaultBorderThickness});
        }

        public static void textAlign(this IJSUnmarshalledRuntime js, string textAlign)
        {
            js.InvokeUnmarshalled<string, object>("textAlign", textAlign);
        }

        public static void fillText(this IJSUnmarshalledRuntime js, string text, float x, float y)
        {
            js.InvokeUnmarshalled<string, float[], object>("fillText", text, new[] {x, y});
        }
    }
}