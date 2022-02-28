using System.Collections;
using System.Collections.Generic;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Equality;

public class EllipseEqualityTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Ellipse.Create(0, 0, 1, 1, 0), Ellipse.Create(0, 0, 1, 1, 0), true },
        new object[] { Ellipse.Create(0, 0, 1, 1, 0), Ellipse.Create(1, 1, 1, 1, 0), false },
        new object[] { Ellipse.Create(0, 0, 1, 1, 0), Ellipse.Create(0, 0, 2, 2, 0), false },
        new object[] { Ellipse.Create(0, 0, 1, 2, 0), Ellipse.Create(0, 0, 1, 2, 1), false },
        new object[] { Ellipse.Create(0, 0, 1, 2, 0), null, false },
        new object[] { null, Ellipse.Create(0, 0, 1, 2, 0), false },
        new object[] { null, null, true },

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