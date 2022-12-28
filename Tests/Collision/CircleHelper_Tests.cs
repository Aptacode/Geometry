using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Tests.Collision.PointCollisionTestData;
using Aptacode.Geometry.Tests.Collision.PrimitiveCollisionTestData;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Aptacode.Geometry.Tests.Collision;

public class CircleHelper_Tests
{
    private readonly ITestOutputHelper testOutputHelper;

    public CircleHelper_Tests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData(0,2,2,0,0,0,1,false)]//Fully outside
    [InlineData(0,2,2,0,0,0,10,true)]//Fully inside
    [InlineData(1,-1,-1,1,0,0,1,true)]//Touching
    [InlineData(0,0,2,0,0,0,1,true)]//Partially inside
    public void CircleLineSegmentIntersection(float aX, float aY, float bX, float bY, float cX, float cY, float cR, bool shouldIntersect)
    {
        //Arrange
        var a = new Vector2(aX, aY);
        var b = new Vector2(bX, bY);
        var c = new Vector2(cX, cY);

        //Act
        var intersects = (a,b).IntersectsCircle(c, cR);

        //Assert
        Assert.Equal(shouldIntersect, intersects);
    }
}