using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.HybridCollisionTestData;

public class EllipseHybridCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Ellipse.Create(0, 0, 1, 1, 0), Point.Zero, true },
        new object[] { Ellipse.Create(0, 0, 1, 1, 0), Ellipse.Create(0, 0, 1, 1, 0), true },
        new object[] { Ellipse.Create(0, 0, 1, 1, 0), Polygon.Create(Vector2.Zero, Vector2.One, new Vector2(0,1)), true },
        new object[] { Ellipse.Create(0, 0, 1, 1, 0), PolyLine.Create(Vector2.Zero, Vector2.One, new Vector2(0,1)), true },
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