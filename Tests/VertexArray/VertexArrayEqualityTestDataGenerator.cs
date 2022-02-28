using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Aptacode.Geometry.Tests.VertexArray;

public class VertexArrayEqualityTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //v1, v2, isEqual

        //Equal
        new object[] { Vertices.VertexArray.Create(Vector2.Zero), Vertices.VertexArray.Create(Vector2.Zero), true },
        new object[]
        {
            Vertices.VertexArray.Create(Vector2.Zero, Vector2.One),
            Vertices.VertexArray.Create(Vector2.Zero, Vector2.One), true
        },

        //Not Equal
        new object[] { Vertices.VertexArray.Create(Vector2.Zero), Vertices.VertexArray.Create(Vector2.One), false },
        new object[]
            { Vertices.VertexArray.Create(Vector2.Zero, Vector2.One), Vertices.VertexArray.Create(Vector2.Zero), false }
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