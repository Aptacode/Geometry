using System;

namespace Aptacode.Geometry.Primitives.Extensions
{
    public static class EllipseExtensions
    {
        public static (double A, double B, double C, double D, double E, double F)
            GetStandardForm(
                this Ellipse ellipse) //Returns the coefficents of the ellipse in the form Ax^2 + Bxy + Cy^2 + Dx + Ey + F = 0.
        {
            var a = ellipse.Radii.X;
            var b = ellipse.Radii.Y;
            if (ellipse.Radii.Y > ellipse.Radii.X)
            {
                a = ellipse.Radii.Y;
                b = ellipse.Radii.X;
            }

            var px = ellipse.Position.X;
            var py = ellipse.Position.Y;

            var d1 = Math.Cos(ellipse.Rotation) * Math.Cos(ellipse.Rotation) / (a * a);
            var d2 = Math.Cos(ellipse.Rotation) * Math.Cos(ellipse.Rotation) / (b * b);
            var d3 = Math.Sin(ellipse.Rotation) * Math.Sin(ellipse.Rotation) / (a * a);
            var d4 = Math.Sin(ellipse.Rotation) * Math.Sin(ellipse.Rotation) / (b * b);
            var d5 = Math.Sin(2 * ellipse.Rotation) / (a * a);
            var d6 = Math.Sin(2 * ellipse.Rotation) / (b * b);

            var A = d1 + d4;
            var B = d5 - d6;
            var C = d3 + d2;
            var D = -2 * px * d1 - py * d5 - 2 * px * d4 + py * d6;
            var E = -1 * px * d5 - 2 * py * d3 + px * d6 - 2 * py * d2;
            var F = px * px * d1 + px * py * d5 + py * py * d3 + px * px * d4 - px * py * d6 + py * py * d2 - 1;

            return (A, B, C, D, E, F);
        }
    }
}