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

        //Assert
        if (p1 != null)
        {
            var equalsMethodResult = p1.AreEqual(p2);
            Assert.Equal(areEqual, equalsMethodResult);
        }
        else
        {
            var equalsMethodResult = p2.AreEqual(p1);
            Assert.Equal(areEqual, equalsMethodResult);
        }
    }
}