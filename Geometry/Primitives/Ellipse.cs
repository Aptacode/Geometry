using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class Ellipse : Primitive
{
    public readonly float Rotation;

    public Vector2 Radii; //The x width(height) and the y height(width)
    public (double A, double B, double C, double D, double E, double F) StandardForm;
    public Vector2 Position => Vertices[0];
    public (Vector2, Vector2) Foci => GetFoci();
    public bool IsCircle => Math.Abs(Radii.X - Radii.Y) < Constants.Tolerance;

    #region IEquatable

    public override bool Equals(object other)
    {
        if (other is not Ellipse otherEllipse) return false;

        if (Math.Abs(Rotation - otherEllipse.Rotation) > Constants.Tolerance) return false;
        var delta = Position - otherEllipse.Position;
        var radiusDelta = Radii - otherEllipse.Radii;
        return Math.Abs(delta.X + delta.Y) < Constants.Tolerance &&
               Math.Abs(radiusDelta.X + radiusDelta.Y) < Constants.Tolerance;
    }

    #endregion

    public override string ToString()
    {
        return $"Ellipse ({Position.X},{Position.Y}), ({Radii.X},{Radii.Y}), {Rotation}";
    }

    #region Collision Detection

    public override bool CollidesWith(Vector2 p)
    {
        return Vector2CollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(Point p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(p, this);
    }

    public override bool CollidesWith(Ellipse p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(PolyLine p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(p, this);
    }

    public override bool CollidesWith(BoundingRectangle p)
    {
        return p.CollidesWith(this);
    }

    public override bool CollidesWith(Polygon p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(p, this);
    }

    #endregion

    #region Construction

    private Ellipse(VertexArray vertexArray, BoundingRectangle boundingRectangle, Vector2 radii, float rotation,
        (double A, double B, double C, double D, double E, double F) standardForm) : base(vertexArray,
        boundingRectangle)
    {
        Radii = radii;
        Rotation = rotation;
        StandardForm = standardForm;
    }

    public static Ellipse Create(float x, float y, float a, float b, float rotation)
    {
        var position = new Vector2(x, y);
        var radii = new Vector2(a, b);
        var vertexArray = VertexArray.Create(position);
        var boundingRectangle = GetBoundingRectangle(position, radii, rotation);
        var standardForm = GetStandardForm(position, radii, rotation);

        return new Ellipse(vertexArray, boundingRectangle, radii, rotation, standardForm);
    }

    public static Ellipse Create(Vector2 position, Vector2 radii, float rotation)
    {
        var vertexArray = VertexArray.Create(position);
        var boundingRectangle = GetBoundingRectangle(position, radii, rotation);
        var standardForm = GetStandardForm(position, radii, rotation);
        return new Ellipse(vertexArray, boundingRectangle, radii, rotation, standardForm);
    }

    public static Ellipse Create(Vector2 position, float radius)
    {
        return Create(position, new Vector2(radius, radius), 0);
    }

    public static readonly Ellipse Zero = Create(Vector2.Zero, Vector2.Zero, 0.0f);
    public static readonly Ellipse Unit = Create(Vector2.Zero, Vector2.One, 0.0f); //This is a circle

    #endregion


    #region Transformations

    private static BoundingRectangle GetBoundingRectangle(Vector2 position, Vector2 radii, float rotation)
    {
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

    public static (double A, double B, double C, double D, double E, double F)
        GetStandardForm(
            Vector2 position, Vector2 radii,
            float rotation) //Returns the coefficents of the ellipse in the form Ax^2 + Bxy + Cy^2 + Dx + Ey + F = 0.
    {
        var a = radii.X;
        var b = radii.Y;
        if (radii.Y > radii.X)
        {
            a = radii.Y;
            b = radii.X;
        }

        var px = position.X;
        var py = position.Y;
        var cos = Math.Cos(rotation);
        var sin = Math.Sin(rotation);
        var sin2 = Math.Sin(2 * rotation);

        var d1 = cos * cos / (a * a);
        var d2 = cos * cos / (b * b);
        var d3 = sin * sin / (a * a);
        var d4 = sin * sin / (b * b);
        var d5 = sin2 / (a * a);
        var d6 = sin2 / (b * b);

        var A = d1 + d4;
        var B = d5 - d6;
        var C = d3 + d2;
        var D = -2 * px * d1 - py * d5 - 2 * px * d4 + py * d6;
        var E = -1 * px * d5 - 2 * py * d3 + px * d6 - 2 * py * d2;
        var F = px * px * d1 + px * py * d5 + py * py * d3 + px * px * d4 - px * py * d6 + py * py * d2 - 1;

        return (A, B, C, D, E, F);
    }

    public override Ellipse Translate(Vector2 delta)
    {
        Vertices.Translate(delta);
        BoundingRectangle = BoundingRectangle.Translate(delta);
        return this;
    }

    public override Ellipse ScaleAboutCenter(Vector2 delta)
    {
        Radii *= delta;
        BoundingRectangle = GetBoundingRectangle(Position, Radii, Rotation);
        return this;
    }

    public override Ellipse Rotate(float theta)
    {
        //Todo

        BoundingRectangle = GetBoundingRectangle(Position, Radii, Rotation);
        return this;
    }

    public override Ellipse Rotate(Vector2 rotationCenter, float theta)
    {
        //Todo

        BoundingRectangle = GetBoundingRectangle(Position, Radii, Rotation);
        return this;
    }

    public override Ellipse Skew(Vector2 delta)
    {
        //Todo

        BoundingRectangle = GetBoundingRectangle(Position, Radii, Rotation);
        return this;
    }

    #endregion

    #region Helpers

    private (Vector2, Vector2) GetFoci()
    {
        if (Radii.X > Radii.Y)
        {
            var c = Vector2.Transform(new Vector2((float)Math.Sqrt(Radii.X * Radii.X - Radii.Y * Radii.Y), 0.0f),
                Matrix3x2.CreateRotation(Rotation));
            var f1 = Position - c;
            var f2 = Position + c;
            return (f1, f2);
        }

        if (Radii.X < Radii.Y)
        {
            var c = Vector2.Transform(new Vector2(0.0f, (float)Math.Sqrt(Radii.Y * Radii.Y - Radii.X * Radii.X)),
                Matrix3x2.CreateRotation(Rotation));
            var f1 = Position - c;
            var f2 = Position + c;
            return (f1, f2);
        }

        return (Position, Position);
    }

    public static (double u0, double u1, double u2, double u3, double u4) GetResultantPolynomial(double A1,
        double B1, double C1, double D1, double E1, double F1, double A2, double B2, double C2, double D2,
        double E2, double F2) //Takes the coefficients of 2 ellipses (conics) and returns the Bezout determinant R(y) = u0 + u1y + u2y^2 + u3y^3 + u4y^4, is is the equation for the solutions for the y points of intersection, they may be complex.
    {
        var v0 = A1 * B2 - A2 * B1;
        var v1 = A1 * C2 - A2 * C1;
        var v2 = A1 * D2 - A2 * D1;
        var v3 = A1 * E2 - A2 * E1;
        var v4 = A1 * F2 - A2 * F1;
        var v5 = B1 * C2 - B2 * C1;
        var v6 = B1 * E2 - B2 * E1;
        var v7 = B1 * F2 - B2 * F1;
        var v8 = C1 * D2 - C2 * D1;
        var v9 = D1 * E2 - D2 * E1;
        var v10 = D1 * F2 - D2 * F1;

        var u0 = v2 * v10 - v4 * v4;
        var u1 = v0 * v10 + v2 * (v7 + v9) - 2 * v3 * v4;
        var u2 = v0 * (v7 + v9) + v2 * (v6 - v8) - v3 * v3 - 2 * v1 * v4;
        var u3 = v0 * (v6 - v8) + v2 * v5 - 2 * v1 * v3;
        var u4 = v0 * v5 - v1 * v1;

        return (u0, u1, u2, u3, u4);
    }

    public static bool QuarticHasRealRoots(double u0, double u1, double u2, double u3, double u4)
    {
        if (Math.Abs(u4) < Constants.Tolerance && Math.Abs(u3) > Constants.Tolerance) return true;

        if (Math.Abs(u4) < Constants.Tolerance && Math.Abs(u3) < Constants.Tolerance && u2 != 0)
        {
            var det = u1 * u1 - 4 * u2 * u0;
            if (Math.Abs(det) < Constants.Tolerance) return true;

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

        if (delta < 0 && Math.Abs(delta) > 0.01f) return true;

        var p = 8 * u4 * u2 - 3 * u3 * u3;
        var d = 64 * u4 * u4 * u4 * u0
                - 16 * u4 * u4 * u2 * u2
                + 16 * u4 * u3 * u3 * u2
                - 16 * u4 * u4 * u3 * u1
                - 3 * u3 * u3 * u3 * u3;

        return (!(p > 0) || !(Math.Abs(p) > Constants.Tolerance)) && !(d > 0);
    }

    #endregion
}