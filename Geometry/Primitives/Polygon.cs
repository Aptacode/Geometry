using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class Polygon : Primitive
{
    public override string ToString()
    {
        return $"Polygon {Vertices.Print()}";
    }

    public Vector2[] Vertices { get; set; }

    #region IEquatable
    public override Polygon Transform(Matrix3x2 matrix)
    {
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i] = Vector2.Transform(Vertices[i], matrix);
        }
        return this;
    }

    public override Polygon Copy()
    {
        var copy = new Vector2[Vertices.Length];
        Vertices.CopyTo(copy, 0);
        return new Polygon(copy);
    }
    public void CopyTo(Polygon destination)
    {
        Vertices.CopyTo(destination.Vertices, 0);
        destination._updateEdges = true;
    }
    public void CopyAndTransformTo(Polygon destination, Matrix3x2 matrix)
    {
        for(int i = 0; i < Vertices.Length; i++)
        {
            destination.Vertices[i] = Vector2.Transform(destination.Vertices[i], matrix);
        }

        destination._updateEdges = true;
    }

    public override bool AreEqual(Primitive other)
    {
        return other != null && other is Polygon otherPolyline && Vertices.AreEqual(otherPolyline.Vertices);
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

    public override Vector2 GetCentroid()
    {
        float accumulatedArea = 0.0f;
        float centerX = 0.0f;
        float centerY = 0.0f;

        Span<Vector2> verticesAsSpan = Vertices;
        for (int i = 0, j = verticesAsSpan.Length - 1; i< verticesAsSpan.Length; j = i++)
        {
            float temp = verticesAsSpan[i].X * verticesAsSpan[j].Y - verticesAsSpan[j].X * verticesAsSpan[i].Y;
            accumulatedArea += temp;
            centerX += (verticesAsSpan[i].X + verticesAsSpan[j].X) * temp;
            centerY += (verticesAsSpan[i].Y + verticesAsSpan[j].Y) * temp;
        }

        if (Math.Abs(accumulatedArea) < 1E-7f)
            return Vector2.Zero;  // Avoid division by zero

        accumulatedArea *= 3f;
        return new Vector2(centerX / accumulatedArea, centerY / accumulatedArea);
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

    public override bool CollidesWith(Circle p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(PolyLine p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(p, this);
    }

    public override bool CollidesWith(Polygon p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    #endregion

    #region Construction

    public Polygon(params Vector2[] vertices)
    {
        Vertices = vertices;
        _edges = new (Vector2 P1, Vector2 P2)[vertices.Length];
    }    
    
    public Polygon(params float[] vertices)
    {
        Vertices = vertices.FromPoints();
        _edges = new (Vector2 P1, Vector2 P2)[vertices.Length];
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

            return new Polygon(new Vector2[] { new Vector2(minX, minY), new Vector2(minX, maxY), new Vector2(maxX, maxY),
                new Vector2(maxX, minY) });
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
        Vertices.Translate(delta);
        _updateEdges = true;
        return this;
    }

    public override Polygon Rotate(float theta)
    {
        var center = GetCentroid();
        Vertices.Rotate(center, theta);
        _updateEdges = true;
        return this;
    }

    public override Polygon Rotate(Vector2 rotationCenter, float theta)
    {
        Vertices.Rotate(rotationCenter, theta);
        _updateEdges = true;
        return this;
    }

    public override Polygon ScaleAboutCenter(Vector2 delta)
    {
        var center = GetCentroid();
        Vertices.Scale(center, delta);
        _updateEdges = true;
        return this;
    }

    public override Polygon Scale(Vector2 scaleCenter, Vector2 delta)
    {
        Vertices.Scale(scaleCenter, delta);
        _updateEdges = true;
        return this;
    }

    public override Polygon Skew(Vector2 delta)
    {
        Vertices.Skew(delta);
        _updateEdges = true;
        return this;
    }

    #endregion
}