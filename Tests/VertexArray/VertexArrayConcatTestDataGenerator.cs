using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Aptacode.Geometry.Tests.VertexArray;

public class VertexArrayConcatTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Concat
        //v1.Concat(v2) == v3
        new object[]
        {
            Vertices.VertexArray.Create(Vector2.Zero), Vertices.VertexArray.Create(Vector2.Zero),
            Vertices.VertexArray.Create(Vector2.Zero)
        },
        new object[]
        {
            Vertices.VertexArray.Create(Vector2.Zero), Vertices.VertexArray.Create(Vector2.One),
            Vertices.VertexArray.Create(Vector2.Zero, Vector2.One)
        },
        new object[]
        {
            Vertices.VertexArray.Create(Vector2.Zero), Vertices.VertexArray.Create(),
            Vertices.VertexArray.Create(Vector2.Zero)
        },
        new object[] { Vertices.VertexArray.Create(), Vertices.VertexArray.Create(), Vertices.VertexArray.Create() }
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