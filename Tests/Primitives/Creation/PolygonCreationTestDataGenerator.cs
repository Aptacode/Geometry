//using System.Collections;
//using System.Collections.Generic;
//using System.Numerics;
//using Aptacode.Geometry.Primitives;

//namespace Aptacode.Geometry.Tests.Primitives.Creation;

//public class PolygonCreationTestDataGenerator : IEnumerable<object[]>
//{
//    private readonly List<object[]> _data = new()
//    {
//        new object[]
//        {
//            Polygon.Rectangle.FromTwoPoints(Vector2.Zero, Vector2.One),
//            Vertices.VertexArray.Create(0, 0, 0, 1, 1, 1, 1, 0)
//        },
//        new object[]
//        {
//            Polygon.Create(Vector2.Zero, Vector2.One, new Vector2(1, 0)), Vertices.VertexArray.Create(0, 0, 1, 1, 1, 0)
//        }
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