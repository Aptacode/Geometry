using System.Numerics;

namespace Aptacode.Geometry.Primitives;

public abstract class Primitive
{
    public abstract override string ToString();

    public abstract Vector2 GetCentroid();

    #region Collision Detection

    public abstract bool CollidesWith(Vector2 p);
    public abstract bool CollidesWith(Point p);
    public abstract bool CollidesWith(Circle p);
    public abstract bool CollidesWith(PolyLine p);
    public abstract bool CollidesWith(Polygon p);

    public virtual bool CollidesWithPrimitive(Primitive p)
    {
        return p switch
        {
            Point point => CollidesWith(point),
            Circle ellipse => CollidesWith(ellipse),
            PolyLine polyline => CollidesWith(polyline),
            Polygon polygon => CollidesWith(polygon)
        };
    }

    #endregion

    #region Transformations

    public abstract Primitive Transform(Matrix3x2 matrix);
    public abstract Primitive Copy();
    public abstract bool AreEqual(Primitive other);

    public abstract Primitive Translate(Vector2 delta);

    public abstract Primitive Rotate(float theta);

    public abstract Primitive Rotate(Vector2 rotationCenter, float theta);

    public abstract Primitive ScaleAboutCenter(Vector2 delta);
    public abstract Primitive Scale(Vector2 scaleCenter, Vector2 delta);
    public abstract Primitive Skew(Vector2 delta);

    #endregion
}