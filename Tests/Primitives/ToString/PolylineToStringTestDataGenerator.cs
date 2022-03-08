using System.Collections;
using System.Collections.Generic;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.ToString;

public class PolylineToStringTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { PolyLine.Zero, "PolyLine (0,0), (0,0)" },
        new object[] { PolyLine.Create(0, 0, 1, 1), "PolyLine (0,0), (1,1)" }
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