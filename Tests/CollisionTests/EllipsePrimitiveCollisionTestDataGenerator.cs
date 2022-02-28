using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.CollisionTests;

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
            Polygon.Rectangle.FromPositionAndSize(new Vector2(2, 2), Vector2.One), false
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