using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Transformation.Translation;

public class PolygonTranslationTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            Polygon.Triangle.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), Vector2.One,
            Polygon.Triangle.Create(Vector2.One, new Vector2(2, 2), new Vector2(1, 2))
        },
        new object[]
        {
            Polygon.Triangle.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1)), Vector2.Zero,
            Polygon.Triangle.Create(Vector2.Zero, Vector2.One, new Vector2(0, 1))
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