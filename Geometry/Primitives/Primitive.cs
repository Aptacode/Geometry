using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public abstract class Primitive
{
    protected Primitive(VertexArray vertices, BoundingRectangle boundingRectangle)
    {
        Vertices = vertices;
        BoundingRectangle = boundingRectangle;
    }

    #region Methods

    public abstract Primitive GetBoundingPrimitive(float margin);

    #endregion

    public abstract override string ToString();

    #region Properties

    public VertexArray Vertices { get; init; }
    public BoundingRectangle BoundingRectangle { get; protected set; }

    #endregion


    #region IEquatable

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public abstract override bool Equals(object obj);

    public static bool operator ==(Primitive lhs, Primitive rhs)
    {
        if (lhs is null) return rhs is null;

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

    public virtual Primitive Translate(Vector2 delta)
    {
        BoundingRectangle = Vertices.Translate(delta);
        return this;
    }

    public virtual Primitive Rotate(float theta)
    {
        var center = BoundingRectangle.Center;
        BoundingRectangle = Vertices.Rotate(center, theta);
        return this;
    }

    public virtual Primitive Rotate(Vector2 rotationCenter, float theta)
    {
        BoundingRectangle = Vertices.Rotate(rotationCenter, theta);
        return this;
    }

    public virtual Primitive ScaleAboutCenter(Vector2 delta)
    {
        BoundingRectangle = Vertices.Scale(BoundingRectangle.Center, delta);
        return this;
    }

    public virtual Primitive Scale(Vector2 scaleCenter, Vector2 delta)
    {
        BoundingRectangle = Vertices.Scale(scaleCenter, delta);
        return this;
    }

    public virtual Primitive Skew(Vector2 delta)
    {
        BoundingRectangle = Vertices.Skew(delta);
        return this;
    }

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