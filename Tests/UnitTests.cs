using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Utilities;
using Xunit;

namespace Aptacode.Geometry.Tests;

public class UnitTests
{
    [Fact]
    public void BoundingRectangle_Test()
    {
        //Arrange
        var ellipse = Ellipse.Create(15, 15, 10, 5, 0.0f);
        //Act
        var sut = ellipse.BoundingRectangle;
        //Assert
        Assert.Equal(new Vector2(5, 20), sut.TopLeft);
    }

    [Fact]
    public void QuarticHasRealRoots_Test()
    {
        var q0 = 1;
        var q1 = 1;
        var q2 = 1;
        var q3 = 1;
        var q4 = 1;

        var sut = Ellipse.QuarticHasRealRoots(q0, q1, q2, q3, q4);

        Assert.False(sut);
    }

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
    public void OnLineSegment_Test()
    {
        var a = new Vector2(0, 0);
        var b = new Vector2(6, 2);
        var c = new Vector2(3, 1);

        Assert.True((a, b).NewOnLineSegment(c));
    }

    [Fact]
    public void newLineSegmentIntersection_Test()
    {
        var A1 = new Vector2(10, 10);
        var A2 = new Vector2(20, 10);

        var B1 = new Vector2(15, 10);
        var B2 = new Vector2(15, 15);

        Assert.True((A1, A2).LineSegmentIntersection((B1, B2)));
    }
}