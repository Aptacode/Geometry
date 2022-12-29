using System.Numerics;
using Aptacode.Geometry.Primitives;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Demo.Pages;

public class CanvasBase : ComponentBase
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (CanvasRef.Ready)
        {
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
        CanvasRef.FillStyle("gray");
        CanvasRef.StrokeStyle("black");
        CanvasRef.LineWidth(1);
        CanvasRef.ClearRect(0, 0, OutputSize, OutputSize);
        CanvasRef.Transform(1, 0, 0, -1, 0, OutputSize);
        CanvasRef.BeginPath();

        var input = Value.ToLower();
        if (input.Contains("ellipse"))
        {
            input = input.Replace("ellipse", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            var ellipse = new Circle(parameters[0], parameters[1], parameters[2]);

            CanvasRef.Ellipse(ellipse.Position.X, (int)ellipse.Position.Y,
                ellipse.Radius,
                ellipse.Radius, 0, 0, 2.0f * (float)Math.PI);
        }
        else if (input.Contains("polygon"))
        {
            input = input.Replace("polygon", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            var polygon = new Polygon(parameters.ToArray());
            var vertices = new double[polygon.Vertices.Length * 2];
            var index = 0;
            for (var i = 0; i < polygon.Vertices.Length; i++)
            {
                vertices[index++] = polygon.Vertices[i].X;
                vertices[index++] = polygon.Vertices[i].Y;
            }

            CanvasRef.Polygon(vertices);
        }
        else if (input.Contains("polyline"))
        {
            input = input.Replace("polyline", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            var polyline = new PolyLine(parameters.ToArray());

            var vertices = new double[polyline.Vertices.Length*2];
            var index = 0;
            for (var i = 0; i < polyline.Vertices.Length; i++)
            {
                vertices[index++] = polyline.Vertices[i].X;
                vertices[index++] = polyline.Vertices[i].Y;
            }

            CanvasRef.PolyLine(vertices);
        }
        else if (input.Contains("point"))
        {
            input = input.Replace("point", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            var point = new Point(parameters[0], parameters[1]);

            CanvasRef.Ellipse(point.Position.X, point.Position.Y,
                1, 1, 0, 0, 2 * (float)Math.PI);
        }

        CanvasRef.Fill();
        CanvasRef.Stroke();
        CanvasRef.Transform(1, 0, 0, -1, 0, OutputSize);
    }

    #region Canvas

    public BlazorCanvas.BlazorCanvas CanvasRef { get; set; }
    public int OutputSize { get; set; } = 1000;
    public string Value { get; set; } = "ellipse 500,500,100,200,2";

    #endregion
}