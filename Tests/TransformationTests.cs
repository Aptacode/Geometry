using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;
using Xunit;

namespace Aptacode.Geometry.Tests
{
    public class TransformationTests
    {
        [Fact]
        public void VertexArray_Translation_Test()
        {
            //Arrange
            var vertexArray = VertexArray.Create(new Vector2(1, 1), new Vector2(2, 1));
            var expectedVertexArray = VertexArray.Create(new Vector2(2, 2), new Vector2(3, 2));
            //Act
            vertexArray.Translate(new Vector2(1, 1));

            //Assert
            foreach (var vertex in expectedVertexArray.Vertices)
            {
                Assert.Contains(vertex, vertexArray.Vertices);
            }
        }

        [Fact]
        public void VertexArray_Rotation_Test()
        {
            //Arrange
            var vertexArray = VertexArray.Create(new Vector2(1, 1), new Vector2(2, 1));
            var expectedVertexArray = VertexArray.Create(new Vector2(1, 1), new Vector2(1, 2));
            //Act
            vertexArray.Rotate(new Vector2(1, 1), (float) Math.PI / 2);
            //Assert
            foreach (var vertex in expectedVertexArray.Vertices)
            {
                Assert.Contains(vertex, vertexArray.Vertices);
            }
        }

        [Fact]
        public void VertexArray_Scale_Test()
        {
            //Arrange
            var vertexArray = VertexArray.Create(new Vector2(2, 2), new Vector2(4, 2));
            var expectedVertexArray = VertexArray.Create(new Vector2(1, 2), new Vector2(5, 2));
            //Act
            vertexArray.Scale(new Vector2(3, 2), new Vector2(2, 1));
            //Assert
            foreach (var vertex in expectedVertexArray.Vertices)
            {
                Assert.Contains(vertex, vertexArray.Vertices);
            }
        }

        [Fact]
        public void VertexArray_BoundingRectangle_Scale_Test()
        {
            //Arrange
            var vertexArray = VertexArray.Create(new Vector2(10, 10), new Vector2(30, 10), new Vector2(30, 30), new Vector2(10, 30));
            var expectedBoundedRectangle = BoundingRectangle.FromTwoPoints(new Vector2(0, 0), new Vector2(40,40));

            //Act
            var actualBoundedRectangle = vertexArray.Scale(new Vector2(20, 20), new Vector2(2, 2));
            //Assert
            Assert.Equal(expectedBoundedRectangle, actualBoundedRectangle);
        }


        //[Fact]
        //public void PolygonTranslation_Test()
        //{
        //    //Arrange
        //    var poly = Polygon.Create(new Vector2(3, 3), new Vector2(7, 3), new Vector2(7, 5), new Vector2(3, 5));
        //    var sut = Polygon.Create(new Vector2(3, 3), new Vector2(7, 3), new Vector2(7, 5), new Vector2(3, 5));
        //    //Act
        //    sut.Translate(new Vector2(1, 0));
        //    //Assert
        //    var expectedVertices = VertexArray.Create(new Vector2(4, 3), new Vector2(8, 3), new Vector2(8, 5),
        //        new Vector2(4, 5));
        //    var expectedBoundingCircleCenter = poly.BoundingCircle.Center + new Vector2(1, 0);
        //    var expectedBoundingCircleRadius = poly.BoundingCircle.Radius;
        //    var expectedEdges = new[]
        //    {
        //        (new Vector2(4, 3), new Vector2(8, 3)), (new Vector2(8, 3), new Vector2(8, 5)),
        //        (new Vector2(8, 5), new Vector2(4, 5)), (new Vector2(4, 5), new Vector2(4, 3))
        //    };
        //    //Assert
        //    foreach (var vertex in expectedVertices.Vertices)
        //    {
        //        Assert.CollidesWith(vertex, sut.Vertices.Vertices);
        //    }

        //    Assert.Equal(expectedBoundingCircleCenter, sut.BoundingCircle.Center);
        //    Assert.Equal(expectedBoundingCircleRadius, sut.BoundingCircle.Radius);
        //    foreach (var edge in expectedEdges)
        //    {
        //        Assert.CollidesWith(edge, sut.Edges);
        //    }
        //}

        //[Fact]
        //public void PolygonRotation_Test()
        //{
        //    //Arrange
        //    var original = Polygon.Create(new Vector2(3, 3), new Vector2(7, 3), new Vector2(7, 5), new Vector2(3, 5));
        //    var sut = Polygon.Create(new Vector2(3, 3), new Vector2(7, 3), new Vector2(7, 5), new Vector2(3, 5));
        //    //Act
        //    sut.Rotate((float)Math.PI / 2);
        //    var expectedVertices = VertexArray.Create(new Vector2(4, 2), new Vector2(6, 2), new Vector2(6, 6),
        //        new Vector2(4, 6));
        //    var expectedBoundingCircleCenter = original.BoundingCircle.Center;
        //    var expectedBoundingCircleRadius = original.BoundingCircle.Radius;
        //    var expectedEdges = new[]
        //    {
        //        (new Vector2(4, 2), new Vector2(6, 2)), (new Vector2(6, 2), new Vector2(6, 6)),
        //        (new Vector2(6, 6), new Vector2(4, 6)), (new Vector2(4, 6), new Vector2(4, 2))
        //    };
        //    //Assert
        //    foreach (var vertex in expectedVertices.Vertices)
        //    {
        //        Assert.CollidesWith(vertex, sut.Vertices.Vertices);
        //    }

        //    Assert.Equal(expectedBoundingCircleCenter, sut.BoundingCircle.Center);
        //    Assert.Equal(expectedBoundingCircleRadius, sut.BoundingCircle.Radius);
        //    foreach (var edge in expectedEdges)
        //    {
        //        Assert.CollidesWith(edge, sut.Edges);
        //    }
        //}


        [Fact]
        public void Polygon_ScaleFromCenter_Test()
        {
            //Arrange
            var rectangle = Polygon.Rectangle.FromPositionAndSize(new Vector2(10, 10), new Vector2(6, 6));
            var expectedRectangle = BoundingRectangle.FromPositionAndSize(new Vector2(7, 7), new Vector2(12, 12));
            //Act
            rectangle.ScaleAboutCenter(new Vector2(2, 2));
            //Assert

            Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
        }

        [Fact]
        public void Polygon_ScaleFromTopLeft_Test()
        {
            //Arrange
            var rectangle = Polygon.Create(7, 7, 10, 13, 13, 7);
            var expectedRectangle = BoundingRectangle.FromPositionAndSize(new Vector2(7, 7), new Vector2(12, 12));
            //Act
            rectangle.ScaleAboutTopLeft(new Vector2(2, 2));
            //Assert

            Assert.Equal(expectedRectangle, rectangle.BoundingRectangle);
        }


    }
}