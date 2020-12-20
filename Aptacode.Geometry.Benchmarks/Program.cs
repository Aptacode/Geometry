using System;
using BenchmarkDotNet.Running;

namespace Aptacode.Geometry.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TransformationBenchmarks>();
        }
    }
}
