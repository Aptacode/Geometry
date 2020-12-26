using System;
using System.Numerics;

namespace Aptacode.Geometry.Vertices
{
    public readonly struct VertexArray : IEquatable<VertexArray>
    {
        public readonly Vector2[] Vertices;

        #region Construction

        public VertexArray(Vector2[] vertices)
        {
            Vertices = vertices;
        }

        public static VertexArray Create(Vector2[] vertices) => new(vertices);

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

        public Vector2 this[int key]
        {
            get => Vertices[key];
            set => Vertices[key] = value;
        }

        public int Length => Vertices.Length;

        #endregion

        #region IEquatable

        public override int GetHashCode() => HashCode.Combine(Vertices);

        public override bool Equals(object obj) => obj is VertexArray other && Equals(other);

        public bool Equals(VertexArray other) => this == other;

        public static bool operator ==(VertexArray lhs, VertexArray rhs)
        {
            if (lhs.Length != rhs.Length)
            {
                return false;
            }

            for (var i = 0; i < lhs.Length; i++)
            {
                var delta = lhs[i] - rhs[i];
                if (Math.Abs(delta.X + delta.Y) > Constants.Tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(VertexArray lhs, VertexArray rhs) => !(lhs == rhs);

        #endregion
    }
}