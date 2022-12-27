using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;

namespace Aptacode.Geometry.Primitives;

public abstract class Primitive : IEquatable<Primitive>
{
    protected Primitive(BoundingRectangle boundingRectangle)
    {
        BoundingRectangle = boundingRectangle;
    }

    public abstract override string ToString();

    #region Properties

    public BoundingRectangle BoundingRectangle { get; protected set; }

    #endregion


    #region IEquatable

    public abstract override int GetHashCode();

    public abstract bool Equals(Primitive? other);

    public override bool Equals(object obj)
    {
        return obj is Primitive other && other.Equals(this);
    }

    public static bool operator ==(Primitive lhs, Primitive rhs)
    {
        if (lhs is null)
        {
            return rhs is null;
        }

        return lhs.Equals(rhs);
    }

    public static bool operator !=(Primitive lhs, Primitive rhs)
    {
        return !(lhs == rhs);
    }

    #endregion

    #region Collision Detection

    public abstract bool CollidesWith(Vector2 p);
    public abstract bool CollidesWith(Point p);
    public abstract bool CollidesWith(Ellipse p);
    public abstract bool CollidesWith(PolyLine p);
    public abstract bool CollidesWith(Polygon p);
    public abstract bool CollidesWith(BoundingRectangle p);

    public virtual bool CollidesWithPrimitive(Primitive p)
    {
        return p switch
        {
            Point point => CollidesWith(point),
            Ellipse ellipse => CollidesWith(ellipse),
            PolyLine polyline => CollidesWith(polyline),
            Polygon polygon => CollidesWith(polygon)
        };
    }

    #endregion

    #region Transformations

    public abstract Primitive Translate(Vector2 delta);

    public abstract Primitive Rotate(float theta);

    public abstract Primitive Rotate(Vector2 rotationCenter, float theta);

    public abstract Primitive ScaleAboutCenter(Vector2 delta);

    public abstract Primitive Scale(Vector2 scaleCenter, Vector2 delta);
    public abstract Primitive Skew(Vector2 delta);

    public virtual Primitive SetSize(Vector2 delta)
    {
        var scaleFactor = delta / BoundingRectangle.Size;
        ScaleAboutCenter(scaleFactor);
        return this;
    }

    public virtual Primitive SetPosition(Vector2 position)
    {
        Translate(position - BoundingRectangle.Center);
        return this;
    }

    #endregion
}