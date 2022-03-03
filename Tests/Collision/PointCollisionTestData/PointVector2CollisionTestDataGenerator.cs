using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PointCollisionTestData;

public class PointVector2CollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Point
        new object[] { Point.Create(0, 0), Vector2.Zero, true },
        new object[] { Point.Create(0, 0), Vector2.One, false },
        new object[] { Point.Create(0, 0), new Vector2(0,1), false },
        new object[] { Point.Create(0, 0), new Vector2(1, 0), false }
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