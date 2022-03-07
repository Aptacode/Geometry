using System;
using BenchmarkDotNet.Running;

namespace Aptacode.Geometry.Benchmarks;

internal static class Program
{
    private static int Main(string[] args)
    {
        try
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                .Run(args, new FastAndDirtyConfig());

            return 0;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
            return 1;
        }
    }
}