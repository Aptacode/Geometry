using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Utilities;
using Xunit;

namespace Aptacode.Geometry.Tests;

public class UnitTests
{
    [Fact]
    public void Perp_Test()
    {
        var a = new Vector2(6, 12);
        var b = a.Perp();

        Assert.Equal(new Vector2(12, -6), b);
    }

    [Fact]
    public void PerpDotXVectorCrossTest()
    {
        var a = new Vector2(6, 12);
        var b = new Vector2(12, 6);

        var perpDot = a.PerpDot(b);
        var cross = a.VectorCross(b);

        Assert.True(perpDot == -cross);
    }

    [Fact]
    public void newLineSegmentIntersection_Test()
    {
        var A1 = new Vector2(10, 10);
        var A2 = new Vector2(20, 10);

        var B1 = new Vector2(15, 10);
        var B2 = new Vector2(15, 15);

        Assert.True((A1, A2).Intersects((B1, B2)));
    }
}