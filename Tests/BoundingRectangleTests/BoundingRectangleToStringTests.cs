using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Xunit;

namespace Aptacode.Geometry.Tests.BoundingRectangleTests;

public class BoundingRectangleToStringTests
{
    [Theory]
    [ClassData(typeof(BoundingRectangleToStringTestDataGenerator))]
    public void BoundingRectangleToString(BoundingRectangle p1, string expected)
    {
        //Arrange
        //Act
        var toString = p1.ToString();

        //Assert
        Assert.Equal(expected, toString);
    }

    public class BoundingRectangleToStringTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[]
                { new BoundingRectangle(Vector2.Zero, Vector2.One), "BoundingRectangle (0,1), (1,1), (1,0), (0,0)" }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}