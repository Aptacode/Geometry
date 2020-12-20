using System.Collections.Generic;
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

        public static VertexArray Aggregate(this IEnumerable<VertexArray> vertexArrays)
        {
            var vertexArray = new VertexArray();
            return vertexArrays.Aggregate(vertexArray, (current, v) => current.Concat(v));
        }

        public static VertexArray Remove(this VertexArray vertexArray, int index)
        {
            var vertices = vertexArray.ToList();
            vertices.RemoveAt(index);
            return VertexArray.Create(vertices.ToArray());
        }

        #region Transformation
        public static VertexArray Translate(this VertexArray vertexArray, Vector2 delta)
        {
            var translationMatrix = Matrix3x2.CreateTranslation(delta);
            var vertices = new Vector2[vertexArray.Length];
            for (var i = 0; i < vertexArray.Length; i++)
            {
                vertices[i] = Vector2.Transform(vertexArray[i], translationMatrix);
            }

            return VertexArray.Create(vertices);
        }

        public static VertexArray Rotate(this VertexArray vertexArray, Vector2 rotationCenter, float theta)
        {
            var rotationMatrix = Matrix3x2.CreateRotation(theta, rotationCenter);
            var vertices = new Vector2[vertexArray.Length];
            for (var i = 0; i < vertexArray.Length; i++)
            {
                vertices[i] = Vector2.Transform(vertexArray[i], rotationMatrix);
            }

            return VertexArray.Create(vertices);
        }

        public static VertexArray Scale(this VertexArray vertexArray, Vector2 scaleCenter, Vector2 delta)
        {
            var scaleMatrix = Matrix3x2.CreateScale(delta, scaleCenter);
            var vertices = new Vector2[vertexArray.Length];
            for (var i = 0; i < vertexArray.Length; i++)
            {
                vertices[i] = Vector2.Transform(vertexArray[i], scaleMatrix);
            }

            return VertexArray.Create(vertices);
        }

        public static VertexArray Skew(this VertexArray vertexArray, Vector2 delta)
        {
            var shearMatrix = new Matrix3x2(1, delta.Y, delta.X, 1, 0, 0); //Not 100% on this one.
            var vertices = new Vector2[vertexArray.Length];
            for (var i = 0; i < vertexArray.Length; i++)
            {
                vertices[i] = Vector2.Transform(vertexArray[i], shearMatrix);
            }

            return VertexArray.Create(vertices);
        }

        #endregion
    }
}