using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;
using Xunit;

namespace Aptacode.Geometry.Tests.CollisionTests
{
    public class FineCollision_Tests
    {
        private readonly CollisionDetector _collisionDetector = new FineCollisionDetector();

        #region Ellipse

        [Fact]
        public void EllipseAndEllipse_FineCollision_Test()
        {
            //Arrange
            var ellipse1 = new Ellipse(new Vector2(8, 5), new Vector2(3, 2), 0.0f);
            var ellipse2 = new Ellipse(new Vector2(5, 5), new Vector2(3, 2), 0.0f);
            //Act
            var sut = ellipse1.CollidesWith(ellipse2, _collisionDetector);
            //Assert
            Assert.True(sut);
        }
        
        [Fact]
        public void EllipseAndCircle_FineCollision_Test()
        {
            //Arrange
            var ellipse = new Ellipse(new Vector2(53, 45), new Vector2(15, 10), 0.0f);
            var circle = new Ellipse(new Vector2(25, 25), new Vector2(15, 15), 0.0f);
            //Act
            var sut = ellipse.CollidesWith(circle, _collisionDetector);
            //Assert
            Assert.True(sut);
        }



        [Fact]
        public void EllipseAndPoint_FineCollision_Test()
        {
            //Arrange
            var ellipse = new Ellipse(new Vector2(5, 5), new Vector2(3, 2), (float) Math.PI / 4f);
            var point = new Point(new Vector2(7, 7));
            //Act
            var sut = ellipse.CollidesWith(point, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void EllipseAndPolygon_FineCollision_Test()
        {
            //Arrange
            var ellipse = new Ellipse(new Vector2(5, 5), new Vector2(3, 2), (float) Math.PI / 4f);
            var polygon = Triangle.Create(new Vector2(3, 3), new Vector2(5, 7), new Vector2(7, 3));
            //Act
            var sut = ellipse.CollidesWith(polygon, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void EllipseAndPolygon_FineCollision_Test2()
        {
            //Arrange
            var ellipse = Ellipse.Create(30, 30, 15, 10, (float) Math.PI);
            var polygon = Rectangle.Create(25, 5, 10, 10);
            //Act
            var sut = ellipse.CollidesWith(polygon, _collisionDetector);
            //Assert
            Assert.False(sut);
        }

        [Fact]
        public void EllipseAndPolyLine_FineCollision_Test()
        {
            //Arrange
            var ellipse = new Ellipse(new Vector2(5, 5), new Vector2(3, 2), (float) Math.PI / 4f);
            var polyLine = PolyLine.Create(new Vector2(3, 3), new Vector2(7, 7));
            //Act
            var sut = ellipse.CollidesWith(polyLine, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        #endregion

        #region Point

        [Fact]
        public void PointAndEllipse_FineCollision_Test()
        {
            //Arrange
            var point = new Point(new Vector2(4, 4));
            var ellipse = new Ellipse(new Vector2(5, 5), new Vector2(3, 7), (float) Math.PI / 16f);
            //Act
            var sut = point.CollidesWith(ellipse, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PointAndPoint_FineCollision_Test()
        {
            //Arrange
            var p1 = new Point(new Vector2(1, 1));
            var p2 = new Point(new Vector2(1, 1));
            //Act
            var sut = p1.CollidesWith(p2, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PointAndPolygon_FineCollision_Test()
        {
            //Arrange
            var poly = new Rectangle(new Vector2(0, 0), new Vector2(20, 0), new Vector2(20, 10), new Vector2(0, 10));
            var points = poly.Vertices;
            //Act
            for (var i = 0; i <= poly.BottomRight.X; i += 2)
            {
                for (var j = 0; j <= poly.BottomRight.Y; j += 2)
                {
                    var pointAsPoint = new Point(new Vector2(i, j));
                    var sut = pointAsPoint.CollidesWith(poly, _collisionDetector);
                    Assert.True(sut);
                }
            }
        }

        [Fact]
        public void PointAndPolyLine_FineCollision_Test()
        {
            //Arrange
            var point = new Point(new Vector2(2, 2));
            var polyLine = PolyLine.Create(new Vector2(0, 2), new Vector2(4, 2));
            //Act
            var sut = point.CollidesWith(polyLine, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        #endregion

        #region Polygon

        [Fact]
        public void PolygonAndEllipse_FineCollision_Test()
        {
            //Arrange
            var polygon = Triangle.Create(new Vector2(3, 3), new Vector2(5, 7), new Vector2(7, 3));
            var ellipse = new Ellipse(new Vector2(5, 5), new Vector2(3, 2), (float) Math.PI / 4f);
            //Act
            var sut = polygon.CollidesWith(ellipse, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolygonAndPoint_FineCollision_Test()
        {
            //Arrange
            var polygon = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(4, 0));
            var point = new Point(new Vector2(4, 0));
            //Act
            var sut = polygon.CollidesWith(point, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolygonAndPolygon_FineCollision_Test()
        {
            //Arrange
            var polygon1 = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(4, 0));
            var polygon2 = Rectangle.Create(new Vector2(0, 0), new Vector2(4, 4));
            //Act
            var sut = polygon1.CollidesWith(polygon2, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolygonAndPolyLine_FineCollision_Test()
        {
            //Arrange
            var polygon = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(4, 0));
            var polyLine = PolyLine.Create(new Vector2(0, 0), new Vector2(4, 4));
            //Act
            var sut = polygon.CollidesWith(polyLine, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        #endregion

        #region PolyLine

        [Fact]
        public void PolyLineAndEllipse_FineCollision_Test()
        {
            //Arrange
            var polyLine = PolyLine.Create(new Vector2(7, 11), new Vector2(18, 19));
            var ellipse = new Ellipse(new Vector2(14, 14), new Vector2(7, 8), 0.0f);
            //Act
            var sut = polyLine.CollidesWith(ellipse,
                _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolyLineAndPoint_FineCollision_Test()
        {
            //Arrange
            var polyLine = PolyLine.Create(new Vector2(0, 2), new Vector2(4, 2));
            var point = new Point(new Vector2(2, 2));
            //Act
            var sut = polyLine.CollidesWith(point,
                _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolyLineAndPolygon_FineCollision_Test()
        {
            //Arrange
            var polyLine = PolyLine.Create(new Vector2(2, 0), new Vector2(2, 6));
            var polygon = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(0, 4));
            //Act
            var sut = polyLine.CollidesWith(polygon,
                _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolyLineAndPolyLine_FineCollision_Test()
        {
            //Arrange
            var polyLine1 = PolyLine.Create(new Vector2(0, 2), new Vector2(4, 2));
            var polyLine2 = PolyLine.Create(new Vector2(0, 1), new Vector2(4, 3));
            //Act
            var sut = polyLine1.CollidesWith(polyLine2,
                _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        #endregion
    }
}