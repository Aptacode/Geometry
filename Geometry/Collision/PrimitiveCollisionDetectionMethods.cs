using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision;

public static class PrimitiveCollisionDetectionMethods
{
    #region Ellipse

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Ellipse p1, Ellipse p2)
    {
        if (Math.Abs(p1.Radii.X - p1.Radii.Y) < Constants.Tolerance &&
            Math.Abs(p2.Radii.X - p2.Radii.Y) < Constants.Tolerance
           ) //Then both ellipses are actually circles and  is definitely(?) faster
        {
            var d = (p2.Position - p1.Position).Length();
            return d < p1.Radii.X + p2.Radii.X;
        }

        var f1 = p1.StandardForm;
        var f2 = p2.StandardForm;
        var (u0, u1, u2, u3, u4) = Ellipse.GetResultantPolynomial(f1.A, f1.B, f1.C, f1.D, f1.E, f1.F, f2.A, f2.B,
            f2.C, f2.D, f2.E, f2.F);

        if (Ellipse.QuarticHasRealRoots(u0, u1, u2, u3, u4)) return true;

        return p1.CollidesWith(p2) || p2.CollidesWith(p1);
    }

    #endregion

    #region Point

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Point p1, Point p2)
    {
        return p1.Equals(p2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Point p1, PolyLine p2)
    {
        if (!p2.BoundingRectangle.CollidesWith(p1))
        {
            return false;
        }

        for (var i = 0; i < p2.LineSegments.Length; i++)
            if (p2.LineSegments[i].OnLineSegment(p1.Position))
                return true;

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Point p1, Polygon p2)
    {
        if (!p2.BoundingRectangle.CollidesWith(p1))
        {
            return false;
        }

        var collision = false;
        var vertices = p2.Vertices.Vertices;
        var point = p1.Position;

        for (int i = 0, j = vertices.Length - 1; i < vertices.Length; j = i++)
        {
            var a = vertices[i];
            var b = vertices[j];
            if ((a, b).OnLineSegment(point)) return true;

            if (a.Y > point.Y != b.Y > point.Y &&
                point.X < (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
                collision = !collision;
        }

        return collision;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Point p1, Ellipse p2) => p2.CollidesWith(p1.Position);

    #endregion

    #region PolyLine

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(PolyLine p1, PolyLine p2)
    {
        if (!p1.BoundingRectangle.CollidesWith(p2))
        {
            return false;
        }

        for (var i = 0; i < p1.LineSegments.Length; i++)
        {
            var edge = p1.LineSegments[i];
            for (var j = 0; j < p2.LineSegments.Length; j++)
            {
                var lineSegment = p2.LineSegments[j];
                if (lineSegment.LineSegmentIntersection(edge)) return true;
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(PolyLine p1, Polygon p2)
    {
        if (!p1.BoundingRectangle.CollidesWith(p2))
        {
            return false;
        }

        for (var i = 0; i < p1.LineSegments.Length; i++)
        {
            var edge = p1.LineSegments[i];
            for (var j = 0; j < p2.Edges.Length; j++)
            {
                var lineSegment = p2.Edges[j];
                if (lineSegment.LineSegmentIntersection(edge)) return true;
            }
        }

        return p2.CollidesWith(p1
            .Vertices[0]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(PolyLine p1, Ellipse p2)
    {
        if (!p1.BoundingRectangle.CollidesWith(p2.BoundingRectangle))
        {
            return false;
        }

        if (Math.Abs(p2.Radii.X - p2.Radii.Y) < Constants.Tolerance)
        {
            //If ellipse is a circle
            return p1.LineSegments.Any(l => l.LineSegmentIntersectsCircle(p2.Position, p2.Radii.X));
        }

        //If ellipse is not a circle
        var stdform = p2.StandardForm;
        foreach (var lineSegment in p1.LineSegments)
        {
            if (p2.CollidesWith(lineSegment.P1) || p2.CollidesWith(lineSegment.P2) || lineSegment.LineSegmentEllipseIntersection(stdform)) return true;
        }

        return false;
    }

    #endregion

    #region Polygon

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Polygon p1, Polygon p2)
    {
        if (!p1.BoundingRectangle.CollidesWith(p2))
        {
            return false;
        }

        for (var i = 0; i < p1.Edges.Length; i++)
        {
            var edge = p1.Edges[i];
            for (var j = 0; j < p2.Edges.Length; j++)
            {
                var lineSegment = p2.Edges[j];
                if (lineSegment.LineSegmentIntersection(edge)) return true;
            }
        }

        return p2.CollidesWith(p1.Vertices[0]) ||
               p1.CollidesWith(p2
                   .Vertices[0]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(Polygon p1, Ellipse p2)
    {
        if (!p1.BoundingRectangle.CollidesWith(p2))
        {
            return false;
        }

        if (p1.CollidesWith(p2.Position)) //This checks containment
            return true;

        if (Math.Abs(p2.Radii.X - p2.Radii.Y) < Constants.Tolerance)
        {
            //If ellipse is a circle
            return p1.Edges.Any(l => l.LineSegmentIntersectsCircle(p2.Position, p2.Radii.X));
        }

        var stdform = p2.StandardForm;
        foreach (var lineSegment in p1.Edges)
        {
            if (p2.CollidesWith(lineSegment.P1) || p2.CollidesWith(lineSegment.P2) || lineSegment.LineSegmentEllipseIntersection(stdform)) return true;
        }

        return false;
    }

    #endregion
}