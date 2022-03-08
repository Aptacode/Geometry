using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Aptacode.Geometry.Tests.VertexArray;

public class VertexArrayTranslateTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Concat
        //v1.Translate(delta) == expected
        new object[]
            { Vertices.VertexArray.Create(Vector2.Zero), Vector2.Zero, Vertices.VertexArray.Create(Vector2.Zero) },
        new object[]
        {
            Vertices.VertexArray.Create(Vector2.Zero, Vector2.One), Vector2.One,
            Vertices.VertexArray.Create(Vector2.One, new Vector2(2, 2))
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