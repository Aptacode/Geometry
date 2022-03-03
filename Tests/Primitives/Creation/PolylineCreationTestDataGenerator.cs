using System.Collections;
using System.Collections.Generic;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Primitives.Creation;

public class PolylineCreationTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { PolyLine.Zero, Vertices.VertexArray.Create(0,0, 0,0) },
        new object[] { PolyLine.Create(0,0, 1,1, 2,2), Vertices.VertexArray.Create(0,0, 1,1, 2,2) },
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