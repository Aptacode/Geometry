using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;
using Xunit;

namespace Aptacode.Geometry.Tests;

public class PrimitiveTests
{
    #region Equality

    public class PrimitiveEqualityTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            //Equality
            //p1.equals(p2) == isEqual

            //Point
            new object[] { Point.Zero, Point.Zero, true},
            new object[] { Point.Zero, Point.Unit, false},
            new object[] { Point.Create(2,2), Point.Create(2,2), true},

            //Ellipse
            new object[] { Ellipse.Create(0, 0, 1, 1, 0), Ellipse.Create(0, 0, 1, 1, 0), true },
            new object[] { Ellipse.Create(0, 0, 1, 1, 0), Ellipse.Create(1, 1, 1, 1, 0), false },
            new object[] { Ellipse.Create(0, 0, 1, 1, 0), Ellipse.Create(0, 0, 2, 2, 0), false },
            new object[] { Ellipse.Create(0, 0, 1, 2, 0), Ellipse.Create(0,0, 1, 2, 1), false },

            //Polygon
            new object[] { Polygon.Create(0,0,1,1,0,1), Polygon.Create(0, 0, 1, 1, 0, 1), true },
            new object[] { Polygon.Create(0,0,1,1,0,1), Polygon.Create(0, 0, 1, 1, 0, 2), false },
            new object[] { Polygon.Create(0,0,1,1,0,1), Polygon.Create(0, 0, 1, 1, 0, 1, 0, 2), false },  
            
            //Polyline
            new object[] { PolyLine.Create(0,0,1,1), PolyLine.Create(0, 0, 1, 1), true },
            new object[] { PolyLine.Create(0,0,1,1), PolyLine.Create(0, 0, 1, 0), false },
            new object[] { PolyLine.Create(0,0,1,1), PolyLine.Create(0, 0, 1, 1, 2, 2), false },


            new object[] { Polygon.Create(0, 0, 1, 1, 0, 1), PolyLine.Create(0, 0, 1, 1, 0, 1), false },
            new object[] { Point.Create(0, 0), PolyLine.Create(0, 0), false },
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

    [Theory]
    [ClassData(typeof(PrimitiveEqualityTestDataGenerator))]
    public void Primitive_Equality(Primitive p1, Primitive p2, bool areEqual)
    {
        //Arrange
        //Act
        var equalityOperatorResult = p1 == p2;
        var inequalityOperatorResult = p1 != p2;
        var equalsMethodResult = p1.Equals(p2);
        var equalsObjectMethodResult = p1.Equals((object)p2);

        //Assert
        Assert.Equal(areEqual, equalityOperatorResult);
        Assert.Equal(!areEqual, inequalityOperatorResult);
        Assert.Equal(areEqual, equalsMethodResult);
        Assert.Equal(areEqual, equalsObjectMethodResult);
    }

    #endregion


    [Fact]
    public void Primitive_SetSize_Test()
    {
        //Arrange
        var rectangle = Polygon.Rectangle.FromPositionAndSize(new Vector2(10, 10), new Vector2(10, 10));
        var expectedRectangle = BoundingRectangle.FromPositionAndSize(new Vector2(10, 10), new Vector2(20, 20));
        //Act
        rectangle.SetSize(new Vector2(20, 20));

        //Assert
        Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
    }

    [Fact]
    public void Primitive_SetPosition_Test()
    {
        //Arrange
        var rectangle = Polygon.Rectangle.FromPositionAndSize(new Vector2(10, 10), new Vector2(10, 10));
        var expectedRectangle = BoundingRectangle.FromPositionAndSize(new Vector2(20, 20), new Vector2(10, 10));
        //Act
        rectangle.SetPosition(new Vector2(20, 20));

        //Assert
        Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
    }

    [Fact]
    public void Polygon_CreateWithFloats_Test()
    {
        //Arrange
        var triangle = Polygon.Create(7, 7, 10, 13, 13, 7);
        //Act
        var expectedVertices = new VertexArray(new[] { new(7, 7), new Vector2(10, 13), new Vector2(13, 7) });
        //Assert
        Assert.Equal(expectedVertices, triangle.Vertices);
    }

    [Fact]
    public void Polygon_CreateWithVector2s_Test()
    {
        //Arrange
        var triangle = Polygon.Create(new Vector2(7, 7), new Vector2(10, 13), new Vector2(13, 7));
        //Act
        var expectedVertices = new VertexArray(new[] { new(7, 7), new Vector2(10, 13), new Vector2(13, 7) });
        //Assert
        Assert.Equal(expectedVertices, triangle.Vertices);
    }
}