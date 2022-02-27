using System.Diagnostics;

namespace Aptacode.Geometry.Demo.Pages;

public class ProfileRunner
{
    private readonly IReadOnlyList<ProfileFunction> _profileFunctions;

    public ProfileRunner(IReadOnlyList<ProfileFunction> profileFunctions)
    {
        _profileFunctions = profileFunctions;
    }

    public List<ProfileFunctionResult> Run(int batchCount, int batchSize)
    {
        var results = new List<ProfileFunctionResult>();

        foreach (var profileFunction in _profileFunctions)
        {
            //Warmup
            profileFunction.Setup();
            for (var i = 0; i < 10; i++)
            {
                profileFunction.Reset();
                profileFunction.Run();
            }

            //Actual Run
            var stopWatch = new Stopwatch();
            var functionDurations = new List<double>();
            profileFunction.Setup();

            for (var i = 0; i < batchCount; i++)
            {
                stopWatch.Restart();

                for (var j = 0; j < batchSize; j++)
                {
                    profileFunction.Reset();

                    profileFunction.Run();
                }

                stopWatch.Stop();
                functionDurations.Add(stopWatch.Elapsed.TotalMilliseconds / batchCount);
            }

            results.Add(new ProfileFunctionResult(profileFunction.Title(), functionDurations));
        }

        return results;
    }
}