using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;

namespace Aptacode.Geometry.Vertices
{
    public static class VertexArrayExtensions
    {
        public static VertexArray Concat(this VertexArray vertexArray, params Vector2[] vertices)
        {
            var newVertices = new Vector2[vertexArray.Length + vertices.Length];
            var count = 0;
            for (var i = 0; i < vertexArray.Length; i++ )
            {
                newVertices[count++] = vertexArray[i];
            }
            for (var i = 0; i < vertices.Length; i++)
            {
                newVertices[count++] = vertices[i];
            }

            return new VertexArray(newVertices);
        }

        public static VertexArray Concat(this VertexArray vertexArrayA, VertexArray vertexArrayB)
        {
            var newVertices = new Vector2[vertexArrayA.Length + vertexArrayB.Length];
            var count = 0;
            for (var i = 0; i < vertexArrayA.Length; i++)
            {
                newVertices[count++] = vertexArrayA[i];
            }
            for (var i = 0; i < vertexArrayB.Length; i++)
            {
                newVertices[count++] = vertexArrayB[i];
            }

            return new VertexArray(newVertices);
        }

        public static VertexArray Aggregate(this IEnumerable<VertexArray> vertexArrays)
        {
            var totalVertices = vertexArrays.Aggregate(0, (current, v) => current += v.Length);
            var newVertices = new Vector2[totalVertices];
            var count = 0;
            foreach (var array in vertexArrays)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    newVertices[count++] = array[i];
                }
            }

            return new VertexArray(newVertices);
        }

        public static VertexArray Remove(this VertexArray vertexArray, int index)
        {
            var vertices = vertexArray.Vertices.ToList();
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