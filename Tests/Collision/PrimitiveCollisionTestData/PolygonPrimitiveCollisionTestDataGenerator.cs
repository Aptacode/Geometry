using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PrimitiveCollisionTestData;

public class PolygonPrimitiveCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Circle Polygon
        new object[]
        {
            new Circle(Vector2.Zero, 1),
            Polygon.Rectangle.FromTwoPoints(new Vector2(2, 2), Vector2.One), false
        },

        //PolyLine Polygon
        new object[]
        {
            Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One),
            Polygon.Rectangle.FromTwoPoints(Vector2.One, new Vector2(2, 2)), true
        },

        //PolyLine Polygon
        new object[]
        {
            Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One),
            PolyLine.Create(Vector2.Zero, Vector2.One), true
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