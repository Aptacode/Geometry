using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Xunit;

namespace Aptacode.Geometry.Tests.CollisionTests;

public class Collision_Tests
{
    [Theory]
    [ClassData(typeof(PrimitivePrimitiveCollisionTestDataGenerator))]
    public void PrimitiveCollidesWithPrimitive(Primitive p1, Primitive p2, bool collides)
    {
        //Arrange

        //Act
        var sut = p1.CollidesWithPrimitive(p2);

        //Assert
        Assert.Equal(collides, sut);
    }

    [Theory]
    [ClassData(typeof(PrimitivePointCollisionTestDataGenerator))]
    public void PrimitiveCollidesWithVector2(Primitive p1, Vector2 p2, bool collides)
    {
        //Arrange

        //Act
        var sut = p1.CollidesWith(p2);

        //Assert
        Assert.Equal(collides, sut);
    }

    [Theory]
    [ClassData(typeof(PrimitiveBoundingRectangleCollisionTestDataGenerator))]
    public void PrimitiveCollidesWithBoundingRectangle(Primitive p1, BoundingRectangle rectangle, bool collides)
    {
        //Arrange

        //Act
        var sut = p1.CollidesWith(rectangle);

        //Assert
        Assert.Equal(collides, sut);
    }

    public class PrimitivePrimitiveCollisionTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            //Point Point
            new object[] { Point.Create(0, 0), Point.Create(0, 0), true },
            new object[] { Point.Create(0, 0), Point.Create(1, 1), false },

            //Ellipse Ellipse
            new object[] { Ellipse.Create(8, 5, 3, 2, 0), Ellipse.Create(5, 5, 3, 2, 0), true }, //ellipse intersection
            new object[]
                { Ellipse.Create(5, 5, 1.5f, 1, 0), Ellipse.Create(5, 5, 3, 2, 0), true }, //ellipse containment
            new object[]
                { Ellipse.Create(10, 10, 10, 10, 0), Ellipse.Create(20, 20, 10, 10, 0), true }, //circle intersection
            new object[]
                { Ellipse.Create(10, 10, 10, 10, 0), Ellipse.Create(10, 10, 5, 5, 0), true }, //circle containment
            new object[]
            {
                Ellipse.Create(Vector2.Zero, Vector2.One, 0), Ellipse.Create(new Vector2(2, 2), Vector2.One, 0), false
            },

            //Ellipse Point
            new object[]
            {
                Ellipse.Create(5, 5, 3, 2, (float)Math.PI / 4f), Point.Create(7, 7), true
            }, 
            new object[] { Ellipse.Create(Vector2.Zero, Vector2.One, 0), Point.Create(new Vector2(2, 2)), false },

            //Ellipse PolyLine
            new object[]
            {
                Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f),
                PolyLine.Create(new Vector2(4, 5), new Vector2(6, 5)), true
            }, 
            new object[]
            {
                Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f),
                PolyLine.Create(new Vector2(3, 3), new Vector2(7, 7)), true
            }, 
            new object[]
            {
                Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f),
                PolyLine.Create(new Vector2(3, 3), new Vector2(7, 7)), true
            }, 
            new object[]
            {
                Ellipse.Create(Vector2.Zero, Vector2.One, 0.0f), PolyLine.Create(new Vector2(2, 2), new Vector2(3, 3)),
                false
            }, 

            //Ellipse Polygon
            new object[]
            {
                Ellipse.Create(30, 30, 20, 10, 0.0f), Polygon.Create(27, 27, 33, 27, 33, 33, 27, 33), true
            },
            new object[]
            {
                Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), (float)Math.PI / 4f),
                Polygon.Create(new Vector2(3, 3), new Vector2(5, 7), new Vector2(7, 3)), true
            },
            new object[]
            {
                Ellipse.Create(Vector2.Zero, Vector2.One, 0.0f),
                Polygon.Rectangle.FromPositionAndSize(new Vector2(2, 2), Vector2.One), false
            },

            //Polyline PolyLine
            new object[]
                { PolyLine.Create(Vector2.Zero, Vector2.One), PolyLine.Create(Vector2.One, new Vector2(2, 2)), true },
            new object[]
            {
                PolyLine.Create(Vector2.Zero, new Vector2(10, 10)),
                PolyLine.Create(new Vector2(10, 0), new Vector2(0, 10)), true
            },
            new object[]
            {
                PolyLine.Create(Vector2.Zero, Vector2.One), PolyLine.Create(new Vector2(2, 2), new Vector2(3, 3)), false
            },

            //PolyLine Polygon
            new object[]
            {
                Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One),
                Polygon.Rectangle.FromPositionAndSize(Vector2.One, Vector2.One), true
            },
            new object[]
            {
                Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One),
                Polygon.Rectangle.FromPositionAndSize(new Vector2(2, 2), Vector2.One), false
            },
            new object[]
            {
                Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One),
                Polygon.Rectangle.FromPositionAndSize(new Vector2(3, 3), Vector2.One), false
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class PrimitivePointCollisionTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            //Point
            new object[] { Point.Create(0, 0), Vector2.Zero, true },
            new object[] { Point.Create(0, 0), Vector2.One, false },

            //Ellipse
            new object[] { Ellipse.Create(0, 0, 10, 10, 0), Vector2.One, true },
            new object[] { Ellipse.Create(0, 0, 10, 10, 0), new Vector2(20, 20), false },

            //PolyLine
            new object[] { PolyLine.Create(0, 0, 10, 10), Vector2.Zero, true },
            new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(5, 5), true },
            new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(11, 11), false },

            //Polygon
            new object[] { Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One), Vector2.Zero, true },
            new object[] { Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One), new Vector2(2, 2), false }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class PrimitiveBoundingRectangleCollisionTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            //Point
            new object[] { Point.Create(0, 0), BoundingRectangle.FromPositionAndSize(Vector2.Zero, Vector2.One), true },
            new object[] { Point.Create(0, 0), BoundingRectangle.FromPositionAndSize(Vector2.One, Vector2.One), false },

            //Ellipse
            new object[]
            {
                Ellipse.Create(0, 0, 1, 1, 0), BoundingRectangle.FromPositionAndSize(Vector2.Zero, Vector2.One), true
            },
            new object[]
            {
                Ellipse.Create(0, 0, 1, 1, 0), BoundingRectangle.FromPositionAndSize(new Vector2(2, 2), Vector2.One),
                false
            },

            //PolyLine
            new object[]
                { PolyLine.Create(0, 0, 1, 1), BoundingRectangle.FromPositionAndSize(Vector2.Zero, Vector2.One), true },
            new object[]
            {
                PolyLine.Create(0, 0, 1, 1), BoundingRectangle.FromPositionAndSize(new Vector2(2, 2), Vector2.One),
                false
            },

            //Polygon
            new object[]
            {
                Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One),
                BoundingRectangle.FromPositionAndSize(Vector2.Zero, Vector2.One), true
            },
            new object[]
            {
                Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One),
                BoundingRectangle.FromPositionAndSize(new Vector2(2, 2), Vector2.One), false
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

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
        var vector = Vector2.One;
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