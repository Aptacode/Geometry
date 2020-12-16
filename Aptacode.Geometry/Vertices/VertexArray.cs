using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Aptacode.Geometry.Vertices
{
    public readonly struct VertexArray : IEquatable<VertexArray>, IEnumerable<Vector2>
    {
        public readonly Vector2[] Vertices;

        #region Construction

        public VertexArray(Vector2[] vertices)
        {
            Vertices = vertices;
        }

        public static VertexArray Create(IEnumerable<Vector2> vertices) => new(vertices.ToArray());
        public static VertexArray Create(params Vector2[] vertices) => new(vertices);

        public static VertexArray Create(Vector2 p1, params Vector2[] vertices)
        {
            var length = vertices.Length + 1;

            var Vertices = new Vector2[length];
            Vertices[0] = p1;
            for (var i = 1; i < length; i++)
            {
                Vertices[i] = vertices[i - 1];
            }

            return new VertexArray(Vertices);
        }

        public static VertexArray Create(Vector2 p1, Vector2 p2, params Vector2[] vertices)
        {
            var length = vertices.Length + 2;

            var Vertices = new Vector2[length];
            Vertices[0] = p1;
            Vertices[1] = p2;
            for (var i = 2; i < length; i++)
            {
                Vertices[i] = vertices[i - 2];
            }

            return new VertexArray(Vertices);
        }

        public static VertexArray Create(Vector2 p1, Vector2 p2, Vector2 p3, params Vector2[] vertices)
        {
            var length = vertices.Length + 3;

            var Vertices = new Vector2[length];
            Vertices[0] = p1;
            Vertices[1] = p2;
            Vertices[2] = p3;
            for (var i = 3; i < length; i++)
            {
                Vertices[i] = vertices[i - 3];
            }

            return new VertexArray(Vertices);
        }

        #endregion

        #region List

        public Vector2 this[int key] => Vertices[key];
        public IEnumerator<Vector2> GetEnumerator() => Vertices.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Vertices.GetEnumerator();
        public int Length => Vertices.Length;

        #endregion

        #region IEquatable

        public override int GetHashCode() => HashCode.Combine(Vertices);

        public override bool Equals(object obj) => obj is VertexArray other && Equals(other);

        public bool Equals(VertexArray other) => this == other;

        public static bool operator ==(VertexArray lhs, VertexArray rhs)
        {
            return lhs.Vertices.Length == rhs.Vertices.Length &&
                   lhs.Vertices.Select((t, i) => t - rhs.Vertices[i])
                       .All(difference =>
                           !(Math.Abs(difference.X) > Constants.Tolerance) &&
                           !(Math.Abs(difference.Y) > Constants.Tolerance));
        }

        public static bool operator !=(VertexArray lhs, VertexArray rhs) => !(lhs == rhs);

        #endregion
    }
}