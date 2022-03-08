using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Transformation.Scale;

public class PolylineCenterScaleTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            PolyLine.Create(Vector2.Zero, Vector2.One),
            Vector2.One,
            PolyLine.Create(Vector2.Zero, Vector2.One)
        },
        new object[]
        {
            PolyLine.Create(Vector2.Zero, Vector2.One),
            new Vector2(2),
            PolyLine.Create(new Vector2(-0.5f, -0.5f), new Vector2(1.5f, 1.5f))
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