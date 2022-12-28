using System.Collections;
using System.Collections.Generic;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Equality;

public class EllipseEqualityTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { new Circle(0, 0, 1), new Circle(0, 0, 1), true },
        new object[] { new Circle(0, 0, 1), new Circle(1, 1, 1), false },
        new object[] { new Circle(0, 0, 1), new Circle(0, 0, 2), false },
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