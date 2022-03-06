using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class PolyLine : Primitive
{
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

    #region Properties

    private bool _updateLineSegments = true;
    private readonly (Vector2 P1, Vector2 P2)[] _lineSegments;

    public (Vector2 P1, Vector2 P2)[] LineSegments
    {
        get
        {
            if (_updateLineSegments) UpdateLineSegments();

            return _lineSegments;
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
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
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

    private PolyLine(VertexArray vertices, BoundingRectangle boundingRectangle) : base(vertices, boundingRectangle)
    {
        _lineSegments = new (Vector2 P1, Vector2 P2)[vertices.Length];
    }

    public static PolyLine Create(params float[] points)
    {
        if (points.Length < 2) return Zero;

        var vertexArray = VertexArray.Create(points);
        return new PolyLine(vertexArray, vertexArray.ToBoundingRectangle());
    }

    public static readonly PolyLine Zero = Create(Vector2.Zero, Vector2.Zero);

    public static PolyLine Create(params Vector2[] points)
    {
        var vertexArray = VertexArray.Create(points);
        return new PolyLine(vertexArray, vertexArray.ToBoundingRectangle());
    }

    #endregion

    #region Transformations

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void UpdateLineSegments()
    {
        _updateLineSegments = false;

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
        base.Translate(delta);
        _updateLineSegments = true;
        return this;
    }

    public override PolyLine ScaleAboutCenter(Vector2 delta)
    {
        base.ScaleAboutCenter(delta);
        _updateLineSegments = true;
        return this;
    }

    public override Primitive Scale(Vector2 scaleCenter, Vector2 delta)
    {
        base.Scale(scaleCenter, delta);
        _updateLineSegments = true;
        return this;
    }

    public override PolyLine Rotate(float theta)
    {
        base.Rotate(theta);
        _updateLineSegments = true;
        return this;
    }

    public override PolyLine Rotate(Vector2 rotationCenter, float theta)
    {
        base.Rotate(rotationCenter, theta);
        _updateLineSegments = true;
        return this;
    }

    public override PolyLine Skew(Vector2 delta)
    {
        base.Skew(delta);
        _updateLineSegments = true;
        return this;
    }

    #endregion
}