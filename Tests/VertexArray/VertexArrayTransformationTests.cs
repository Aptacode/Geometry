using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;
using Xunit;

namespace Aptacode.Geometry.Tests.VertexArray;

public class VertexArrayTransformationTests
{
    [Fact]
    public void VertexArray_Translation_Test()
    {
        //Arrange
        var vertexArray = Vertices.VertexArray.Create(new Vector2(1, 1), new Vector2(2, 1));
        var expectedVertexArray = Vertices.VertexArray.Create(new Vector2(2, 2), new Vector2(3, 2));
        //Act
        vertexArray.Translate(new Vector2(1, 1));

        //Assert
        foreach (var vertex in expectedVertexArray.Vertices) Assert.Contains(vertex, vertexArray.Vertices);
    }

    [Fact]
    public void VertexArray_Rotation_Test()
    {
        //Arrange
        var vertexArray = Vertices.VertexArray.Create(new Vector2(1, 1), new Vector2(2, 1));
        var expectedVertexArray = Vertices.VertexArray.Create(new Vector2(1, 1), new Vector2(1, 2));
        //Act
        vertexArray.Rotate(new Vector2(1, 1), (float)Math.PI / 2);
        //Assert
        foreach (var vertex in expectedVertexArray.Vertices) Assert.Contains(vertex, vertexArray.Vertices);
    }

    [Fact]
    public void VertexArray_Scale_Test()
    {
        //Arrange
        var vertexArray = Vertices.VertexArray.Create(new Vector2(2, 2), new Vector2(4, 2));
        var expectedVertexArray = Vertices.VertexArray.Create(new Vector2(1, 2), new Vector2(5, 2));
        //Act
        vertexArray.Scale(new Vector2(3, 2), new Vector2(2, 1));
        //Assert
        foreach (var vertex in expectedVertexArray.Vertices) Assert.Contains(vertex, vertexArray.Vertices);
    }

    [Fact]
    public void VertexArray_BoundingRectangle_Scale_Test()
    {
        //Arrange
        var vertexArray = Vertices.VertexArray.Create(new Vector2(10, 10), new Vector2(30, 10), new Vector2(30, 30),
            new Vector2(10, 30));
        var expectedBoundedRectangle = new BoundingRectangle(new Vector2(0, 0), new Vector2(40, 40));

        //Act
        var actualBoundedRectangle = vertexArray.Scale(new Vector2(20, 20), new Vector2(2, 2));
        //Assert
        Assert.Equal(expectedBoundedRectangle, actualBoundedRectangle);
    }
}