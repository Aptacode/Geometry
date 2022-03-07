using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PrimitiveCollisionTestData;

public class EllipsePrimitiveCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Ellipse Ellipse
        new object[] { Ellipse.Create(8, 5, 3, 2, 0), Ellipse.Create(5, 5, 3, 2, 0), true }, //ellipse intersection
        new object[]
            { Ellipse.Create(5, 5, 1.5f, 1, 0), Ellipse.Create(5, 5, 3, 2, 0), true }, //ellipse containment
        new object[]
            { Ellipse.Create(10, 10, 10, 10, 0), Ellipse.Create(20, 20, 10, 10, 0), true }, //circle intersection
        new object[]
            { Ellipse.Create(10, 10, 10, 10, 0), Ellipse.Create(10, 10, 5, 5, 0), true }, //circle containment
        new object[]
        {
            Ellipse.Create(Vector2.Zero, Vector2.One, 0), Ellipse.Create(new Vector2(2, 2), Vector2.One, 0), false
        },
        new object[]
        {
            Ellipse.Create(5, 5, 3, 2, (float)Math.PI / 4f), Point.Create(7, 7), true
        },
        new object[] { Ellipse.Create(Vector2.Zero, Vector2.One, 0), Point.Create(new Vector2(2, 2)), false },
        new object[]
        {
            Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f),
            PolyLine.Create(new Vector2(4, 5), new Vector2(6, 5)), true
        },
        new object[]
        {
            Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f),
            PolyLine.Create(new Vector2(3, 3), new Vector2(7, 7)), true
        },
        new object[]
        {
            Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), 0.0f),
            PolyLine.Create(new Vector2(3, 3), new Vector2(7, 7)), true
        },
        new object[]
        {
            Ellipse.Create(Vector2.Zero, Vector2.One, 0.0f), PolyLine.Create(new Vector2(2, 2), new Vector2(3, 3)),
            false
        },

        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(10, 0), new Vector2(10, 20)), true },
        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(9, 0), new Vector2(9, 20)), true },
        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(11, 0), new Vector2(11, 20)), true },
        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(8, 0), new Vector2(8, 20)), false },
        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(12, 0), new Vector2(12, 20)), false },
        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 10), new Vector2(20, 10)), true },
        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 9), new Vector2(20, 9)), true },
        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 11), new Vector2(20, 11)), true },
        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 8), new Vector2(20, 8)), false },
        new object[]
            { Ellipse.Create(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 12), new Vector2(20, 12)), false },
        new object[]
        {
            Ellipse.Create(30, 30, 20, 10, 0.0f), Polygon.Create(27, 27, 33, 27, 33, 33, 27, 33), true
        },
        new object[]
        {
            Ellipse.Create(new Vector2(5, 5), new Vector2(3, 2), (float)Math.PI / 4f),
            Polygon.Create(new Vector2(3, 3), new Vector2(5, 7), new Vector2(7, 3)), true
        },
        new object[]
        {
            Ellipse.Create(Vector2.Zero, Vector2.One, 0.0f),
            Polygon.Rectangle.FromTwoPoints(new Vector2(2, 2), Vector2.One), false
        }
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