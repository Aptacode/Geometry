﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Xunit;

namespace Aptacode.Geometry.Tests.BoundingRectangleTests;

public class BoundingRectangleEqualityTests
{
    [Theory]
    [ClassData(typeof(BoundingRectangleEqualityTestDataGenerator))]
    public void BoundingRectangleEquality_Equality(BoundingRectangle p1, BoundingRectangle p2, bool areEqual)
    {
        //Arrange
        //Act
        var equalityOperatorResult = p1 == p2;
        var inequalityOperatorResult = p1 != p2;

        if (p1 != null)
        {
            var equalsMethodResult = p1.Equals(p2);
            Assert.Equal(areEqual, equalsMethodResult);
        }

        //Assert
        Assert.Equal(areEqual, equalityOperatorResult);
        Assert.Equal(!areEqual, inequalityOperatorResult);
    }

    public class BoundingRectangleEqualityTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[]
            {
                new BoundingRectangle(Vector2.Zero, Vector2.One), new BoundingRectangle(Vector2.Zero, Vector2.One), true
            },
            new object[]
            {
                new BoundingRectangle(Vector2.Zero, Vector2.One), new BoundingRectangle(Vector2.Zero, new Vector2(2)),
                false
            },
            new object[] { new BoundingRectangle(Vector2.Zero, Vector2.One), null, false },
            new object[] { null, new BoundingRectangle(Vector2.Zero, Vector2.One), false },
            new object[] { null, null, true }
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