using System;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Utilities;

namespace Aptacode.Geometry.Collision;

public static class Helpers
{
    public static bool OnLineSegment(this (Vector2 P1, Vector2 P2) line, Vector2 point)
    {
        var d1 = (line.P1 - point).Length();
        var d2 = (line.P2 - point).Length();
        var lineLength = (line.P2 - line.P1).Length();
        var delta = d1 + d2;
        return Math.Abs(delta - lineLength) < Constants.Tolerance;
    }

    public static bool NewOnLineSegment(this (Vector2 P1, Vector2 P2) line, Vector2 point)
    {
        var minVector = Vector2.Min(line.P1, line.P2);
        var maxVector = Vector2.Max(line.P1, line.P2);
        if (point.X >= minVector.X && point.X <= maxVector.X && point.Y >= minVector.Y && point.Y <= maxVector.Y)
        {
            var perpDot = Vector2.Dot((line.P1 - line.P2).Perp(), point);
            if (perpDot == 0) return true;
        }

        return false;
    }

    // Given three collinear points p, q, r, the function checks if
    // point q lies on line segment 'pr'
    private static bool OnSegment(Vector2 p, Vector2 q, Vector2 r)
    {
        return q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
               q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y);
    }

    // To find orientation of ordered triplet (p, q, r).
    // The function returns following values
    // 0 --> p, q and r are collinear
    // 1 --> Clockwise
    // 2 --> Counterclockwise
    private static int Orientation(Vector2 p, Vector2 q, Vector2 r)
    {
        // See https://www.geeksforgeeks.org/orientation-3-ordered-points/
        // for details of bel/ow formula.
        var val = (q.Y - p.Y) * (r.X - q.X) -
                  (q.X - p.X) * (r.Y - q.Y);

        if (val == 0) return 0; // collinear

        return val > 0 ? 1 : 2; // clock or counterclock wise
    }

    // The main function that returns true if line segment 'p1q1'
    // and 'p2q2' intersect.
    public static bool LineSegmentIntersection(this (Vector2 P1, Vector2 P2) line1, (Vector2 P1, Vector2 P2) line2)
    {
        var (p1, q1) = line1;
        var (p2, q2) = line2;
        // Find the four orientations needed for general and
        // special cases
        var o1 = Orientation(p1, q1, p2);
        var o2 = Orientation(p1, q1, q2);
        var o3 = Orientation(p2, q2, p1);
        var o4 = Orientation(p2, q2, q1);

        // General case
        if (o1 != o2 && o3 != o4)
            return true;

        // Special Cases
        // p1, q1 and p2 are collinear and p2 lies on segment p1q1
        if (o1 == 0 && OnSegment(p1, p2, q1)) return true;

        // p1, q1 and q2 are collinear and q2 lies on segment p1q1
        if (o2 == 0 && OnSegment(p1, q2, q1)) return true;

        // p2, q2 and p1 are collinear and p1 lies on segment p2q2
        if (o3 == 0 && OnSegment(p2, p1, q2)) return true;

        // p2, q2 and q1 are collinear and q1 lies on segment p2q2
        if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

        return false; // Doesn't fall in any of the above cases
    }

    public static bool LineSegmentEllipseIntersection(this (Vector2 P1, Vector2 P2) line,
        (double A, double B, double C, double D, double E, double F) stdform)
    {
        var v2 = line.P2;
        var v1 = line.P1;
        var dx = v2.X - v1.X;
        var dy = v2.Y - v1.Y;

        var a = stdform.A * dx * dx + stdform.B * dx * dy + stdform.C * dy * dy;
        var b = 2 * stdform.A * v1.X * dx + stdform.B * v1.X * dy + 2 * stdform.C * v1.Y * dy +
                stdform.D * dx + stdform.E * dy;
        var c = stdform.A * v1.X * v1.X + stdform.B * v1.X * v1.Y + stdform.C * v1.Y * v1.Y +
                stdform.D * v1.X + stdform.E * v1.Y + stdform.F;

        var det = b * b - 4 * a * c;
        if (!(det >= 0)) return false;
        var t1 = (-b + Math.Sqrt(det)) / (2 * a);
        var t2 = (-b - Math.Sqrt(det)) / (2 * a);
        return t1 is >= 0 and <= 1 || t2 is >= 0 and <= 1;
    }

    public static Vector2[] MakeLargeVertexList(int start, int xtranslate)
    {
        var vertexList = new List<Vector2>();
        for (var i = start; i < 50; i++) vertexList.Add(new Vector2(i + xtranslate, i));

        vertexList.Add(new Vector2(50 + xtranslate, start));
        return vertexList.ToArray();
    }
}