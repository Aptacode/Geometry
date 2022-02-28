using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Tests.Primitives.Transformation.Scale;
using Aptacode.Geometry.Tests.Primitives.Transformation.Translate;
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

    [Theory]
    [ClassData(typeof(PointCenterScaleTestDataGenerator))]
    [ClassData(typeof(EllipseCenterScaleTestDataGenerator))]
    [ClassData(typeof(PolygonCenterScaleTestDataGenerator))]
    [ClassData(typeof(PolylineCenterScaleTestDataGenerator))]
    public void Primitive_CenterScale(Primitive p1, Vector2 delta, Primitive expected)
    {
        //Arrange
        //Act
        p1.ScaleAboutCenter(delta);

        //Assert
        Assert.Equal(p1, expected);
    }

    [Fact]
    public void Primitive_SetSize_Test()
    {
        //Arrange
        var rectangle = Polygon.Rectangle.FromTwoPoints(new Vector2(-1, -1), new Vector2(1, 1));
        //Act
        rectangle.SetSize(new Vector2(4, 4));

        //Assert
        var expectedRectangle = BoundingRectangle.FromTwoPoints(new Vector2(-2, -2), new Vector2(2, 2));
        Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
    }

    [Fact]
    public void Primitive_SetPosition_Test()
    {
        //Arrange
        var rectangle = Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One);
        //Act
        rectangle.SetPosition(new Vector2(2, 2));

        //Assert
        var expectedRectangle = BoundingRectangle.FromTwoPoints(new Vector2(1.5f, 1.5f), new Vector2(2.5f, 2.5f));
        Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
    }
}