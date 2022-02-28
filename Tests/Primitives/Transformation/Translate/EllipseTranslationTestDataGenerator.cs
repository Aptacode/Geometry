using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Transformation.Translate;

public class EllipseTranslationTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { Ellipse.Circle.Create(Vector2.Zero, 1), Vector2.One, Ellipse.Circle.Create(Vector2.One, 1) },
        new object[] { Ellipse.Circle.Create(Vector2.Zero, 1), Vector2.Zero, Ellipse.Circle.Create(Vector2.Zero, 1) }
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