using System.Collections;
using System.Collections.Generic;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Equality;

public class PolygonEqualityTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { new Polygon(0, 0, 1, 1, 0, 1), new Polygon(0, 0, 1, 1, 0, 1), true },
        new object[] { new Polygon(0, 0, 1, 1, 0, 1), new Polygon(0, 0, 1, 1, 0, 2), false },
        new object[] { new Polygon(0, 0, 1, 1, 0, 1), new Polygon(0, 0, 1, 1, 0, 1, 0, 2), false },
        new object[] { new Polygon(0, 0, 1, 1, 0, 1), null, false },
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