using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Polygons;
using Xunit;

namespace Aptacode.Geometry.Tests.CollisionTests
{
    public class CoarseCollision_Tests
    {
        private readonly CollisionDetector _collisionDetector = new CoarseCollisionDetector();

        #region Ellipse

        [Fact]
        public void CircleAndCircle_CoarseCollision_Test()
        {
            //Arrange
            var circle1 = new Ellipse(new Vector2(2, 2), new Vector2(2, 2), 0);
            var circle2 = new Ellipse(new Vector2(2, 3), new Vector2(2, 2), 0);
            //Act
            var sut = circle1.CollidesWith(circle2, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void EllipseAndEllipse_CoarseCollision_Test()
        {
            //Arrange
            var ellipse1 = new Ellipse(new Vector2(4, 2), new Vector2(2, 2), 0);
            var ellipse2 = new Ellipse(new Vector2(4, 5), new Vector2(4, 2), 0);
            //Act
            var sut = ellipse1.CollidesWith(ellipse2, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void EllipseAndPoint_CoarseCollision_Test()
        {
            //Arrange
            var ellipse = new Ellipse(new Vector2(2, 2), new Vector2(2, 1), 0.0f);
            var point = new Point(new Vector2(2, 0));
            //Act
            var sut = ellipse.CollidesWith(point, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void EllipseAndPolygon_CoarseCollision_Test()
        {
            //Arrange
            var ellipse = new Ellipse(new Vector2(5, 5), new Vector2(3, 5), 0.0f);
            var polygon = Triangle.Create(new Vector2(7, 7), new Vector2(9, 11), new Vector2(11, 7));
            //Act
            var sut = ellipse.CollidesWith(polygon, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void EllipseAndPolyLine_CoarseCollision_Test()
        {
            //Arrange
            var ellipse = new Ellipse(new Vector2(2, 2), new Vector2(2, 1), 0.0f);
            var polyLine = PolyLine.Create(new Vector2(4, 0), new Vector2(4, 4));
            //Act
            var sut = ellipse.CollidesWith(polyLine, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        #endregion

        #region Point

        [Fact]
        public void PointAndEllipse_CoarseCollision_Test()
        {
            //Arrange
            var point = new Point(new Vector2(3, 3));
            var ellipse = new Ellipse(new Vector2(5, 5), new Vector2(5, 1), 0.0f);
            //Act
            var sut = point.CollidesWith(ellipse, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PointAndPoint_CoarseCollision_Test()
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
        public void PointAndPolygon_CoarseCollision_Test()
        {
            //Arrange
            var point = new Point(new Vector2(2, 2));
            var poly = new Triangle(new Vector2(0, 0), new Vector2(2, 4), new Vector2(0, 4));
            //Act
            var sut = point.CollidesWith(poly, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PointAndPolyLine_CoarseCollision_Test()
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
        public void PolygonAndEllipse_CoarseCollision_Test()
        {
            //Arrange
            var ellipse = new Ellipse(new Vector2(5, 5), new Vector2(3, 5), 0.0f);
            var polygon = Triangle.Create(new Vector2(7, 7), new Vector2(9, 11), new Vector2(11, 7));
            //Act
            var sut = polygon.CollidesWith(ellipse, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolygonAndPoint_CoarseCollision_Test()
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
        public void PolygonAndPolygon_CoarseCollision_Test()
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
        public void PolygonAndPolyLine_CoarseCollision_Test()
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
        public void PolyLineAndCircle_CoarseCollision_Test()
        {
            //Arrange
            var polyLine = PolyLine.Create(new Vector2(0, 5), new Vector2(4, 5));
            var circle = new Ellipse(new Vector2(3, 3), new Vector2(4, 1), 0.0f);
            //Act
            var sut = polyLine.CollidesWith(circle,
                _collisionDetector); //These primitives do not actually collide, just their bounding circles
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolyLineAndPoint_CoarseCollision_Test()
        {
            //Arrange
            var polyLine = PolyLine.Create(new Vector2(0, 2), new Vector2(4, 2));
            var point = new Point(new Vector2(2, 1));
            //Act
            var sut = polyLine.CollidesWith(point,
                _collisionDetector); //These primitives do not actually collide, just their bounding circles
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolyLineAndPolygon_CoarseCollision_Test()
        {
            //Arrange
            var polyLine = PolyLine.Create(new Vector2(0, 5), new Vector2(4, 5));
            var polygon = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(0, 4));
            //Act
            var sut = polyLine.CollidesWith(polygon,
                _collisionDetector); //These primitives do not actually collide, just their bounding circles
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolyLineAndPolyLine_CoarseCollision_Test()
        {
            //Arrange
            var polyLine1 = PolyLine.Create(new Vector2(0, 2), new Vector2(4, 2));
            var polyLine2 = PolyLine.Create(new Vector2(0, 3), new Vector2(4, 3));
            //Act
            var sut = polyLine1.CollidesWith(polyLine2,
                _collisionDetector); //These primitives do not actually collide, just their bounding circles
            //Assert
            Assert.True(sut);
        }

        #endregion
    }
}