using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PointCollisionTestData;

public class PolygonPointCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Point
        new object[] { Point.Create(0, 0), Vector2.Zero, true },
        new object[] { Point.Create(0, 0), Vector2.One, false },

        //Ellipse
        new object[] { Ellipse.Create(0, 0, 10, 10, 0), Vector2.One, true },
        new object[] { Ellipse.Create(0, 0, 10, 10, 0), new Vector2(20, 20), false },

        //PolyLine
        new object[] { PolyLine.Create(0, 0, 10, 10), Vector2.Zero, true },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(5, 5), true },
        new object[] { PolyLine.Create(0, 0, 10, 10), new Vector2(11, 11), false },

        //Polygon
        new object[] { Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One), Vector2.Zero, true },
        new object[] { Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One), new Vector2(2, 2), false }
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