using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class Ellipse : Primitive
{
    #region ToString
    public override string ToString()
    {
        return $"Ellipse ({Position.X},{Position.Y}), ({Radii.X},{Radii.Y}), {Rotation}";
    }

    #endregion

    #region Properties

    public float Rotation { get; }
    public Vector2 Radii { get; private set; }
    private bool _updateStandardForm = true;
    private (double A, double B, double C, double D, double E, double F) _standardForm;

    public (double A, double B, double C, double D, double E, double F) StandardForm
    {
        get
        {
            if (_updateStandardForm)
            {
                _standardForm = GetStandardForm();
                _updateStandardForm = false;
            }

            return _standardForm;
        }
    }

    public Vector2 Position => Vertices[0];

    private bool _updateFoci = true;
    private (Vector2, Vector2) _foci;

    public (Vector2, Vector2) Foci
    {
        get
        {
            if (_updateFoci)
            {
                _foci = GetFoci();
                _updateFoci = false;
            }

            return _foci;
        }
    }

    public bool IsCircle => Math.Abs(Radii.X - Radii.Y) < Constants.Tolerance;

    #endregion

    #region Construction

    private Ellipse(VertexArray vertexArray, BoundingRectangle boundingRectangle, Vector2 radii, float rotation) : base(
        vertexArray,
        boundingRectangle)
    {
        Radii = radii;
        Rotation = rotation;
    }

    public static Ellipse Create(float x, float y, float a, float b, float rotation)
    {
        var position = new Vector2(x, y);
        var radii = new Vector2(a, b);
        var vertexArray = VertexArray.Create(position);
        var boundingRectangle = EllipseExtensions.GetBoundingRectangle(position, radii, rotation);

        return new Ellipse(vertexArray, boundingRectangle, radii, rotation);
    }

    public static Ellipse Create(Vector2 position, Vector2 radii, float rotation)
    {
        var vertexArray = VertexArray.Create(position);
        var boundingRectangle = EllipseExtensions.GetBoundingRectangle(position, radii, rotation);

        return new Ellipse(vertexArray, boundingRectangle, radii, rotation);
    }

    public static Ellipse Create(Vector2 position, float radius)
    {
        return Create(position, new Vector2(radius, radius), 0);
    }

    public static readonly Ellipse Zero = Create(Vector2.Zero, Vector2.Zero, 0.0f);
    public static readonly Ellipse Unit = Create(Vector2.Zero, Vector2.One, 0.0f); //This is a circle

    #endregion

    #region IEquatable

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public override bool Equals(Primitive? other)
    {
        if (other == null)
        {
            return false;
        }

        if (other is not Ellipse otherEllipse)
        {
            return false;
        }

        if (Math.Abs(Rotation - otherEllipse.Rotation) > Constants.Tolerance)
        {
            return false;
        }

        var delta = Position - otherEllipse.Position;
        var radiusDelta = Radii - otherEllipse.Radii;
        return Math.Abs(delta.X + delta.Y) < Constants.Tolerance &&
               Math.Abs(radiusDelta.X + radiusDelta.Y) < Constants.Tolerance;
    }

    #endregion

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

    #region Transformations

    public override Ellipse Translate(Vector2 delta)
    {
        Vertices.Translate(delta);
        BoundingRectangle = BoundingRectangle.Translate(delta);
        _updateFoci = _updateStandardForm = true;
        return this;
    }

    public override Ellipse ScaleAboutCenter(Vector2 delta)
    {
        Radii *= delta;
        BoundingRectangle = EllipseExtensions.GetBoundingRectangle(Position, Radii, Rotation);
        _updateFoci = _updateStandardForm = true;

        return this;
    }

    public override Ellipse Rotate(float theta)
    {
        //Todo

        BoundingRectangle = EllipseExtensions.GetBoundingRectangle(Position, Radii, Rotation);
        _updateFoci = _updateStandardForm = true;

        return this;
    }

    public override Ellipse Rotate(Vector2 rotationCenter, float theta)
    {
        //Todo

        BoundingRectangle = EllipseExtensions.GetBoundingRectangle(Position, Radii, Rotation);
        _updateFoci = _updateStandardForm = true;

        return this;
    }

    public override Ellipse Skew(Vector2 delta)
    {
        //Todo

        BoundingRectangle = EllipseExtensions.GetBoundingRectangle(Position, Radii, Rotation);
        _updateFoci = _updateStandardForm = true;

        return this;
    }

    #endregion

    #region Helpers

    private (Vector2 f1, Vector2 f2) GetFoci()
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

    private (double A, double B, double C, double D, double E, double F)
        GetStandardForm() //Returns the coefficents of the ellipse in the form Ax^2 + Bxy + Cy^2 + Dx + Ey + F = 0.
    {
        var rXSquared = Radii.X * Radii.X;
        var rySquared = Radii.Y * Radii.Y;

        var px = Position.X;
        var py = Position.Y;
        var cos = Math.Cos(Rotation);
        var sin = Math.Sin(Rotation);
        var sin2 = Math.Sin(2 * Rotation);

        var d1 = cos * cos / rXSquared;
        var d2 = cos * cos / rySquared;
        var d3 = sin * sin / rXSquared;
        var d4 = sin * sin / rySquared;
        var d5 = sin2 / rXSquared;
        var d6 = sin2 / rySquared;

        var a = d1 + d4;
        var b = d5 - d6;
        var c = d3 + d2;
        var d = -2 * px * d1 - py * d5 - 2 * px * d4 + py * d6;
        var e = -1 * px * d5 - 2 * py * d3 + px * d6 - 2 * py * d2;
        var f = px * px * d1 + px * py * d5 + py * py * d3 + px * px * d4 - px * py * d6 + py * py * d2 - 1;

        return (a, b, c, d, e, f);
    }

    #endregion
}