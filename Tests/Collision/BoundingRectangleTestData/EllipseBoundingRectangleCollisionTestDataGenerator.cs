using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.BoundingRectangleTestData;

public class EllipseBoundingRectangleCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Ellipse
        new object[]
        {
            Ellipse.Create(Vector2.Zero, 1), new BoundingRectangle(Vector2.Zero, Vector2.One), true
        },
        new object[]
        {
            Ellipse.Create(Vector2.Zero, 1), new BoundingRectangle(new Vector2(0, 1), Vector2.One),
            true
        },
        new object[]
        {
            Ellipse.Create(Vector2.Zero, 1), new BoundingRectangle(new Vector2(3, 3), Vector2.One),
            false
        },
   
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