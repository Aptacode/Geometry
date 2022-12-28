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
        new object[]
            { new Circle(10, 10, 10), new Circle(20, 20, 10), true }, //circle intersection
        new object[]
            { new Circle(10, 10, 10), new Circle(10, 10, 5), true }, //circle containment
        new object[]
        {
            new Circle(Vector2.Zero, 1), new Circle(new Vector2(2, 2), 1), false
        },
        new object[] { new Circle(Vector2.Zero, 1), new Point(new Vector2(2, 2)), false },
        new object[]
        {
            new Circle(Vector2.Zero, 1), PolyLine.Create(new Vector2(2, 2), new Vector2(3, 3)),
            false
        },

        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(10, 0), new Vector2(10, 20)), true },
        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(9, 0), new Vector2(9, 20)), true },
        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(11, 0), new Vector2(11, 20)), true },
        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(8, 0), new Vector2(8, 20)), false },
        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(12, 0), new Vector2(12, 20)), false },
        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 10), new Vector2(20, 10)), true },
        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 9), new Vector2(20, 9)), true },
        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 11), new Vector2(20, 11)), true },
        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 8), new Vector2(20, 8)), false },
        new object[]
            { new Circle(new Vector2(10), 1), PolyLine.Create(new Vector2(0, 12), new Vector2(20, 12)), false },
        new object[]
        {
            new Circle(Vector2.Zero, 1),
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