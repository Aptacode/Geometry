using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Primitives;
using Xunit;

namespace Aptacode.Geometry.Tests
{
    public class UnitTests
    {
        [Fact]
        public void BoundingCircle_Test()
        {
            //Arrange
            var poly = PolyLine.Create(new Vector2(0, 0), new Vector2(6, 0), new Vector2(6, 6));
            //Act
            var sut = poly.MinimumBoundingCircle();
            //Assert
            Assert.Equal(new Vector2(3, 3), sut.Center);
        }

        [Fact]
        public void BoundingCircle_Test2()
        {
            //Arrange
            var poly = PolyLine.Create(new Vector2(3, 0), new Vector2(1, 0), new Vector2(0, 2), new Vector2(1, 4),
                new Vector2(3, 4), new Vector2(4, 2));
            //Act
            var sut = poly.MinimumBoundingCircle();
            //Assert
            Assert.Equal(new Vector2(2, 2), sut.Center);
        }

        [Fact]
        public void CircleFromThreePoints_Test()
        {
            //Arrange
            var point1 = new Vector2(0, 0);
            var point2 = new Vector2(3, 0);
            var point3 = new Vector2(3, 3);
            //Act
            var sut = BoundingCircle.FromThreePoints(point1, point2, point3);
            //Assert
            Assert.Equal(new Vector2(1.5f, 1.5f), sut.Center);
            //Assert.Equal(9.03f, sut.Radius);
        }

        [Fact]
        public void CircleFromThreePoints_Test2()
        {
            //Arrange
            var point1 = new Vector2(-6, 3);
            var point2 = new Vector2(-3, 2);
            var point3 = new Vector2(0, 3);
            //Act
            var sut = BoundingCircle.FromThreePoints(point1, point2, point3);
            //Assert
            Assert.Equal(new Vector2(-3f, 3f), sut.Center);
            //Assert.Equal(9.03f, sut.Radius);
        }
    }
}