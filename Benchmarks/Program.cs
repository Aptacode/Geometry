using BenchmarkDotNet.Running;

namespace Aptacode.Geometry.Benchmarks;

internal static class Program
{
    private static int Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
            .Run(args, new FastAndDirtyConfig());

        return 0;
    }
}