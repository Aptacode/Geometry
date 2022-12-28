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
        new object[] { new Point(0, 0), new Point(0, 0), true },
        new object[] { new Point(0, 0), new Point(1, 1), false },

        //Ellipse Point
        new object[] { new Point(new Vector2(2, 2)), new Circle(Vector2.Zero,1), false }
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