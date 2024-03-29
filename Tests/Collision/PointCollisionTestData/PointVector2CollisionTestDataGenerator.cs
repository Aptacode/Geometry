﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PointCollisionTestData;

public class PointVector2CollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Point
        new object[] { new Point(0, 0), new Vector2(0, 0), true },
        new object[] { new Point(0, 0), new Vector2(0, 1), false },
        new object[] { new Point(0, 0), new Vector2(1, 0), false },
        new object[] { new Point(0, 0), new Vector2(1, 1), false }
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