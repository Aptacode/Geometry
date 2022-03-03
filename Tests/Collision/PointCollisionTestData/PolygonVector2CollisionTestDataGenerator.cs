using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PointCollisionTestData;

public class PolygonVector2CollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Polygon
        new object[] { Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One), Vector2.Zero, true },
        new object[] { Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One), new Vector2(2, 2), false },
        new object[] { Polygon.Rectangle.FromTwoPoints(new Vector2(10,10), new Vector2(20,20)), Vector2.Zero, false },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), Vector2.Zero, false },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), new Vector2(14, 15), false },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), new Vector2(15, 15), true },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), new Vector2(16, 15), true },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), new Vector2(19, 15), true },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), new Vector2(20, 15), true },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), new Vector2(21, 15), false },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), new Vector2(15, 9), false },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), new Vector2(15, 10), true },
        new object[] { Polygon.Create(new Vector2(10,10), new Vector2(20,20), new Vector2(20, 10)), new Vector2(15, 11), true },
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