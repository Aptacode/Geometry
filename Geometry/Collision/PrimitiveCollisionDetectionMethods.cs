using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision;

public static class PrimitiveCollisionDetectionMethods
{
    #region Ellipse

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Circle p1, Circle p2)
    {
        return (p2.Position - p1.Position).Length() < p1.Radius + p2.Radius;
    }

    #endregion

    #region Point

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Point p1, Point p2)
    {
        return (p1.Position - p2.Position).Length() <= Constants.Tolerance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Point p1, PolyLine p2)
    {
        Span<(Vector2 P1, Vector2 P2)> lineSegmentsAsSpan = p2.LineSegments;
        //Check if the point is on any line segment
        for (var i = 0; i < p2.LineSegments.Length; i++)
        {
            if (lineSegmentsAsSpan[i].Intersects(p1.Position))
            {
                return true;
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Point p1, Polygon p2)
    {
        var collision = false;
        var point = p1.Position;
        Span<(Vector2 P1, Vector2 P2)> edgesAsSpan = p2.Edges;
        for (var i = 0; i < edgesAsSpan.Length; i++)
        {
            var edge = edgesAsSpan[i];
            if (edge.Intersects(point))
            {
                return true;
            }

            var a = edge.P1;
            var b = edge.P2;

            if (a.Y > point.Y != b.Y > point.Y && //points y component lies between the lines endpoints
                point.X < (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
            {
                collision = !collision;
            }
        }

        return collision;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Point p1, Circle p2)
    {
        return p2.CollidesWith(p1.Position);
    }

    #endregion

    #region PolyLine

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(PolyLine p1, PolyLine p2)
    {
        //Check if either primitive is contained with the other
        if (p2.CollidesWith(p1.Vertices[0]) ||
            p1.CollidesWith(p2
                .Vertices[0]))
        {
            return true;
        }

        Span<(Vector2 P1, Vector2 P2)> p1SegmentsAsSpan = p1.LineSegments;
        Span<(Vector2 P1, Vector2 P2)> p2SegmentsAsSpan = p2.LineSegments;
        for (var i = 0; i < p1.LineSegments.Length; i++)
        {
            var edge = p1SegmentsAsSpan[i];
            for (var j = 0; j < p2SegmentsAsSpan.Length; j++)
            {
                var lineSegment = p2SegmentsAsSpan[j];
                if (lineSegment.Intersects(edge))
                {
                    return true;
                }
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(PolyLine p1, Polygon p2)
    {
        //Check if either primitive is contained with the other
        if (p2.CollidesWith(p1.Vertices[0]) ||
            p1.CollidesWith(p2
                .Vertices[0]))
        {
            return true;
        }

        Span<(Vector2 P1, Vector2 P2)> p1SegmentsAsSpan = p1.LineSegments;
        Span<(Vector2 P1, Vector2 P2)> p2EdgesAsSpan = p2.Edges;
        for (var i = 0; i < p1SegmentsAsSpan.Length; i++)
        {
            var edge = p1SegmentsAsSpan[i];
            for (var j = 0; j < p2EdgesAsSpan.Length; j++)
            {
                if (p2EdgesAsSpan[j].Intersects(edge))
                {
                    return true;
                }
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(PolyLine p1, Circle p2)
    {
        Span<(Vector2 P1, Vector2 P2)> p1SegmentsAsSpan = p1.LineSegments;

        for (int i = 0; i < p1SegmentsAsSpan.Length; i++)
        {
            var lineSegment = p1SegmentsAsSpan[i];
            if (lineSegment.IntersectsCircle(p2.Position, p2.Radius))
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Polygon

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Polygon p1, Polygon p2)
    {
        //Check if either primitive is contained with the other
        if (p2.CollidesWith(p1.Vertices[0]) ||
            p1.CollidesWith(p2
                .Vertices[0]))
        {
            return true;
        }

        Span<(Vector2 P1, Vector2 P2)> p1EdgesAsSpan = p1.Edges;
        Span<(Vector2 P1, Vector2 P2)> p2EdgesAsSpan = p2.Edges;

        for (var i = 0; i < p1EdgesAsSpan.Length; i++)
        {
            var edge = p1EdgesAsSpan[i];
            for (var j = 0; j < p2EdgesAsSpan.Length; j++)
            {
                var lineSegment = p2EdgesAsSpan[j];
                if (lineSegment.Intersects(edge))
                {
                    return true;
                }
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Polygon p1, Circle p2)
    {
        if (p1.CollidesWith(p2.Position)) //This checks containment
        {
            return true;
        }
        Span<(Vector2 P1, Vector2 P2)> p1EdgesAsSpan = p1.Edges;

        for (int i = 0; i < p1EdgesAsSpan.Length; i++)
        {
            var lineSegment = p1EdgesAsSpan[i];
            if (lineSegment.IntersectsCircle(p2.Position, p2.Radius))
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}