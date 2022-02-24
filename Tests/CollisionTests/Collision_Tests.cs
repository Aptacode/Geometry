using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Xunit;

namespace Aptacode.Geometry.Tests.CollisionTests;

public class Collision_Tests
{
    #region Point

    [Fact]
    public void PointAndPoint_Collision_Test()
    {
        //Arrange
        var p1 = Point.Create(new Vector2(1, 1));
        var p2 = Point.Create(new Vector2(1, 1));
        //Act
        var sut = p1.CollidesWith(p2);
        //Assert
        Assert.True(sut);
    }

    #endregion

    #region Ellipse

    [Fact]
    public void EllipseAndEllipse_IntersectionCollision_Test()
    {
        //Arrange
        var ellipse1 = Ellipse.Create(new Vector2(8, 5), new Vector2(3, 2), 0.0f);
        var ellipse2 = Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f);
        //Act
        var sut = ellipse1.CollidesWith(ellipse2);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void EllipseAndEllipse_ContainmentCollision_Test()
    {
        //Arrange
        var ellipse1 = Ellipse.Create(new Vector2(5, 5), new Vector2(1.5f, 1), 0.0f);
        var ellipse2 = Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f);
        //Act
        var sut = ellipse1.CollidesWith(ellipse2);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void CircleAndCircle_IntersectionCollision_Test()
    {
        //Arrange
        var circle1 = Ellipse.Create(new Vector2(45, 45), new Vector2(15, 15), 0.0f);
        var circle2 = Ellipse.Create(new Vector2(25, 25), new Vector2(15, 15), 0.0f);
        //Act
        var sut = circle1.CollidesWith(circle2);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void CircleAndCircle_ContainmentCollision_Test()
    {
        //Arrange
        var circle1 = Ellipse.Create(new Vector2(25, 25), new Vector2(10, 10), 0.0f);
        var circle2 = Ellipse.Create(new Vector2(25, 25), new Vector2(15, 15), 0.0f);
        //Act
        var sut = circle1.CollidesWith(circle2);
        //Assert
        Assert.True(sut);
    }


    [Fact]
    public void EllipseAndPoint_ContainmentCollision_Test()
    {
        //Arrange
        var ellipse = Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), (float)Math.PI / 4f);
        var point = Point.Create(new Vector2(7, 7));
        //Act
        var sut = ellipse.CollidesWith(point);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void EllipseAndPolygon_IntersectionCollision_Test()
    {
        //Arrange
        var ellipse = Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), (float)Math.PI / 4f);
        var polygon = Polygon.Create(new Vector2(3, 3), new Vector2(5, 7), new Vector2(7, 3));
        //Act
        var sut = ellipse.CollidesWith(polygon);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void EllipseAndPolygon_ContainmentCollision_Test2()
    {
        //Arrange
        var ellipse = Ellipse.Create(30, 30, 20, 10, 0.0f);
        var polygon = Polygon.Create(27, 27, 33, 27, 33, 33, 27, 33);
        //Act
        var sut = ellipse.CollidesWith(polygon);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void EllipseAndPolyLine_IntersectionCollision_Test()
    {
        //Arrange
        var ellipse = Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f);
        var polyLine = PolyLine.Create(new Vector2(3, 3), new Vector2(7, 7));
        //Act
        var sut = ellipse.CollidesWith(polyLine);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void EllipseAndPolyLine_ContainmentCollision_Test()
    {
        //Arrange
        var ellipse = Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f);
        var polyLine = PolyLine.Create(new Vector2(4, 5), new Vector2(6, 5));
        //Act
        var sut = ellipse.CollidesWith(polyLine);
        //Assert
        Assert.True(sut);
    }

    #endregion

    #region Polygon

    [Fact]
    public void PolygonAndEllipse_ContainmentCollision_Test()
    {
        //Arrange
        var polygon = Polygon.Create(20, 20, 40, 20, 40, 40, 20, 40);
        var ellipse = Ellipse.Create(new Vector2(30, 30), new Vector2(3, 2), (float)Math.PI / 4f);
        //Act
        var sut = polygon.CollidesWith(ellipse);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void PolygonAndPoint_ContainmentCollision_Test()
    {
        //Arrange
        var polygon = Polygon.Create(20, 20, 40, 20, 40, 40, 20, 40);
        var point = Point.Create(new Vector2(30, 30));
        //Act
        var sut = polygon.CollidesWith(point);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void PolygonAndPolygon_ContainmentCollision_Test()
    {
        //Arrange
        var polygon1 = Polygon.Create(20, 20, 40, 20, 40, 40, 20, 40);
        var polygon2 = Polygon.Create(25, 25, 35, 25, 35, 35, 25, 35);
        //Act
        var sut = polygon1.CollidesWith(polygon2);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void PolygonAndPolygon_IntersectionCollision_Test()
    {
        //Arrange
        var polygon1 = Polygon.Create(20, 20, 40, 20, 40, 40, 20, 40);
        var polygon2 = Polygon.Create(25, 20, 45, 20, 45, 40, 25, 40);
        //Act
        var sut = polygon1.CollidesWith(polygon2);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void PolygonAndPolyLine_ContainmentCollision_Test()
    {
        //Arrange
        var polygon = Polygon.Create(20, 20, 40, 20, 40, 40, 20, 40);
        var polyLine = PolyLine.Create(new Vector2(25, 30), new Vector2(35, 30));
        //Act
        var sut = polygon.CollidesWith(polyLine);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void PolygonAndPolyLine_IntersectionCollision_Test()
    {
        //Arrange
        var polygon = Polygon.Create(20, 20, 40, 20, 40, 40, 20, 40);
        var polyLine = PolyLine.Create(new Vector2(5, 30), new Vector2(55, 30));
        //Act
        var sut = polygon.CollidesWith(polyLine);
        //Assert
        Assert.True(sut);
    }

    #endregion

    #region PolyLine

    [Fact]
    public void PolyLineAndPoint_ContainmentCollision_Test()
    {
        //Arrange
        var polyLine = PolyLine.Create(new Vector2(0, 2), new Vector2(4, 2));
        var point = Point.Create(new Vector2(2, 2));
        //Act
        var sut = polyLine.CollidesWith(point);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void PolyLineAndPolyLine_IntersectionCollision_Test()
    {
        //Arrange
        var polyLine1 = PolyLine.Create(new Vector2(0, 2), new Vector2(4, 2));
        var polyLine2 = PolyLine.Create(new Vector2(0, 1), new Vector2(4, 3));
        //Act
        var sut = polyLine1.CollidesWith(polyLine2);
        //Assert
        Assert.True(sut);
    }

    #endregion

    #region Vector2

    [Fact]
    public void Vector2AndPoint_Collision_Test()
    {
        //Arrange
        var point = Point.Create(1, 1);
        var vector = new Vector2(1, 1);
        //Act
        var sut = point.CollidesWith(vector);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void Vector2AndPolyLine_Collision_Test()
    {
        //Arrange
        var polyline = PolyLine.Create(1, 1, 3, 3);
        var vector = new Vector2(2, 2);
        //Act
        var sut = polyline.CollidesWith(vector);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void Vector2AndPolygon_Collision_Test()
    {
        //Arrange
        var polygon = Polygon.Create(1, 1, 3, 3, 3, 1);
        var vector = new Vector2(3, 3);
        //Act
        var sut = polygon.CollidesWith(vector);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void Vector2AndEllipse_Collision_Test()
    {
        //Arrange
        var ellipse = Ellipse.Create(3, 3, 4, 2, 0.0f);
        var vector = new Vector2(3, 3);
        //Act
        var sut = ellipse.CollidesWith(vector);
        //Assert
        Assert.True(sut);
    }

    #endregion

    #region BoundingRectangle

    [Fact]
    public void BoundingRectangleAndBoundingRectangle_IntersectionCollision_Test()
    {
        //Arrange
        var boundingRect1 = BoundingRectangle.FromTwoPoints(new Vector2(10, 10), new Vector2(30, 30));
        var boundingRect2 = BoundingRectangle.FromTwoPoints(new Vector2(20, 15), new Vector2(40, 35));
        //Act
        var sut = boundingRect1.CollidesWith(boundingRect2);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void BoundingRectangleAndBoundingRectangle_ContainmentCollision_Test()
    {
        //Arrange
        var boundingRect1 = BoundingRectangle.FromTwoPoints(new Vector2(10, 10), new Vector2(30, 30));
        var boundingRect2 = BoundingRectangle.FromTwoPoints(new Vector2(15, 15), new Vector2(25, 25));
        //Act
        var sut = boundingRect1.CollidesWith(boundingRect2);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void BoundingRectangleAndPoint_Collision_Test()
    {
        //Arrange
        var boundingRect1 = BoundingRectangle.FromTwoPoints(new Vector2(10, 10), new Vector2(30, 30));
        var point = Point.Create(20, 20);
        //Act
        var sut = boundingRect1.CollidesWith(point);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void BoundingRectangleAndPolyLine_IntersectionCollision_Test()
    {
        //Arrange
        var boundingRect1 = BoundingRectangle.FromPositionAndSize(new Vector2(18, 18), new Vector2(10, 10));
        var polyline = PolyLine.Create(24, 28, 24, 16);
        //Act
        var sut = boundingRect1.CollidesWith(polyline);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void BoundingRectangleAndPolyLine_ContainmentCollision_Test()
    {
        //Arrange
        var boundingRect1 = BoundingRectangle.FromTwoPoints(new Vector2(10, 10), new Vector2(30, 30));
        var polyline = PolyLine.Create(15, 15, 25, 25);
        //Act
        var sut = boundingRect1.CollidesWith(polyline);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void BoundingRectangleAndPolygon_IntersectionCollision_Test()
    {
        //Arrange
        var boundingRect1 = BoundingRectangle.FromTwoPoints(new Vector2(10, 10), new Vector2(30, 30));
        var polyline = Polygon.Create(5, 5, 15, 35, 35, 5);
        //Act
        var sut = boundingRect1.CollidesWith(polyline);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void BoundingRectangleAndPolygon_ContainmentCollision_Test()
    {
        //Arrange
        var boundingRect1 = BoundingRectangle.FromTwoPoints(new Vector2(10, 10), new Vector2(30, 30));
        var polyline = Polygon.Create(12, 12, 20, 20, 28, 12);
        //Act
        var sut = boundingRect1.CollidesWith(polyline);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void BoundingRectangleAndEllipse_IntersectionCollision_Test()
    {
        //Arrange
        var boundingRect1 = BoundingRectangle.FromTwoPoints(new Vector2(10, 10), new Vector2(30, 30));
        var ellipse = Ellipse.Create(20, 20, 12, 15, 0.0f);
        //Act
        var sut = boundingRect1.CollidesWith(ellipse);
        //Assert
        Assert.True(sut);
    }

    [Fact]
    public void BoundingRectangleAndEllipse_ContainmentCollision_Test()
    {
        //Arrange
        var boundingRect1 = BoundingRectangle.FromTwoPoints(new Vector2(10, 10), new Vector2(30, 30));
        var ellipse = Ellipse.Create(20, 20, 4, 6, 0.0f);

        //Act
        var sut = boundingRect1.CollidesWith(ellipse);
        //Assert
        Assert.True(sut);
    }

    #endregion
}