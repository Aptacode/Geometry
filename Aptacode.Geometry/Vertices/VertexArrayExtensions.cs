using System.Linq;
using System.Numerics;

namespace Aptacode.Geometry.Vertices
{
    public static class VertexArrayExtensions
    {
        public static VertexArray Concat(this VertexArray vertexArray, params Vector2[] vertices) =>
            new(vertexArray.Concat(vertices).ToArray());

        public static VertexArray Concat(this VertexArray vertexArrayA, VertexArray vertexArrayB) =>
            new(vertexArrayA.Concat(vertexArrayB).ToArray());

        public static VertexArray Remove(this VertexArray vertexArray, int index)
        {
            var vertices = vertexArray.ToList();
            vertices.RemoveAt(index);
            return VertexArray.Create(vertices);
        }

        #region Translation

        public static VertexArray Translate(this VertexArray vertexArray, Vector2 delta)
        {
            return VertexArray.Create(vertexArray.Select(v => v + delta));
        }

        public static VertexArray Rotate(this VertexArray vertexArray, Vector2 rotationCenter, float theta)
        {
            return VertexArray.Create(vertexArray.Select(v => Vector2.Transform(v, Matrix3x2.CreateRotation(theta, rotationCenter))));
        }

        public static VertexArray Scale(this VertexArray vertexArray, Vector2 delta) =>
            //ToDo
            vertexArray;

        public static VertexArray Skew(this VertexArray vertexArray, Vector2 delta) =>
            //ToDo
            vertexArray;

        #endregion
    }
}