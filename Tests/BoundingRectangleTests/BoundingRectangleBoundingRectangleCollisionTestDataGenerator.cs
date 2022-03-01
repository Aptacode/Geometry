using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Aptacode.Geometry.Tests.BoundingRectangleTests;

public class BoundingRectangleBoundingRectangleCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Geometry.Collision.Rectangles.BoundingRectangle.FromTwoPoints(Vector2.Zero, Vector2.One), Geometry.Collision.Rectangles.BoundingRectangle.FromTwoPoints(Vector2.Zero, Vector2.One), true },
        new object[] { Geometry.Collision.Rectangles.BoundingRectangle.FromTwoPoints(Vector2.Zero, Vector2.One), Geometry.Collision.Rectangles.BoundingRectangle.FromTwoPoints(Vector2.One, new Vector2(2)), true },
        new object[] { Geometry.Collision.Rectangles.BoundingRectangle.FromTwoPoints(Vector2.Zero, Vector2.One), Geometry.Collision.Rectangles.BoundingRectangle.FromTwoPoints(new Vector2(2), new Vector2(3)), false },

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