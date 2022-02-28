using System.Collections;
using System.Collections.Generic;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Equality;

public class PolylineEqualityTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { PolyLine.Create(0, 0, 1, 1), PolyLine.Create(0, 0, 1, 1), true },
        new object[] { PolyLine.Create(0, 0, 1, 1), PolyLine.Create(0, 0, 1, 0), false },
        new object[] { PolyLine.Create(0, 0, 1, 1), PolyLine.Create(0, 0, 1, 1, 2, 2), false }
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