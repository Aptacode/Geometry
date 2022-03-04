using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Transformation.Translate;

public class PolygonTranslationTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One), Vector2.One,
            Polygon.Rectangle.FromTwoPoints(Vector2.One, new Vector2(2, 2))
        },
        new object[]
        {
            Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One), Vector2.Zero,
            Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One)
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