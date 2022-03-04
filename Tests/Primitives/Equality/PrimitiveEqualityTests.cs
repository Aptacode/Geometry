using Aptacode.Geometry.Primitives;
using Xunit;

namespace Aptacode.Geometry.Tests.Primitives.Equality;

public class PrimitiveEqualityTests
{
    [Theory]
    [ClassData(typeof(PointEqualityTestDataGenerator))]
    [ClassData(typeof(EllipseEqualityTestDataGenerator))]
    [ClassData(typeof(PolygonEqualityTestDataGenerator))]
    [ClassData(typeof(PolylineEqualityTestDataGenerator))]
    public void Primitive_Equality(Primitive p1, Primitive p2, bool areEqual)
    {
        //Arrange
        //Act
        var equalityOperatorResult = p1 == p2;
        var inequalityOperatorResult = p1 != p2;

        //Assert
        if (p1 != null)
        {
            var equalsMethodResult = p1.Equals(p2);
            Assert.Equal(areEqual, equalsMethodResult);
        }
        Assert.Equal(areEqual, equalityOperatorResult);
        Assert.Equal(!areEqual, inequalityOperatorResult);
    }
}