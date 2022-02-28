using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Xunit;

namespace Aptacode.Geometry.Tests.CollisionTests;

public class Collision_Tests
{
    [Theory]
    [ClassData(typeof(PointPrimitiveCollisionTestDataGenerator))]
    [ClassData(typeof(EllipsePrimitiveCollisionTestDataGenerator))]
    [ClassData(typeof(PolygonPrimitiveCollisionTestDataGenerator))]
    [ClassData(typeof(PolylinePrimitiveCollisionTestDataGenerator))]
    public void PrimitiveCollidesWithPrimitive(Primitive p1, Primitive p2, bool collides)
    {
        //Arrange

        //Act
        var sut = p1.CollidesWithPrimitive(p2);

        //Assert
        Assert.Equal(collides, sut);
    }

    [Theory]
    [ClassData(typeof(PointPointCollisionTestDataGenerator))]
    [ClassData(typeof(EllipsePointCollisionTestDataGenerator))]
    [ClassData(typeof(PolygonPointCollisionTestDataGenerator))]
    [ClassData(typeof(PolylinePrimitivePointCollisionTestDataGenerator))]
    public void PrimitiveCollidesWithVector2(Primitive p1, Vector2 p2, bool collides)
    {
        //Arrange

        //Act
        var sut = p1.CollidesWith(p2);

        //Assert
        Assert.Equal(collides, sut);
    }

    [Theory]
    [ClassData(typeof(PointBoundingRectangleCollisionTestDataGenerator))]
    [ClassData(typeof(EllipseBoundingRectangleCollisionTestDataGenerator))]
    [ClassData(typeof(PolygonBoundingRectangleCollisionTestDataGenerator))]
    [ClassData(typeof(PolygonBoundingRectangleCollisionTestDataGenerator))]
    public void PrimitiveCollidesWithBoundingRectangle(Primitive p1, BoundingRectangle rectangle, bool collides)
    {
        //Arrange

        //Act
        var sut = p1.CollidesWith(rectangle);

        //Assert
        Assert.Equal(collides, sut);
    }
}