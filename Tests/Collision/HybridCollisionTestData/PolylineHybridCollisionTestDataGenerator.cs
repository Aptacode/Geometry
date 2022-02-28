using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PointCollisionTestData;

public class PolylineHybridCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { PolyLine.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), Point.Zero, true },
        new object[] { PolyLine.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), Ellipse.Create(0, 0, 1, 1, 0), true },
        new object[] { PolyLine.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), Polygon.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), true },
        new object[] { PolyLine.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), PolyLine.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), true },
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