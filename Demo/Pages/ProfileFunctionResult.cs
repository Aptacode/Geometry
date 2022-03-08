namespace Aptacode.Geometry.Demo.Pages;

public record ProfileFunctionResult(string Title, IReadOnlyList<double> Elapsed)
{
    public double Fastest => Elapsed.Min();
    public double Slowest => Elapsed.Max();
    public double Average => Elapsed.Average();
}