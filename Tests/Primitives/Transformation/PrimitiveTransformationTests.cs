using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Tests.Primitives.Transformation.Translation;
using Xunit;

namespace Aptacode.Geometry.Tests.Primitives.Transformation;

public class PrimitiveTransformationTests
{
    [Theory]
    [ClassData(typeof(PointTranslationTestDataGenerator))]
    [ClassData(typeof(EllipseTranslationTestDataGenerator))]
    [ClassData(typeof(PolygonTranslationTestDataGenerator))]
    [ClassData(typeof(PolylineTranslationTestDataGenerator))]
    public void Primitive_Translation(Primitive p1, Vector2 delta, Primitive expected)
    {
        //Arrange
        //Act
        p1.Translate(delta);

        //Assert
        Assert.Equal(p1, expected);
    }

    [Fact]
    public void Polygon_ScaleFromCenter_Test()
    {
        //Arrange
        var rectangle = Polygon.Rectangle.FromPositionAndSize(new Vector2(10, 10), new Vector2(6, 6));
        var expectedRectangle = BoundingRectangle.FromPositionAndSize(new Vector2(7, 7), new Vector2(12, 12));
        //Act
        rectangle.ScaleAboutCenter(new Vector2(2, 2));
        //Assert

        Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
    }

    [Fact]
    public void Polygon_ScaleFromTopLeft_Test()
    {
        //Arrange
        var rectangle = Polygon.Create(7, 7, 10, 13, 13, 7);
        var expectedRectangle = BoundingRectangle.FromPositionAndSize(new Vector2(7, 7), new Vector2(12, 12));
        //Act
        rectangle.ScaleAboutTopLeft(new Vector2(2, 2));
        //Assert

        Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
    }

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
}