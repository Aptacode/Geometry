using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;
using Aptacode.Geometry.Vertices;
using Xunit;

namespace Aptacode.Geometry.Tests
{
    public class TransformationTests
    {
        [Fact]
        public void PolygonTranslation_Test()
        {
            //Arrange
            var poly = Polygon.Create(new Vector2(3, 3), new Vector2(7, 3), new Vector2(7, 5), new Vector2(3, 5));
            //Act
            var sut = poly.Translate(new Vector2(1, 0));
            //Assert
            Assert.Equal(new Vector2(4, 3), sut.Vertices[0]);
            Assert.Equal(new Vector2(8, 3), sut.Vertices[1]);
            Assert.Equal(new Vector2(8, 5), sut.Vertices[2]);
            Assert.Equal(new Vector2(4, 5), sut.Vertices[3]);
        }

        [Fact]
        public void PolygonRotation_Test()
        {
            //Arrange
            var poly = Polygon.Create(new Vector2(3, 3), new Vector2(7, 3), new Vector2(7, 5), new Vector2(3, 5));
            //Act
            var sut = poly.Rotate((float)Math.PI/2);
            var expectedVertices = VertexArray.Create(new Vector2(4, 2), new Vector2(6, 2), new Vector2(6, 6), new Vector2(4, 6));
            //Assert
            foreach(var vertex in expectedVertices.Vertices)
            {
                Assert.Contains(vertex, sut.Vertices);
            }
        }
    }
}