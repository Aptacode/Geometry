using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class Polygon : Primitive
{
    public override string ToString()
    {
        return $"Polygon {Vertices}";
    }

    #region IEquatable

    public override bool Equals(Primitive? other)
    {
        return other != null && other is Polygon otherPolygon && Vertices.Equals(otherPolygon.Vertices);
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    #endregion

    #region Properties

    private bool _updateEdges = true;
    private readonly (Vector2 P1, Vector2 P2)[] _edges;

    public (Vector2 P1, Vector2 P2)[] Edges
    {
        get
        {
            if (_updateEdges)
            {
                UpdateEdges();
            }

            return _edges;
        }
    }

    #endregion

    #region Collision Detection

    public override bool CollidesWith(Vector2 p)
    {
        return Vector2CollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(Point p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(p, this);
    }

    public override bool CollidesWith(Ellipse p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(PolyLine p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(p, this);
    }

    public override bool CollidesWith(BoundingRectangle p)
    {
        return p.CollidesWith(this);
    }

    public override bool CollidesWith(Polygon p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    #endregion

    #region Construction

    private Polygon(VertexArray vertices, BoundingRectangle boundingRectangle) : base(
        vertices, boundingRectangle)
    {
        _edges = new (Vector2 P1, Vector2 P2)[vertices.Length];
    }

    public static Polygon Create(params Vector2[] vertices)
    {
        var vertexArray = VertexArray.Create(vertices);
        var boundingRectangle = vertexArray.ToBoundingRectangle();
        return new Polygon(vertexArray, boundingRectangle);
    }

    public static Polygon Create(params float[] points)
    {
        var vertexArray = VertexArray.Create(points);
        var boundingRectangle = vertexArray.ToBoundingRectangle();
        return new Polygon(vertexArray, boundingRectangle);
    }

    public static class Rectangle
    {
        public static Polygon FromTwoPoints(Vector2 a, Vector2 b)
        {
            var minX = a.X;
            var maxX = b.X;
            var minY = a.Y;
            var maxY = b.Y;
            if (a.X > b.X)
            {
                minX = b.X;
                maxX = a.X;
            }

            if (a.Y > b.Y)
            {
                minY = b.Y;
                maxY = a.Y;
            }

            return Create(new Vector2(minX, minY), new Vector2(minX, maxY), new Vector2(maxX, maxY),
                new Vector2(maxX, minY));
        }
    }

    #endregion

    #region Transformations

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void UpdateEdges()
    {
        _updateEdges = false;

        var lastVertex = Vector2.Zero;
        for (var i = 0; i < Vertices.Length; i++)
        {
            var vertex = Vertices[i];
            if (i > 0)
            {
                _edges[i - 1] = new ValueTuple<Vector2, Vector2>(lastVertex, vertex);
            }

            lastVertex = vertex;
        }

        _edges[^1] = new ValueTuple<Vector2, Vector2>(lastVertex, Vertices[0]);
    }

    public override Polygon Translate(Vector2 delta)
    {
        base.Translate(delta);
        _updateEdges = true;
        return this;
    }

    public override Polygon ScaleAboutCenter(Vector2 delta)
    {
        base.ScaleAboutCenter(delta);
        _updateEdges = true;
        return this;
    }

    public override Primitive Scale(Vector2 scaleCenter, Vector2 delta)
    {
        base.Scale(scaleCenter, delta);
        _updateEdges = true;
        return this;
    }

    public override Polygon Rotate(float theta)
    {
        base.Rotate(theta);
        _updateEdges = true;
        return this;
    }

    public override Polygon Rotate(Vector2 rotationCenter, float theta)
    {
        base.Rotate(rotationCenter, theta);
        _updateEdges = true;
        return this;
    }

    public override Polygon Skew(Vector2 delta)
    {
        base.Skew(delta);
        _updateEdges = true;
        return this;
    }

    #endregion
}