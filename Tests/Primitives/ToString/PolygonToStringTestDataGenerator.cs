using System.Collections;
using System.Collections.Generic;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.ToString;

public class PolygonToStringTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Polygon.Create(0, 0, 1, 1, 0, 1), "Polygon (0,0), (1,1), (0,1)" }
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