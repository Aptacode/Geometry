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

        [Fact]
        public void CircleAndCircle_FineCollision_Test()
        {
            //Arrange
            var circle1 = new Circle(new Vector2(2, 2), 2);
            var circle2 = new Circle(new Vector2(2, 3), 2);
            //Act
            var sut = circle1.CollidesWith(circle2, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void CircleAndPoint_FineCollision_Test()
        {
            //Arrange
            var circle = new Circle(new Vector2(2, 2), 2);
            var point = new Point(new Vector2(2, 1));
            //Act
            var sut = circle.CollidesWith(point, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void CircleAndPolygon_FineCollision_Test()
        {
            //Arrange
            var circle = new Circle(new Vector2(2, 2), 2);
            var polygon = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(4, 0));
            //Act
            var sut = circle.CollidesWith(polygon, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void CircleAndPolyLine_FineCollision_Test()
        {
            //Arrange
            var circle = new Circle(new Vector2(2, 2), 2);
            var polyLine = PolyLine.Create(new Vector2(0, 0), new Vector2(4, 4));
            //Act
            var sut = circle.CollidesWith(polyLine, _collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PointAndCircle_FineCollision_Test()
        {
            //Arrange
            var point = new Point(new Vector2(4, 4));
            var circle = new Circle(new Vector2(5, 5), 3);
            //Act
            var sut = point.CollidesWith(circle, _collisionDetector);
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

        [Fact]
        public void PolygonAndCircle_FineCollision_Test()
        {
            //Arrange
            var polygon = Triangle.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(4, 0));
            var circle = new Circle(new Vector2(2, 2), 2);
            //Act
            var sut = polygon.CollidesWith(circle, _collisionDetector);
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

        [Fact]
        public void PolyLineAndCircle_FineCollision_Test()
        {
            //Arrange
            var polyLine = PolyLine.Create(new Vector2(0, 3), new Vector2(4, 3));
            var circle = new Circle(new Vector2(2, 2), 2);
            //Act
            var sut = polyLine.CollidesWith(circle,
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
    }
}