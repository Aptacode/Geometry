using System.Numerics;
using Aptacode.BlazorCanvas;
using Aptacode.Geometry.Primitives;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Demo.Pages;

public class CanvasBase : ComponentBase
{
    #region Dependencies
    [Inject] public BlazorCanvasInterop BlazorCanvas { get; set; }

    #endregion

    #region Canvas
    public ElementReference Canvas { get; set; }
    private readonly Guid _canvasId = Guid.NewGuid();
    public int OutputSize { get; set; } = 1000;
    public string Value { get; set; } = "ellipse 500,500,100,200,2";

    #endregion

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Console.WriteLine($"Register canvas for scene: {_canvasId}");
            await BlazorCanvas.Register(_canvasId.ToString(), Canvas);
            Update();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected void OnInputChanged(ChangeEventArgs eventArgs)
    {
        Value = eventArgs.Value?.ToString() ?? string.Empty;
        Update();
    }

    private void Update()
    {
        BlazorCanvas.SelectCanvas(_canvasId.ToString());
        BlazorCanvas.FillStyle("gray");
        BlazorCanvas.StrokeStyle("black");
        BlazorCanvas.LineWidth(1);
        BlazorCanvas.ClearRect(0, 0, OutputSize, OutputSize);
        BlazorCanvas.Transform(1, 0, 0, -1, 0, OutputSize);

        BlazorCanvas.BeginPath();

        var input = Value.ToLower();
        if (input.Contains("ellipse"))
        {
            input = input.Replace("ellipse", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            var ellipse = Ellipse.Create(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);

            BlazorCanvas.Ellipse(ellipse.Position.X, (int)ellipse.Position.Y,
                ellipse.Radii.X,
                ellipse.Radii.Y, ellipse.Rotation, 0, 2.0f * (float)Math.PI);
        }
        else if (input.Contains("polygon"))
        {
            input = input.Replace("polygon", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            var polygon = Polygon.Create(parameters.ToArray());
            var vertices = new Vector2[polygon.Vertices.Length];
            for (var i = 0; i < polygon.Vertices.Length; i++) vertices[i] = polygon.Vertices[i];

            BlazorCanvas.Polygon(vertices);
        }
        else if (input.Contains("polyline"))
        {
            input = input.Replace("polyline", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            var polyline = PolyLine.Create(parameters.ToArray());

            var vertices = new Vector2[polyline.Vertices.Length];
            for (var i = 0; i < polyline.Vertices.Length; i++) vertices[i] = polyline.Vertices[i];

            BlazorCanvas.PolyLine(vertices);
        }
        else if (input.Contains("point"))
        {
            input = input.Replace("point", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            var point = Point.Create(parameters[0], parameters[1]);

            BlazorCanvas.Ellipse(point.Position.X, point.Position.Y,
                1, 1 , 0, 0, 2 * (float)Math.PI);
        }

        BlazorCanvas.Fill();
        BlazorCanvas.Stroke();
        BlazorCanvas.Transform(1, 0, 0, -1, 0, OutputSize);
    }
}