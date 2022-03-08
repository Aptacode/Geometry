using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Transformation.Scale;

public class EllipseCenterScaleTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Ellipse.Create(Vector2.Zero, 1), Vector2.One, Ellipse.Create(Vector2.Zero, 1) },
        new object[]
            { Ellipse.Create(Vector2.Zero, 1), new Vector2(2, 2), Ellipse.Create(Vector2.Zero, 2) }
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