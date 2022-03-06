using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Aptacode.Geometry.Vertices;

public readonly struct VertexArray : IEquatable<VertexArray>
{
    public readonly Vector2[] Vertices;

    #region Construction

    public VertexArray(Vector2[] vertices)
    {
        Vertices = vertices ?? Array.Empty<Vector2>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VertexArray Empty()
    {
        return new VertexArray(Array.Empty<Vector2>());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VertexArray Create(params Vector2[] vertices)
    {
        return new VertexArray(vertices);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VertexArray Create(params float[] vertices)
    {
        switch (vertices.Length)
        {
            case 0:
                return new VertexArray(Array.Empty<Vector2>());
            case 1:
                return new VertexArray(new[] { new Vector2(vertices[0], 0) });
        }

        var vertexArray = new Vector2[vertices.Length / 2];
        var vertexIndex = 0;
        for (var i = 0; i < vertices.Length; i += 2)
            vertexArray[vertexIndex++] = new Vector2(vertices[i], vertices[i + 1]);
        return new VertexArray(vertexArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VertexArray Create(IEnumerable<Vector2> vertices)
    {
        return new VertexArray(vertices.ToArray());
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

    public override int GetHashCode()
    {
        return HashCode.Combine(Vertices);
    }

    public override bool Equals(object obj)
    {
        return obj is VertexArray other && Equals(other);
    }

    public bool Equals(VertexArray other)
    {
        return this == other;
    }

    public static bool operator ==(VertexArray lhs, VertexArray rhs)
    {
        if (lhs.Length != rhs.Length) return false;

        for (var i = 0; i < lhs.Length; i++)
        {
            var delta = lhs[i] - rhs[i];
            if (Math.Abs(delta.X) + Math.Abs(delta.Y) > Constants.Tolerance) return false;
        }

        return true;
    }

    public static bool operator !=(VertexArray lhs, VertexArray rhs)
    {
        return !(lhs == rhs);
    }

    #endregion

    public override string ToString()
    {
        return string.Join(", ", Vertices.Select(v => $"({v.X},{v.Y})"));
    }
}