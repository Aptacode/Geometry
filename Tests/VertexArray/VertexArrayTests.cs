using System.Numerics;
using Aptacode.Geometry.Vertices;
using Xunit;

namespace Aptacode.Geometry.Tests.VertexArray;

public class VertexArrayTests
{
    [Theory]
    [ClassData(typeof(VertexArrayEqualityTestDataGenerator))]
    public void VertexEquality(Vertices.VertexArray v1, Vertices.VertexArray v2, bool areEqual)
    {
        //Arrange
        //Act
        var equalityOperatorResult = v1 == v2;
        var inequalityOperatorResult = v1 != v2;
        var equalsMethodResult = v1.Equals(v2);
        var equalsObjectMethodResult = v1.Equals((object)v2);

        //Assert
        Assert.Equal(areEqual, equalityOperatorResult);
        Assert.Equal(!areEqual, inequalityOperatorResult);
        Assert.Equal(areEqual, equalsMethodResult);
        Assert.Equal(areEqual, equalsObjectMethodResult);
    }

    [Theory]
    [ClassData(typeof(VertexArrayConcatTestDataGenerator))]
    public void VertexArray_Concat(Vertices.VertexArray v1, Vertices.VertexArray v2, Vertices.VertexArray expected)
    {
        //Arrange
        //Act
        var sut = v1.Concat(v2);

        //Assert
        Assert.Equal(expected, sut);
    }

    [Theory]
    [ClassData(typeof(VertexArrayTranslateTestDataGenerator))]
    public void VertexArray_Translate(Vertices.VertexArray v1, Vector2 delta, Vertices.VertexArray expected)
    {
        //Arrange
        //Act
        v1.Translate(delta);

        //Assert
        Assert.Equal(expected, v1);
    }
}