using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PrimitiveCollisionTestData;

public class PointPrimitiveCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Point Point
        new object[] { Point.Create(0, 0), Point.Create(0, 0), true },
        new object[] { Point.Create(0, 0), Point.Create(1, 1), false },

        //Ellipse Point
        new object[]
        {
            Point.Create(7, 7), Ellipse.Create(5, 5, 3, 2, (float)Math.PI / 4f), true
        },
        new object[] { Point.Create(new Vector2(2, 2)), Ellipse.Create(Vector2.Zero, Vector2.One, 0), false }
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