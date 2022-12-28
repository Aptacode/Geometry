using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Tests.Collision.PointCollisionTestData;
using Aptacode.Geometry.Tests.Collision.PrimitiveCollisionTestData;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Aptacode.Geometry.Tests.Collision;

public class Collision_Tests
{
    private readonly ITestOutputHelper testOutputHelper;

    public Collision_Tests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Theory]
    [ClassData(typeof(PointPrimitiveCollisionTestDataGenerator))]
    [ClassData(typeof(EllipsePrimitiveCollisionTestDataGenerator))]
    [ClassData(typeof(PolygonPrimitiveCollisionTestDataGenerator))]
    [ClassData(typeof(PolylinePrimitiveCollisionTestDataGenerator))]
    public void PrimitiveCollidesWithPrimitive(Primitive p1, Primitive p2, bool collides)
    {
        //Arrange
        testOutputHelper.WriteLine($"{p1} - {p2}: {collides}");
        //Act
        var collidesWithPrimitiveResult = p1.CollidesWithPrimitive(p2);
        var collidesWithResult = p2 switch
        {
            Point primitive => p1.CollidesWith(primitive),
            Circle primitive => p1.CollidesWith(primitive),
            Polygon primitive => p1.CollidesWith(primitive),
            PolyLine primitive => p1.CollidesWith(primitive),
            _ => false
        };


        //Assert
        Assert.Equal(collides, collidesWithPrimitiveResult);
        Assert.Equal(collides, collidesWithResult);
    }

    [Theory]
    [ClassData(typeof(PointVector2CollisionTestDataGenerator))]
    [ClassData(typeof(EllipseVector2CollisionTestDataGenerator))]
    [ClassData(typeof(PolygonVector2CollisionTestDataGenerator))]
    [ClassData(typeof(PolylineVector2CollisionTestDataGenerator))]
    public void PrimitiveCollidesWithVector2(Primitive p1, Vector2 p2, bool collides)
    {
        //Arrange
        testOutputHelper.WriteLine($"{p1} - ({p2.X},{p2.Y}): {collides}");

        //Act
        var sut = p1.CollidesWith(p2);

        //Assert
        Assert.Equal(collides, sut);
    }
}