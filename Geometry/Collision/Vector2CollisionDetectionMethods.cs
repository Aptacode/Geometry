using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision;

public static class Vector2CollisionDetectionMethods
{
    #region Vector2

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(this Point p2, Vector2 p1)
    {
        return Math.Abs(p1.X - p2.Position.X) <= Constants.Tolerance &&
               Math.Abs(p1.Y - p2.Position.Y) <= Constants.Tolerance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(this PolyLine p2, Vector2 p1)
    {
        Span<(Vector2 P1, Vector2 P2)> lineSegmentsAsSpan = p2.LineSegments;
        for (var i = 0; i < lineSegmentsAsSpan.Length; i++)
        {
            if (lineSegmentsAsSpan[i].Intersects(p1))
            {
                return true;
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(this Polygon p2, Vector2 p1)
    {
        var collision = false;
        var edges = p2.Edges;
        var point = p1;

        Span<(Vector2 P1, Vector2 P2)> edgesAsSpan = edges;
        for (var i = 0; i < edgesAsSpan.Length; i++)
        {
            var line = edgesAsSpan[i];
            if (line.Intersects(point))
            {
                return true;
            }

            if (((line.P1.Y >= point.Y && line.P2.Y <= point.Y) ||
                 (line.P1.Y <= point.Y && line.P2.Y >= point.Y)) &&
                point.X <= (line.P2.X - line.P1.X) * (point.Y - line.P1.Y) / (line.P2.Y - line.P1.Y) + line.P1.X)
            {
                collision = !collision;
            }
        }

        return collision;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(this Circle p2, Vector2 p1)
    {
        return (p2.Position - p1).Length() <= p2.Radius;

    }

    #endregion
}