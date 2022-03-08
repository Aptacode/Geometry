using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision.Rectangles;

namespace Aptacode.Geometry.Primitives;

public static class EllipseExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BoundingRectangle GetBoundingRectangle(Vector2 position, Vector2 radii, float rotation)
    {
        if (Math.Abs(radii.X - radii.Y) <= Constants.Tolerance)
        {
            return new BoundingRectangle(position - radii, position + radii);
        }

        var asquared = radii.X * radii.X;
        var bsquared = radii.Y * radii.Y;

        var costheta = Math.Cos(rotation);
        var costhetasquared = costheta * costheta;

        var sintheta = Math.Sin(rotation);
        var sinthetasquared = sintheta * sintheta;

        var xdelta = (float)Math.Sqrt(asquared * costhetasquared + bsquared * sinthetasquared);
        var ydelta = (float)Math.Sqrt(asquared * sinthetasquared + bsquared * costhetasquared);
        var delta = new Vector2(xdelta, ydelta);
        var bottomLeft = position - delta;
        var topRight = position + delta;

        return new BoundingRectangle
        {
            BottomLeft = bottomLeft,
            TopRight = topRight
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (double u0, double u1, double u2, double u3, double u4) GetResultantPolynomial(double a1,
       double b1, double c1, double d1, double e1, double f1, double a2, double b2, double c2, double d2,
       double e2, double f2) //Takes the coefficients of 2 ellipses (conics) and returns the Bezout determinant R(y) = u0 + u1y + u2y^2 + u3y^3 + u4y^4, is is the equation for the solutions for the y points of intersection, they may be complex.
    {
        var v0 = a1 * b2 - a2 * b1;
        var v1 = a1 * c2 - a2 * c1;
        var v2 = a1 * d2 - a2 * d1;
        var v3 = a1 * e2 - a2 * e1;
        var v4 = a1 * f2 - a2 * f1;
        var v5 = b1 * c2 - b2 * c1;
        var v6 = b1 * e2 - b2 * e1;
        var v7 = b1 * f2 - b2 * f1;
        var v8 = c1 * d2 - c2 * d1;
        var v9 = d1 * e2 - d2 * e1;
        var v10 = d1 * f2 - d2 * f1;

        var u0 = v2 * v10 - v4 * v4;
        var u1 = v0 * v10 + v2 * (v7 + v9) - 2 * v3 * v4;
        var u2 = v0 * (v7 + v9) + v2 * (v6 - v8) - v3 * v3 - 2 * v1 * v4;
        var u3 = v0 * (v6 - v8) + v2 * v5 - 2 * v1 * v3;
        var u4 = v0 * v5 - v1 * v1;

        return (u0, u1, u2, u3, u4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool QuarticHasRealRoots(double u0, double u1, double u2, double u3, double u4)
    {
        if (Math.Abs(u4) < Constants.Tolerance && Math.Abs(u3) > Constants.Tolerance)
        {
            return true;
        }

        if (Math.Abs(u4) < Constants.Tolerance && Math.Abs(u3) < Constants.Tolerance && Math.Abs(u2) < Constants.Tolerance)
        {
            var det = u1 * u1 - 4 * u2 * u0;
            if (Math.Abs(det) < Constants.Tolerance)
            {
                return true;
            }

            return det >= 0;
        }

        var delta = 256 * u4 * u4 * u4 * u0 * u0 * u0
                    - 192 * u4 * u4 * u3 * u1 * u0 * u0
                    - 128 * u4 * u4 * u2 * u2 * u0 * u0
                    + 144 * u4 * u4 * u2 * u1 * u1 * u0
                    - 27 * u4 * u4 * u1 * u1 * u1 * u1
                    + 144 * u4 * u3 * u3 * u1 * u0 * u0
                    - 6 * u4 * u3 * u3 * u1 * u1 * u0
                    - 80 * u4 * u3 * u2 * u2 * u1 * u0
                    + 18 * u4 * u3 * u2 * u1 * u1 * u1
                    + 16 * u4 * u2 * u2 * u2 * u2 * u0
                    - 4 * u4 * u2 * u2 * u2 * u1 * u1
                    - 27 * u3 * u3 * u3 * u3 * u0 * u0
                    + 18 * u3 * u3 * u3 * u2 * u1 * u0
                    - 4 * u3 * u3 * u3 * u1 * u1 * u1
                    - 4 * u3 * u3 * u2 * u2 * u2 * u0
                    + u3 * u3 * u2 * u2 * u1 * u1;

        if (delta < 0 && Math.Abs(delta) > 0.01f)
        {
            return true;
        }

        var p = 8 * u4 * u2 - 3 * u3 * u3;
        var d = 64 * u4 * u4 * u4 * u0
                - 16 * u4 * u4 * u2 * u2
                + 16 * u4 * u3 * u3 * u2
                - 16 * u4 * u4 * u3 * u1
                - 3 * u3 * u3 * u3 * u3;

        return (p <= 0 || Math.Abs(p) <= Constants.Tolerance) && d <= 0;
    }
}