using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;
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
        public void BoundingRectangle_Test()
        {
            //Arrange
            var ellipse = Ellipse.Create(15, 15, 10, 5, 0.0f);
            //Act
            var sut = ellipse.MinimumBoundingRectangle();
            //Assert
            Assert.Equal(new Vector2(5, 10), sut.TopLeft);
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

        [Fact]
        public void StandardFormEllipse_Test()
        {
            var ellipse = new Ellipse(new Vector2(5, 5), new Vector2(5, 3), 0);
            var sut = ellipse.GetStandardForm();
            Assert.True(Math.Abs(9 / 225f - sut.A) < Constants.Tolerance);
            Assert.True(Math.Abs(25 / 225f - sut.C) < Constants.Tolerance);
            Assert.Equal(0, sut.B);
            Assert.True(Math.Abs(-90 / 225f - sut.D) < Constants.Tolerance);
            Assert.True(Math.Abs(-250 / 225f - sut.E) < Constants.Tolerance);
            Assert.True(Math.Abs(625 / 225f - sut.F) < Constants.Tolerance);
        }

        [Fact]
        public void QuarticHasRealRoots_Test()
        {
            var q0 = 1;
            var q1 = 1;
            var q2 = 1;
            var q3 = 1;
            var q4 = 1;

            var sut = Ellipse.QuarticHasRealRoots(q0, q1, q2, q3, q4);

            Assert.False(sut);
        }
    }
}