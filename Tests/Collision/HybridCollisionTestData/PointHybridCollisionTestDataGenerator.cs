using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.HybridCollisionTestData;

public class PointHybridCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Point.Zero, Point.Zero, true },
        new object[] { Point.Zero, Ellipse.Create(0, 0, 1, 1, 0), true },
        new object[] { Point.Zero, Polygon.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), true },
        new object[] { Point.Zero, PolyLine.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), true },
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