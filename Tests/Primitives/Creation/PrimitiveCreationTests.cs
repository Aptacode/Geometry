using Aptacode.Geometry.Primitives;
using Xunit;

namespace Aptacode.Geometry.Tests.Primitives.Creation;

public class PrimitiveCreationTests
{
    [Theory]
    [ClassData(typeof(PointCreationTestDataGenerator))]
    [ClassData(typeof(EllipseCreationTestDataGenerator))]
    [ClassData(typeof(PolygonCreationTestDataGenerator))]
    [ClassData(typeof(PolylineCreationTestDataGenerator))]
    public void PrimitiveCreation(Primitive primitive, Vertices.VertexArray expected)
    {
        //Arrange
        //Act
        //Assert
        Assert.Equal(expected, primitive.Vertices);
    }
}