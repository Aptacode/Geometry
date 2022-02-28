using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Transformation.Translate;

public class PolylineTranslationTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            PolyLine.Create(Vector2.Zero, Vector2.One), Vector2.One, PolyLine.Create(Vector2.One, new Vector2(2, 2))
        },
        new object[]
            { PolyLine.Create(Vector2.Zero, Vector2.One), Vector2.Zero, PolyLine.Create(Vector2.Zero, Vector2.One) }
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