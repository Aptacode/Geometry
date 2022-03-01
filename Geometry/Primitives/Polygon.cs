using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class Polygon : Primitive
{
    #region Properties

    public readonly (Vector2 P1, Vector2 P2)[] Edges;

    #endregion

    #region IEquatable

    public override bool Equals(object other)
    {
        return other is Polygon otherPolygon && Vertices.Equals(otherPolygon.Vertices);
    }

    #endregion

    public override string ToString()
    {
        return $"Polygon {Vertices}";
    }

    #region Collision Detection

    public override Primitive GetBoundingPrimitive(float margin)
    {
        var deltaX = (margin + BoundingRectangle.Size.X) / BoundingRectangle.Size.X;
        var deltaY = (margin + BoundingRectangle.Size.Y) / BoundingRectangle.Size.Y;
        var scaleFactor = new Vector2(deltaX, deltaY);
        Vertices.Scale(BoundingRectangle.Center, scaleFactor);

        //Todo scale polygon instead of creating convex hull
        return Create(Vertices.Vertices);
    }

    public override bool CollidesWith(Vector2 p)
    {
        return Vector2CollisionDetector.CollidesWith(this, p);
    }

    public override bool CollidesWith(Point p)
    {
        return CollisionDetectorMethods.CollidesWith(p, this);
    }

    public override bool CollidesWith(Ellipse p)
    {
        return CollisionDetectorMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(PolyLine p)
    {
        return CollisionDetectorMethods.CollidesWith(p, this);
    }

    public override bool CollidesWith(BoundingRectangle p)
    {
        return p.CollidesWith(this);
    }

    public override bool CollidesWith(Polygon p)
    {
        return CollisionDetectorMethods.CollidesWith(this, p);
    }

    #endregion

    #region Construction

    private Polygon(VertexArray vertices, BoundingRectangle boundingRectangle, (Vector2 P1, Vector2 P2)[] edges) :
        base(vertices, boundingRectangle)
    {
        Edges = edges;
    }

    public static Polygon Create(params Vector2[] vertices)
    {
        var minX = float.MaxValue;
        var maxX = float.MinValue;
        var minY = float.MaxValue;
        var maxY = float.MinValue;

        var edges = new (Vector2 P1, Vector2 P2)[vertices.Length];
        var lastVertex = Vector2.Zero;
        for (var i = 0; i < vertices.Length; i++)
        {
            var vertex = vertices[i];

            if (i > 0) edges[i - 1] = new ValueTuple<Vector2, Vector2>(lastVertex, vertex);

            lastVertex = vertex;

            if (vertex.X < minX) minX = vertex.X;
            if (vertex.X > maxX) maxX = vertex.X;

            if (vertex.Y < minY) minY = vertex.Y;
            if (vertex.Y > maxY) maxY = vertex.Y;
        }

        edges[^1] = new ValueTuple<Vector2, Vector2>(lastVertex, vertices[0]);

        return new Polygon(VertexArray.Create(vertices),
            new BoundingRectangle(new Vector2(minX, minY), new Vector2(maxX, maxY)),
            edges);
    }

    public static Polygon Create(params float[] points)
    {
        var minX = float.MaxValue;
        var maxX = float.MinValue;
        var minY = float.MaxValue;
        var maxY = float.MinValue;

        var vertexArray = new Vector2[points.Length / 2];
        var edges = new (Vector2 P1, Vector2 P2)[vertexArray.Length];
        var lastVertex = Vector2.Zero;

        var vertexIndex = 0;
        for (var i = 0; i < points.Length; i++)
        {
            var vertex = vertexArray[vertexIndex++] = new Vector2(points[i], points[++i]);

            if (vertexIndex > 1) edges[vertexIndex - 2] = new ValueTuple<Vector2, Vector2>(lastVertex, vertex);

            lastVertex = vertex;

            if (vertex.X < minX) minX = vertex.X;
            if (vertex.X > maxX) maxX = vertex.X;

            if (vertex.Y < minY) minY = vertex.Y;
            if (vertex.Y > maxY) maxY = vertex.Y;
        }

        edges[^1] = new ValueTuple<Vector2, Vector2>(lastVertex, vertexArray[0]);

        return new Polygon(VertexArray.Create(vertexArray),
            new BoundingRectangle(new Vector2(minX, minY), new Vector2(maxX, maxY)),
            edges);
    }

    public static class Rectangle
    {
        public static Polygon Create(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft)
        {
            return Polygon.Create(topLeft, topRight, bottomRight, bottomLeft);
        }

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

            return Polygon.Create(new Vector2(minX, maxY), new Vector2(maxX, maxY), new Vector2(maxX, minY),
                new Vector2(minX, minY));
        }
    }

    public static class Triangle
    {
        public static Polygon Create(Vector2 a, Vector2 b, Vector2 c)
        {
            return Polygon.Create(a, b, c);
        }
    }

    #endregion

    #region Transformations

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void UpdateEdges()
    {
        var lastVertex = Vector2.Zero;
        for (var i = 0; i < Vertices.Length; i++)
        {
            var vertex = Vertices[i];
            if (i > 0) Edges[i - 1] = new ValueTuple<Vector2, Vector2>(lastVertex, vertex);

            lastVertex = vertex;
        }

        Edges[^1] = new ValueTuple<Vector2, Vector2>(lastVertex, Vertices[0]);
    }

    public override Polygon Translate(Vector2 delta)
    {
        base.Translate(delta);
        UpdateEdges();
        return this;
    }

    public override Polygon ScaleAboutCenter(Vector2 delta)
    {
        base.ScaleAboutCenter(delta);
        UpdateEdges();
        return this;
    }

    public override Primitive Scale(Vector2 scaleCenter, Vector2 delta)
    {
        base.Scale(scaleCenter, delta);
        UpdateEdges();
        return this;
    }

    public override Polygon Rotate(float theta)
    {
        base.Rotate(theta);
        UpdateEdges();
        return this;
    }

    public override Polygon Rotate(Vector2 rotationCenter, float theta)
    {
        base.Rotate(rotationCenter, theta);
        UpdateEdges();
        return this;
    }

    public override Polygon Skew(Vector2 delta)
    {
        base.Skew(delta);
        UpdateEdges();
        return this;
    }

    #endregion
}