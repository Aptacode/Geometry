using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PrimitiveCollisionTestData;

public class PolylinePrimitiveCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Ellipse PolyLine
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

        //Polyline PolyLine
        new object[]
            { PolyLine.Create(Vector2.Zero, Vector2.One), PolyLine.Create(Vector2.One, new Vector2(2, 2)), true },
        new object[]
        {
            PolyLine.Create(Vector2.Zero, new Vector2(10, 10)),
            PolyLine.Create(new Vector2(10, 0), new Vector2(0, 10)), true
        },
        new object[]
        {
            PolyLine.Create(Vector2.Zero, Vector2.One), PolyLine.Create(new Vector2(2, 2), new Vector2(3, 3)), false
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