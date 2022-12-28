using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Tests.Primitives.Transformation.Scale;
using Aptacode.Geometry.Tests.Primitives.Transformation.Translate;
using Xunit;
using Xunit.Abstractions;

namespace Aptacode.Geometry.Tests.Primitives.Transformation;

public class PrimitiveTransformationTests
{
    private readonly ITestOutputHelper testOutput;

    public PrimitiveTransformationTests(ITestOutputHelper testOutput)
    {
        this.testOutput = testOutput;
    }
    [Theory]
    [ClassData(typeof(PointTranslationTestDataGenerator))]
    [ClassData(typeof(EllipseTranslationTestDataGenerator))]
    [ClassData(typeof(PolygonTranslationTestDataGenerator))]
    [ClassData(typeof(PolylineTranslationTestDataGenerator))]
    public void Primitive_Translation(Primitive p1, Vector2 delta, Primitive expected)
    {
        //Arrange
        testOutput.WriteLine($"{p1} + ({delta.X},{delta.Y}) => {expected}");

        //Act
        p1.Translate(delta);

        //Assert
        Assert.True(expected.AreEqual(p1));
    }

    [Theory]
    [ClassData(typeof(PointCenterScaleTestDataGenerator))]
    [ClassData(typeof(EllipseCenterScaleTestDataGenerator))]
    [ClassData(typeof(PolygonCenterScaleTestDataGenerator))]
    [ClassData(typeof(PolylineCenterScaleTestDataGenerator))]
    public void Primitive_CenterScale(Primitive p1, Vector2 delta, Primitive expected)
    {
        //Arrange
        testOutput.WriteLine($"{p1} + ({delta.X},{delta.Y}) => {expected}");

        //Act
        var actual = p1.ScaleAboutCenter(delta);

        testOutput.WriteLine($"Actual: {actual}");

        //Assert
        Assert.True(expected.AreEqual(p1));
    }
}