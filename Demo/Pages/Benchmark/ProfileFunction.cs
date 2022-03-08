namespace Aptacode.Geometry.Demo.Pages.Benchmark;

public abstract class ProfileFunction
{
    public abstract string Title();

    public virtual void Setup()
    {
    }

    public virtual void Reset()
    {
    }

    public abstract void Run();
}