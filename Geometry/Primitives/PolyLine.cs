using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class PolyLine : Primitive
{
    #region Properties

    public readonly (Vector2 P1, Vector2 P2)[] LineSegments;

    #endregion

    #region IEquatable


    public override bool Equals(object other)
    {
        return other is PolyLine otherPolyline && Vertices.Equals(otherPolyline.Vertices);
    }

    #endregion

    public override string ToString()
    {
        return $"PolyLine {Vertices}";
    }

    #region Collision Detection

    public override Primitive GetBoundingPrimitive(float margin)
    {
        if (margin < Constants.Tolerance) return this;
        var boundingRectangle = Vertices.ToBoundingRectangle();
        var marginDelta = new Vector2(margin, margin);
        return Polygon.Rectangle.FromTwoPoints(boundingRectangle.TopLeft - marginDelta,
            boundingRectangle.BottomRight + marginDelta);
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
        return CollisionDetectorMethods.CollidesWith(this, p);
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

    private PolyLine(VertexArray vertices, BoundingRectangle boundingRectangle,
        (Vector2 P1, Vector2 P2)[] lineSegments) : base(vertices, boundingRectangle)
    {
        LineSegments = lineSegments;
    }

    public static PolyLine Create(params float[] points)
    {
        if (points.Length < 2) return Zero;

        var minX = float.MaxValue;
        var maxX = float.MinValue;
        var minY = float.MaxValue;
        var maxY = float.MinValue;

        var vertexCount = points.Length / 2;
        var vertices = new Vector2[vertexCount];
        var lineSegments = new (Vector2 P1, Vector2 P2)[vertexCount - 1];

        var vertexIndex = 0;
        var lastVertex = vertices[0];
        for (var i = 0; i < points.Length; i++)
        {
            var vertex = vertices[vertexIndex++] = new Vector2(points[i], points[++i]);

            if (vertexIndex > 1) lineSegments[vertexIndex - 2] = new ValueTuple<Vector2, Vector2>(lastVertex, vertex);

            lastVertex = vertex;

            if (vertex.X < minX) minX = vertex.X;
            if (vertex.X > maxX) maxX = vertex.X;

            if (vertex.Y < minY) minY = vertex.Y;
            if (vertex.Y > maxY) maxY = vertex.Y;
        }

        return new PolyLine(VertexArray.Create(vertices),
            new(new Vector2(minX, minY), new Vector2(maxX, maxY)),
            lineSegments);
    }

    public static readonly PolyLine Zero = Create(Vector2.Zero, Vector2.Zero);

    public static PolyLine Create(params Vector2[] points)
    {
        var minX = float.MaxValue;
        var maxX = float.MinValue;
        var minY = float.MaxValue;
        var maxY = float.MinValue;

        var lineSegments = new (Vector2 P1, Vector2 P2)[points.Length - 1];

        var lastVertex = points[0];
        for (var i = 0; i < points.Length; i++)
        {
            var vertex = points[i];

            if (i > 0) lineSegments[i - 1] = new ValueTuple<Vector2, Vector2>(lastVertex, vertex);

            lastVertex = vertex;

            if (vertex.X < minX) minX = vertex.X;
            if (vertex.X > maxX) maxX = vertex.X;

            if (vertex.Y < minY) minY = vertex.Y;
            if (vertex.Y > maxY) maxY = vertex.Y;
        }

        return new PolyLine(VertexArray.Create(points),
            new(new Vector2(minX, minY), new Vector2(maxX, maxY)),
            lineSegments);
    }

    #endregion

    #region Transformations

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void UpdateLineSegments()
    {
        var lastVertex = Vertices[0];
        for (var i = 0; i < Vertices.Length; i++)
        {
            var vertex = Vertices[i];
            if (i > 0) LineSegments[i - 1] = new ValueTuple<Vector2, Vector2>(lastVertex, vertex);

            lastVertex = vertex;
        }
    }

    public override PolyLine Translate(Vector2 delta)
    {
        for (var i = 0; i < LineSegments.Length; i++)
        {
            var lineSegment = LineSegments[i];
            LineSegments[i] = new ValueTuple<Vector2, Vector2>(lineSegment.P1 + delta, lineSegment.P2 + delta);
        }

        base.Translate(delta);
        UpdateLineSegments();
        return this;
    }

    public override PolyLine ScaleAboutCenter(Vector2 delta)
    {
        base.ScaleAboutCenter(delta);
        UpdateLineSegments();
        return this;
    }

    public override Primitive Scale(Vector2 scaleCenter, Vector2 delta)
    {
        base.Scale(scaleCenter, delta);
        UpdateLineSegments();
        return this;
    }

    public override PolyLine Rotate(float theta)
    {
        base.Rotate(theta);
        UpdateLineSegments();
        return this;
    }

    public override PolyLine Rotate(Vector2 rotationCenter, float theta)
    {
        base.Rotate(rotationCenter, theta);
        UpdateLineSegments();
        return this;
    }

    public override PolyLine Skew(Vector2 delta)
    {
        base.Skew(delta);
        UpdateLineSegments();
        return this;
    }

    #endregion
}