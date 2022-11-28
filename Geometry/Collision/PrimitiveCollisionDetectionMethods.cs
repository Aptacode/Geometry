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
        //Early escape if the ellipse bounding rectangles don't collide
        if (!p2.BoundingRectangle.CollidesWith(p1.BoundingRectangle))
        {
            return false;
        }

        if (p1.IsCircle && p2.IsCircle)
            //Both ellipses are circles can check if distance between center is less then combined radii
        {
            return (p2.Position - p1.Position).Length() < p1.Radii.X + p2.Radii.X;
        }

        var f1 = p1.StandardForm;
        var f2 = p2.StandardForm;
        var (u0, u1, u2, u3, u4) = EllipseExtensions.GetResultantPolynomial(f1.A, f1.B, f1.C, f1.D, f1.E, f1.F, f2.A,
            f2.B,
            f2.C, f2.D, f2.E, f2.F);

        return EllipseExtensions.QuarticHasRealRoots(u0, u1, u2, u3, u4);
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
        //Check if point is inside polyline bounding rectangle
        if (!p2.BoundingRectangle.CollidesWith(p1.Position))
        {
            return false;
        }

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
        //Check if the point is in the polygons bounding rectangle
        if (!p2.BoundingRectangle.CollidesWith(p1.BoundingRectangle))
        {
            return false;
        }

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
    public static bool CollidesWith(Point p1, Ellipse p2)
    {
        return p2.CollidesWith(p1.Position);
    }

    #endregion

    #region PolyLine

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CollidesWith(PolyLine p1, PolyLine p2)
    {
        //Check if bounding rectangles collide
        if (!p1.BoundingRectangle.CollidesWith(p2.BoundingRectangle))
        {
            return false;
        }

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
        if (!p1.BoundingRectangle.CollidesWith(p2.BoundingRectangle))
        {
            return false;
        }

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
    public static bool CollidesWith(PolyLine p1, Ellipse p2)
    {
        if (!p1.BoundingRectangle.CollidesWith(p2.BoundingRectangle))
        {
            return false;
        }
        Span<(Vector2 P1, Vector2 P2)> p1SegmentsAsSpan = p1.LineSegments;

        if (p2.IsCircle)
            //If ellipse is a circle
        {
            for (int i = 0; i < p1SegmentsAsSpan.Length; i++)
            {
                var lineSegment = p1SegmentsAsSpan[i];
                if (lineSegment.IntersectsCircle(p2.Position, p2.Radii.X))
                {
                    return true;
                }
            }

            return false;
        }

        //If ellipse is not a circle
        var stdform = p2.StandardForm;
        for (int i = 0; i < p1SegmentsAsSpan.Length; i++)
        {
            var lineSegment = p1SegmentsAsSpan[i];
            if (p2.CollidesWith(lineSegment.P1) || p2.CollidesWith(lineSegment.P2) || lineSegment.Intersects(stdform))
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
        if (!p1.BoundingRectangle.CollidesWith(p2.BoundingRectangle))
        {
            return false;
        }

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
    public static bool CollidesWith(Polygon p1, Ellipse p2)
    {
        if (!p1.BoundingRectangle.CollidesWith(p2))
        {
            return false;
        }

        if (p1.CollidesWith(p2.Position)) //This checks containment
        {
            return true;
        }
        Span<(Vector2 P1, Vector2 P2)> p1EdgesAsSpan = p1.Edges;

        if (p2.IsCircle)
            //If ellipse is a circle
        {
            for (int i = 0; i < p1EdgesAsSpan.Length; i++)
            {
                var lineSegment = p1EdgesAsSpan[i];
                if (lineSegment.IntersectsCircle(p2.Position, p2.Radii.X))
                {
                    return true;
                }
            }

            return false;
        }

        var stdform = p2.StandardForm;


        for (int i = 0; i < p1EdgesAsSpan.Length; i++)
        {
            var lineSegment = p1EdgesAsSpan[i];
            if (p2.CollidesWith(lineSegment.P1) || p2.CollidesWith(lineSegment.P2) ||
                lineSegment.Intersects(stdform))
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}