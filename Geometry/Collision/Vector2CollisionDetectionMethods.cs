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
        for (var i = 0; i < p2.LineSegments.Length; i++)
            if (p2.LineSegments[i].OnLineSegment(p1))
                return true;

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(this Polygon p2, Vector2 p1)
    {
        var collision = false;
        var edges = p2.Edges;
        var point = p1;

        foreach (var line in edges)
        {
            if (line.OnLineSegment(point)) return true;

            if ((line.P1.Y >= point.Y && line.P2.Y <= point.Y ||
                 line.P1.Y <= point.Y && line.P2.Y >= point.Y) &&
                point.X <= (line.P2.X - line.P1.X) * (point.Y - line.P1.Y) / (line.P2.Y - line.P1.Y) + line.P1.X)
                collision = !collision;
        }

        return collision;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(this Ellipse p2, Vector2 p1)
    {
        if (!p2.BoundingRectangle.CollidesWith(p1))
        {
            return false;
        }

        var f1dist = (p2.Foci.Item1 - p1).Length();
        var f2dist = (p2.Foci.Item2 - p1).Length();
        if (p2.Radii.X >= p2.Radii.Y) //X is the major axis
            return f1dist + f2dist <= 2 * p2.Radii.X;

        if (p2.Radii.Y > p2.Radii.X) //Y is the major axis
            return f1dist + f2dist <= 2 * p2.Radii.Y;

        return false;
    }

    #endregion
}