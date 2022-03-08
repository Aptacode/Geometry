using System.Numerics;
using Aptacode.Geometry.Primitives;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Demo.Pages;

public class AsciiBase : ComponentBase
{
    public int[,] Output { get; set; } = new int[10, 10];

    public string Value { get; set; } = "ellipse 0,0,1,2,2";

    protected override void OnInitialized()
    {
        var outputSize = 10;
        Output = new int[outputSize, outputSize];

        UpdateEllipse();

        base.OnInitialized();
    }

    protected void OnInputChanged(ChangeEventArgs eventArgs)
    {
        Value = eventArgs.Value?.ToString() ?? string.Empty;
        UpdateEllipse();
    }

    protected void OnEnterPressed()
    {
        UpdateEllipse();
    }

    private void UpdateEllipse()
    {
        var input = Value.ToLower();
        if (input.Contains("ellipse"))
        {
            input = input.Replace("ellipse", "");
            input = input.Replace(" ", "");
            var parameters = input.Split(",").Select(int.Parse).ToList();

            var ellipse = Ellipse.Create(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);
            Draw(ellipse);
        }
    }

    private void Draw(Ellipse ellipse)
    {
        for (var i = 0; i < Output.GetLength(0); i++)
        {
            for (var j = 0; j < Output.GetLength(1); j++)
            {
                Output[i, j] = ellipse.CollidesWith(new Vector2(i - 4, j - 4)) ? 1 : 0;
            }
        }
        StateHasChanged();
    }
}