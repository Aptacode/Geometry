using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;
using Xunit;

namespace Aptacode.Geometry.Tests
{
    public class PrimitiveTests
    {
        [Fact]
        public void Primitive_SetSize_Test()
        {
            //Arrange
            var rectangle = Polygon.Rectangle.FromPositionAndSize(new Vector2(10, 10), new Vector2(10, 10));
            var expectedRectangle = BoundingRectangle.FromPositionAndSize(new Vector2(10, 10), new Vector2(20, 20));
            //Act
            rectangle.SetSize(new Vector2(20, 20));

            //Assert
            Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
        }

        [Fact]
        public void Primitive_SetPosition_Test()
        {
            //Arrange
            var rectangle = Polygon.Rectangle.FromPositionAndSize(new Vector2(10, 10), new Vector2(10, 10));
            var expectedRectangle = BoundingRectangle.FromPositionAndSize(new Vector2(20, 20), new Vector2(10, 10));
            //Act
            rectangle.SetPosition(new Vector2(20, 20));

            //Assert
            Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
        }

        [Fact]

        public void Polygon_CreateWithFloats_Test()
        {
            //Arrange
            var triangle = Polygon.Create(7, 7, 10, 13, 13, 7);
            //Act
            var expectedVertices = new VertexArray(new Vector2[] { new Vector2(7, 7), new Vector2(10, 13), new Vector2(13, 7) });
            //Assert
            Assert.Equal(expectedVertices, triangle.Vertices);
        }
        [Fact]

        public void Polygon_CreateWithVector2s_Test()
        {
            //Arrange
            var triangle = Polygon.Create(new Vector2(7, 7), new Vector2(10, 13), new Vector2(13, 7));
            //Act
            var expectedVertices = new VertexArray(new Vector2[] { new Vector2(7, 7), new Vector2(10, 13), new Vector2(13, 7) });
            //Assert
            Assert.Equal(expectedVertices, triangle.Vertices);
        }

    }
}