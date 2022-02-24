using System;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Utilities;

namespace Aptacode.Geometry.Collision;

public static class Helpers
{
    public static bool OnLineSegment(this (Vector2 A, Vector2 B) line, Vector2 point)
    {
        var d1 = (line.A - point).Length();
        var d2 = (line.B - point).Length();
        var lineLength = (line.B - line.A).Length();
        var delta = d1 + d2;
        return Math.Abs(delta - lineLength) < Constants.Tolerance;
    }

    public static bool NewOnLineSegment(this (Vector2 A, Vector2 B) line, Vector2 point)
    {
        var minVector = Vector2.Min(line.A, line.B);
        var maxVector = Vector2.Max(line.A, line.B);
        if (point.X >= minVector.X && point.X <= maxVector.X && point.Y >= minVector.Y && point.Y <= maxVector.Y)
        {
            var perpDot = Vector2.Dot((line.A - line.B).Perp(), point);
            if (perpDot == 0) return true;
        }

        return false;
    }


    public static (float m, float c) ToLineEquation(Vector2 start, Vector2 end)
    {
        if (Math.Abs(end.X - start.X) < Constants.Tolerance) return (float.PositiveInfinity, start.X);

        var m = (end.Y - start.Y) / (end.X - start.X);
        var c = -m * start.X + start.Y;
        return (m, c);
    }

    public static bool LineSegmentIntersection(this (Vector2, Vector2) line1, (Vector2, Vector2) line2)
    {
        var line1AsVector = line1.Item2 - line1.Item1;
        var line2ACross = line1AsVector.VectorCross(line2.Item1 - line1.Item1);
        var line2BCross = line1AsVector.VectorCross(line2.Item2 - line1.Item1);

        if (line2ACross > 0 && line2BCross > 0 ||
            line2ACross < 0 &&
            line2BCross <
            0) //if both points of one line segment are above or below the other then they cannot intersect.
            return false;

        //If not intersection is not guaranteed so now we check the other way round

        var line2AsVector = line2.Item2 - line2.Item1;
        var line1ACross = line2AsVector.VectorCross(line1.Item1 - line2.Item1);
        var line1BCross = line2AsVector.VectorCross(line1.Item2 - line2.Item1);

        return (!(line1ACross > 0) || !(line1BCross > 0)) && (!(line1ACross < 0) || !(line1BCross < 0));
    }

    public static bool LineSegmentEllipseIntersection(this (Vector2 v1, Vector2 v2) line,
        (double A, double B, double C, double D, double E, double F) stdform)
    {
        var v2 = line.v2;
        var v1 = line.v1;
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