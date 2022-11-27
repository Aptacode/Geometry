using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.EllipseStandardForm;

public class EllipseStandardFormTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Ellipse.Create(Vector2.Zero, 1), 1, 0, 1, 0, 0, -1 },
        new object[] { Ellipse.Create(new Vector2(3, 0), 1), 1, 0, 1, -6, 0, 8 },
        new object[] { Ellipse.Create(0, 0, 2, 1, 0), 1, 0, 4, 0, 0, -4 },
        new object[] { Ellipse.Create(0, 0, 1, 2, MathF.PI / 2f), 1, 0, 4, 0, 0, -4 }
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