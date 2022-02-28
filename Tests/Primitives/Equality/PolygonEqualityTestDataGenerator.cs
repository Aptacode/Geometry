using System.Collections;
using System.Collections.Generic;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Equality;

public class PolygonEqualityTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Polygon.Create(0, 0, 1, 1, 0, 1), Polygon.Create(0, 0, 1, 1, 0, 1), true },
        new object[] { Polygon.Create(0, 0, 1, 1, 0, 1), Polygon.Create(0, 0, 1, 1, 0, 2), false },
        new object[] { Polygon.Create(0, 0, 1, 1, 0, 1), Polygon.Create(0, 0, 1, 1, 0, 1, 0, 2), false }
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