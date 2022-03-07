using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PointCollisionTestData;

public class PolylineVector2CollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //PolyLine
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(-1, -1), false },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(0, -1), false },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(-1, 0), false },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(0, 0), true },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(5, 5), true },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(10, 10), true },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(11, 11), false },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(10, 11), false },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(11, 10), false }
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