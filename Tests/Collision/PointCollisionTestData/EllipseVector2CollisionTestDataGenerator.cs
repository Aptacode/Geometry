using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PointCollisionTestData;

public class EllipseVector2CollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Ellipse
        new object[] { Ellipse.Create(0, 0, 10, 10, 0), Vector2.One, true },
        new object[] { Ellipse.Create(0, 0, 10, 10, 0), new Vector2(20, 20), false },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(0, 0), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(0, 1), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(1, 1), false },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(0, 1), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(1, -1), false },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(0, -1), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(-1, -1), false },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(-1, 0), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(-1, 1), false }
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