using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Primitives;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace Aptacode.Geometry.Tests
{
    public class UnitTests
    {

        private readonly CollisionDetector collisionDetector = new CoarseCollisionDetector();

        [Fact]
        public void PointAndPoint_CoarseCollision_Test()
        {
            //Arrange
            var p1 = new Point(new Vector2(1, 1));
            var p2 = new Point(new Vector2(1, 1));
            //Act
            var sut = p1.CollidesWith(p2, collisionDetector);
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
            var sut = point.CollidesWith(polyLine, collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PointAndPolygon_CoarseCollision_Test()
        {
            //Arrange
            var point = new Point(new Vector2(2, 2));
            var poly = Polygon.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(0, 4));
            //Act
            var sut = point.CollidesWith(poly, collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PointAndCircle_CoarseCollision_Test()
        {
            //Arrange
            var point = new Point(new Vector2(4, 4));
            var circle = new Circle(new Vector2(5, 5), 3);
            //Act
            var sut = point.CollidesWith(circle, collisionDetector);
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
            var sut = polyLine.CollidesWith(point, collisionDetector); //These primitives do not actually collide, just their bounding circles
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
            var sut = polyLine1.CollidesWith(polyLine2, collisionDetector); //These primitives do not actually collide, just their bounding circles
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolyLineAndPolygon_CoarseCollision_Test()
        {
            //Arrange
            var polyLine = PolyLine.Create(new Vector2(0, 5), new Vector2(4, 5));
            var polygon = Polygon.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(0, 4));
            //Act
            var sut = polyLine.CollidesWith(polygon, collisionDetector); //These primitives do not actually collide, just their bounding circles
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolyLineAndCircle_CoarseCollision_Test()
        {
            //Arrange
            var polyLine = PolyLine.Create(new Vector2(0, 5), new Vector2(4, 5));
            var circle = new Circle(new Vector2(2, 2), 2);
            //Act
            var sut = polyLine.CollidesWith(circle, collisionDetector); //These primitives do not actually collide, just their bounding circles
            //Assert
            Assert.True(sut);
        }
        
        [Fact]
        public void PolygonAndPoint_CoarseCollision_Test()
        {
            //Arrange
            var polygon = Polygon.Create(new Vector2(0, 0), new Vector2(2, 4), new Vector2(4, 0));
            var point = new Point(new Vector2(4, 0));
            //Act
            var sut = polygon.CollidesWith(point, collisionDetector);
            //Assert
            Assert.True(sut);
        }

        [Fact]
        public void PolygonAndPolyLine_CoarseCollision_Test()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public void PolygonAndPolygon_CoarseCollision_Test()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public void PolygonAndCircle_CoarseCollision_Test()
        {
            //Arrange
            //Act
            //Assert
        }
        [Fact]
        public void CircleAndPoint_CoarseCollision_Test()
        {
            //Arrange
            //Act
            //Assert
        }        
        
        [Fact]
        public void CircleAndPolyLine_CoarseCollision_Test()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public void CircleAndPolygon_CoarseCollision_Test()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public void CircleAndCircle_CoarseCollision_Test()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}
