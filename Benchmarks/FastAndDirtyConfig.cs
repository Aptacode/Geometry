using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Perfolizer.Horology;

namespace Aptacode.Geometry.Benchmarks;

public class FastAndDirtyConfig : ManualConfig
{
    public FastAndDirtyConfig()
    {
        Add(DefaultConfig.Instance); // *** add default loggers, reporters etc? ***

        AddJob(Job.Default
            .WithLaunchCount(1) // benchmark process will be launched only once
            .WithIterationTime(TimeInterval.Millisecond * 150) // 100ms per iteration
            .WithWarmupCount(3) // 3 warmup iteration
            .WithIterationCount(3)
        );
    }
}