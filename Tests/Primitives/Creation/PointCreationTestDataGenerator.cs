//using System.Collections;
//using System.Collections.Generic;
//using System.Numerics;
//using Aptacode.Geometry.Primitives;

//namespace Aptacode.Geometry.Tests.Primitives.Creation;

//public class PointCreationTestDataGenerator : IEnumerable<object[]>
//{
//    private readonly List<object[]> _data = new()
//    {
//        new object[] { Point.Zero, Vertices.VertexArray.Create(0, 0) },
//        new object[] { Point.Unit, Vertices.VertexArray.Create(1, 1) },
//        new object[] { Point.Create(0, 0), Vertices.VertexArray.Create(0, 0) },
//        new object[] { Point.Create(new Vector2(0, 0)), Vertices.VertexArray.Create(0, 0) }
//    };

//    public IEnumerator<object[]> GetEnumerator()
//    {
//        return _data.GetEnumerator();
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }
//}