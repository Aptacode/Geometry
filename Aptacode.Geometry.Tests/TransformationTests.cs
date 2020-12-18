using System;
using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;
using Xunit;

namespace Aptacode.Geometry.Tests
{
    public class TransformationTests
    {
        [Fact]
        public void VertexArray_Translation_Test()
        {
            //Arrange
            var vertexArray = VertexArray.Create(new Vector2(1, 1), new Vector2(2, 1));
            var expectedVertexArray = VertexArray.Create(new Vector2(2, 2), new Vector2(3, 2));
            //Act
            var sut = vertexArray.Translate(new Vector2(1, 1));
            //Assert
            foreach (var vertex in expectedVertexArray.Vertices)
            {
                Assert.Contains(vertex, sut.Vertices);
            }
        }

        [Fact]
        public void VertexArray_Rotation_Test()
        {
            //Arrange
            var vertexArray = VertexArray.Create(new Vector2(1, 1), new Vector2(2, 1));
            var expectedVertexArray = VertexArray.Create(new Vector2(1, 1), new Vector2(1, 2));
            //Act
            var sut = vertexArray.Rotate(new Vector2(1, 1), (float) Math.PI / 2);
            //Assert
            foreach (var vertex in expectedVertexArray.Vertices)
            {
                Assert.Contains(vertex, sut.Vertices);
            }
        }

        [Fact]
        public void VertexArray_Scale_Test()
        {
            //Arrange
            var vertexArray = VertexArray.Create(new Vector2(2, 2), new Vector2(4, 2));
            var expectedVertexArray = VertexArray.Create(new Vector2(1, 2), new Vector2(5, 2));
            //Act
            var sut = vertexArray.Scale(new Vector2(3, 2), new Vector2(2, 1));
            //Assert
            foreach (var vertex in expectedVertexArray.Vertices)
            {
                Assert.Contains(vertex, sut.Vertices);
            }
        }


        [Fact]
        public void PolygonTranslation_Test()
        {
            //Arrange
            var poly = Polygon.Create(new Vector2(3, 3), new Vector2(7, 3), new Vector2(7, 5), new Vector2(3, 5));
            //Act
            var sut = poly.Translate(new Vector2(1, 0));
            //Assert
            var expectedVertices = VertexArray.Create(new Vector2(4, 3), new Vector2(8, 3), new Vector2(8, 5),
                new Vector2(4, 5));
            var expectedBoundingCircleCenter = poly.BoundingCircle.Center + new Vector2(1, 0);
            var expectedBoundingCircleRadius = poly.BoundingCircle.Radius;
            var expectedEdges = new[]
            {
                (new Vector2(4, 3), new Vector2(8, 3)), (new Vector2(8, 3), new Vector2(8, 5)),
                (new Vector2(8, 5), new Vector2(4, 5)), (new Vector2(4, 5), new Vector2(4, 3))
            };
            //Assert
            foreach (var vertex in expectedVertices.Vertices)
            {
                Assert.Contains(vertex, sut.Vertices);
            }

            Assert.Equal(expectedBoundingCircleCenter, sut.BoundingCircle.Center);
            Assert.Equal(expectedBoundingCircleRadius, sut.BoundingCircle.Radius);
            foreach (var edge in expectedEdges)
            {
                Assert.Contains(edge, sut.Edges);
            }
        }

        [Fact]
        public void PolygonRotation_Test()
        {
            //Arrange
            var poly = Polygon.Create(new Vector2(3, 3), new Vector2(7, 3), new Vector2(7, 5), new Vector2(3, 5));
            //Act
            var sut = poly.Rotate((float) Math.PI / 2);
            var expectedVertices = VertexArray.Create(new Vector2(4, 2), new Vector2(6, 2), new Vector2(6, 6),
                new Vector2(4, 6));
            var expectedBoundingCircleCenter = poly.BoundingCircle.Center;
            var expectedBoundingCircleRadius = poly.BoundingCircle.Radius;
            var expectedEdges = new[]
            {
                (new Vector2(4, 2), new Vector2(6, 2)), (new Vector2(6, 2), new Vector2(6, 6)),
                (new Vector2(6, 6), new Vector2(4, 6)), (new Vector2(4, 6), new Vector2(4, 2))
            };
            //Assert
            foreach (var vertex in expectedVertices.Vertices)
            {
                Assert.Contains(vertex, sut.Vertices);
            }

            Assert.Equal(expectedBoundingCircleCenter, sut.BoundingCircle.Center);
            Assert.Equal(expectedBoundingCircleRadius, sut.BoundingCircle.Radius);
            foreach (var edge in expectedEdges)
            {
                Assert.Contains(edge, sut.Edges);
            }
        }
    }
}