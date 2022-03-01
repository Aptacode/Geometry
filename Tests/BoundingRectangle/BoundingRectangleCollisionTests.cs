using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Xunit;

namespace Aptacode.Geometry.Tests.Primitives.Equality;

public class BoundingRectangleCollisionTests
{
    public class BoundingRectangleCollisionTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[] { BoundingRectangle.FromTwoPoints(Vector2.Zero, Vector2.One), BoundingRectangle.FromTwoPoints(Vector2.Zero, Vector2.One), true },
            new object[] { BoundingRectangle.FromTwoPoints(Vector2.Zero, Vector2.One), BoundingRectangle.FromTwoPoints(Vector2.One, new Vector2(2)), true },
            new object[] { BoundingRectangle.FromTwoPoints(Vector2.Zero, Vector2.One), BoundingRectangle.FromTwoPoints(new Vector2(2), new Vector2(3)), false },

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

    [Theory]
    [ClassData(typeof(BoundingRectangleCollisionTestDataGenerator))]
    public void BoundingRectangleCollision(BoundingRectangle p1, BoundingRectangle p2, bool collides)
    {
        //Arrange

        //Act
        var sut = p1.CollidesWith(p2);

        //Assert
        Assert.Equal(collides, sut);
    }
}