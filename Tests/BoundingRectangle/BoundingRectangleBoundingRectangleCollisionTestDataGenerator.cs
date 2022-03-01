using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;

namespace Aptacode.Geometry.Tests.Primitives.Equality;

public class BoundingRectangleBoundingRectangleCollisionTestDataGenerator : IEnumerable<object[]>
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