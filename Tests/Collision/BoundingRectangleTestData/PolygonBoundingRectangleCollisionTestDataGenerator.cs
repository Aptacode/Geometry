using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.BoundingRectangleTestData;

public class PolygonBoundingRectangleCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One),
            BoundingRectangle.FromTwoPoints(Vector2.Zero, Vector2.One), true
        },
        new object[]
        {
            Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One),
            BoundingRectangle.FromTwoPoints(new Vector2(2, 2), new Vector2(3, 3)), false
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