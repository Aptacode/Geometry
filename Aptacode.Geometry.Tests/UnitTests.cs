using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Aptacode.Geometry.Primitives.Polygons;
using Xunit;

namespace Aptacode.Geometry.Tests
{
    public class UnitTests
    {

        [Fact]
        public void CircleFromThreePoints_Test()
        {
            //Arrange
            var point1 = new Vector2(3, 9);
            var point2 = new Vector2(15, 9);
            var point3 = new Vector2(0, 3);
            //Act
            var sut = BoundingCircleAlgorithm.CreateCircleFromThreePoints(point1, point2, point3);
            //Assert
            Assert.Equal(new Vector2(9, 2.25f), sut.Position);
            //Assert.Equal(9.03f, sut.Radius);
        }

        [Fact]
        public void BoundingCircle_Test()
        {
            //Arrange
            var poly = PolyLine.Create(new Vector2(0, 0), new Vector2(6, 0), new Vector2(6, 6));
            //Act
            var sut = BoundingCircleAlgorithm.MinimumBoundingCircle(poly);
            //Assert
            Assert.Equal(new Vector2(3, 3), sut.Position);
        }

        [Fact]
        public void BoundingCircle_Test2()
        {
            //Arrange
            var poly = PolyLine.Create(new Vector2(3, 0), new Vector2(1, 0), new Vector2(0, 2), new Vector2(1, 4), new Vector2(3, 4), new Vector2(4,2));
            //Act
            var sut = BoundingCircleAlgorithm.MinimumBoundingCircle(poly);
            //Assert
            Assert.Equal(new Vector2(2, 2), sut.Position);
        }
    }
}
