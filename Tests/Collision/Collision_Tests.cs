﻿using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Tests.Collision.BoundingRectangleTestData;
using Aptacode.Geometry.Tests.Collision.PointCollisionTestData;
using Aptacode.Geometry.Tests.Collision.PrimitiveCollisionTestData;
using Xunit;

namespace Aptacode.Geometry.Tests.Collision;

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
        var collidesWithPrimitiveResult = p1.CollidesWithPrimitive(p2);
        var collidesWithResult = p2 switch
        {
            Point primitive => p1.CollidesWith(primitive),
            Ellipse primitive => p1.CollidesWith(primitive),
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