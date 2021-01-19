using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;
using Aptacode.Geometry.Utilities;
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

        [Fact]

        public void Perp_Test()
        {
            var a = new Vector2(6, 12);
            var b = a.Perp();

            Assert.Equal(new Vector2(12, -6), b);
        }

        [Fact]
        public void PerpDotXVectorCrossTest()
        {
            var a = new Vector2(6, 12);
            var b = new Vector2(12, 6);

            var perpDot = a.PerpDot(b);
            var cross = a.VectorCross(b);

            Assert.True(perpDot == -cross);
        }

        [Fact]
        public void OnLineSegment_Test()
        {
            var a = new Vector2(0, 0);
            var b = new Vector2(6, 2);
            var c = new Vector2(3, 1);

            Assert.True(Helpers.newOnLineSegment((a, b), c));

        }

        [Fact]
        public void newLineSegmentIntersection_Test()
        {
            var A1 = new Vector2(10, 10);
            var A2 = new Vector2(20, 10);

            var B1 = new Vector2(15, 10);
            var B2 = new Vector2(15, 15);

            Assert.True(Helpers.newLineSegmentIntersection((A1, A2), (B1, B2)));
        }


        [Fact]
        public void SweepingLine_Test()
        {
            var sweepingLine = new SweepingLine();

            var poly1 = Polygon.Create(10, 10, 10, 15, 15, 20, 20, 15, 20, 10);
            var poly2 = Polygon.Create(15, 10, 15, 15, 20, 20, 25, 15, 25, 10);

            var collision = sweepingLine.RunSweepingLine(poly1, poly2);

            Assert.True(collision);
        }
    }
}