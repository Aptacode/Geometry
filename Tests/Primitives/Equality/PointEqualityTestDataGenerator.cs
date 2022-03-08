using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Equality;

public class PointEqualityTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Point.Zero, Point.Zero, true },
        new object[] { Point.Unit, Point.Unit, true },
        new object[] { Point.Create(2, 2), Point.Create(2, 2), true },
        new object[] { Point.Unit, Point.Zero, false },
        new object[] { Point.Zero, Ellipse.Unit, false },
        new object[] { Point.Zero, Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One), false },
        new object[] { Point.Zero, PolyLine.Create(Vector2.Zero, Vector2.One), false },
        new object[] { Point.Zero, null, false },
        new object[] { null, Point.Zero, false },
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