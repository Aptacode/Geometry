using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.CollisionTests;

public class PolylinePrimitivePointCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //PolyLine
        new object[] { PolyLine.Create(0, 0, 10, 10), Vector2.Zero, true },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(5, 5), true },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(11, 11), false }
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