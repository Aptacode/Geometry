using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Aptacode.Geometry.Tests.BoundingRectangleTests;

public class BoundingRectangleVector2CollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { new Geometry.Collision.Rectangles.BoundingRectangle(Vector2.Zero, Vector2.One), Vector2.One, true },
        new object[] { new Geometry.Collision.Rectangles.BoundingRectangle(Vector2.Zero, Vector2.One), new Vector2(0.5f), true },
        new object[] { new Geometry.Collision.Rectangles.BoundingRectangle(Vector2.Zero, Vector2.One), new Vector2(2), false },
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