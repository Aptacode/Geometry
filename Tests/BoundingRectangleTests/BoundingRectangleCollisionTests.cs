using System.Numerics;
using Xunit;

namespace Aptacode.Geometry.Tests.BoundingRectangleTests;

public class BoundingRectangleCollisionTests
{


    [Theory]
    [ClassData(typeof(BoundingRectangleBoundingRectangleCollisionTestDataGenerator))]
    public void BoundingRectangleBoundingRectangleCollision(Geometry.Collision.Rectangles.BoundingRectangle p1, Geometry.Collision.Rectangles.BoundingRectangle p2, bool collides)
    {
        //Arrange

        //Act
        var sut = p1.CollidesWith(p2);

        //Assert
        Assert.Equal(collides, sut);
    }

    [Theory]
    [ClassData(typeof(BoundingRectangleVector2CollisionTestDataGenerator))]
    public void BoundingRectangleVector2Collision(Geometry.Collision.Rectangles.BoundingRectangle p1, Vector2 p2, bool collides)
    {
        //Arrange

        //Act
        var sut = p1.CollidesWith(p2);

        //Assert
        Assert.Equal(collides, sut);
    }
}