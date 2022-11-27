using System.Numerics;
using Aptacode.Geometry.Primitives;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Demo.Pages;

public class AsciiBase : ComponentBase
{
    private int _outputSize = 10;
    public int[,] Output { get; set; } = new int[10, 10];

    public string Value { get; set; } = "ellipse 0,0,1,2,2";

    public int OutputSize
    {
        get => _outputSize;
        set
        {
            _outputSize = value;
            Update();
        }
    }

    protected override void OnInitialized()
    {
        Update();

        base.OnInitialized();
    }

    protected void OnInputChanged(ChangeEventArgs eventArgs)
    {
        Value = eventArgs.Value?.ToString() ?? string.Empty;
        Update();
    }

    protected void OnOutputSizeInputChanged(ChangeEventArgs eventArgs)
    {
        OutputSize = int.Parse(eventArgs.Value?.ToString() ?? string.Empty);
        Update();
    }

    private void Update()
    {
        Primitive primitive = null;

        var input = Value.ToLower();
        if (input.Contains("ellipse"))
        {
            input = input.Replace("ellipse", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            primitive = Ellipse.Create(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);
        }
        else if (input.Contains("polygon"))
        {
            input = input.Replace("polygon", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            primitive = Polygon.Create(parameters.ToArray());
        }
        else if (input.Contains("polyline"))
        {
            input = input.Replace("polyline", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            primitive = PolyLine.Create(parameters.ToArray());
        }
        else if (input.Contains("point"))
        {
            input = input.Replace("point", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(float.Parse).ToList();

            primitive = Point.Create(parameters[0], parameters[1]);
        }

        if (primitive != null)
        {
            Draw(primitive);
        }
    }

    private void Draw(Primitive primitive)
    {
        Output = new int[OutputSize, OutputSize];

        var offset = (int)Math.Round(OutputSize / 2.0f) - 1;

        for (var i = 0; i < Output.GetLength(0); i++)
        {
            for (var j = 0; j < Output.GetLength(1); j++)
            {
                Output[i, j] = primitive.CollidesWith(new Vector2(j - offset, i - offset)) ? 1 : 0;
            }
        }

        StateHasChanged();
    }

    protected string GetColour(int i, int j)
    {
        return Output[i, j] == 1 ? "red" : "black";
    }
}