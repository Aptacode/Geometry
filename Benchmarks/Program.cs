using System;
using BenchmarkDotNet.Running;

namespace Aptacode.Geometry.Benchmarks;

internal class Program
{
    private static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<TransformationBenchmarks>();
        Console.WriteLine(summary);
        Console.ReadLine();
    }
}