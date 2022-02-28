using Aptacode.Geometry.Primitives;
using Xunit;

namespace Aptacode.Geometry.Tests.Primitives.ToString;

public class PrimitiveToStringTests
{
    [Theory]
    [ClassData(typeof(PointToStringTestDataGenerator))]
    [ClassData(typeof(EllipseToStringTestDataGenerator))]
    [ClassData(typeof(PolygonToStringTestDataGenerator))]
    [ClassData(typeof(PolylineToStringTestDataGenerator))]
    public void PrimitiveToStringTest(Primitive p1, string expected)
    {
        //Arrange
        //Act
        var toString = p1.ToString();

        //Assert
        Assert.Equal(expected, toString);
    }
}