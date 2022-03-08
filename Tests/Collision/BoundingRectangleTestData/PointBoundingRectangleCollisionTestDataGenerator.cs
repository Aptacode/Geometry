using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.BoundingRectangleTestData;

public class PointBoundingRectangleCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Point
        new object[] { Point.Zero, new BoundingRectangle(Vector2.Zero, Vector2.One), true },
        new object[] { Point.Unit, new BoundingRectangle(Vector2.Zero, new Vector2(2, 2)), true },
        new object[] { Point.Zero, new BoundingRectangle(Vector2.One, Vector2.One), false }
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