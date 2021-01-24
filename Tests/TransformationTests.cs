using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
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
    }
}