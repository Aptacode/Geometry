using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.CollisionTests;

public class PolygonPrimitiveCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Ellipse Polygon
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
        },

        //PolyLine Polygon
        new object[]
        {
            Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One),
            Polygon.Rectangle.FromPositionAndSize(Vector2.One, Vector2.One), true
        },
        new object[]
        {
            Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One),
            Polygon.Rectangle.FromPositionAndSize(new Vector2(2, 2), Vector2.One), false
        },
        new object[]
        {
            Polygon.Rectangle.FromPositionAndSize(Vector2.Zero, Vector2.One),
            Polygon.Rectangle.FromPositionAndSize(new Vector2(3, 3), Vector2.One), false
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