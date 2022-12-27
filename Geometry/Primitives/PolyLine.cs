using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives;

public sealed class PolyLine : Primitive
{
    public override string ToString()
    {
        return $"PolyLine {Vertices.Print()}";
    }

    public Vector2[] Vertices { get; set; }

    #region IEquatable

    public override bool Equals(Primitive? other)
    {
        return other != null && other is PolyLine otherPolyline && Vertices.AreEqual(otherPolyline.Vertices);
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    #endregion

    #region Properties

    private bool _updateLineSegments = true;
    private readonly (Vector2 P1, Vector2 P2)[] _lineSegments;

    public (Vector2 P1, Vector2 P2)[] LineSegments
    {
        get
        {
            if (_updateLineSegments)
            {
                UpdateLineSegments();
            }

            return _lineSegments;
        }
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
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(BoundingRectangle p)
    {
        return p.CollidesWith(this);
    }

    public override bool CollidesWith(Polygon p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    #endregion

    #region Construction

    private PolyLine(Vector2[] vertices, BoundingRectangle boundingRectangle) : base(boundingRectangle)
    {
        Vertices = vertices;
        _lineSegments = vertices.Length switch
        {
            0 => new (Vector2 P1, Vector2 P2)[0], //If no verticies are given there will be no linesegments
            1 => new (Vector2 P1, Vector2 P2)[1], //If one vertex is given make one linesegment with length 0
            _ => new (Vector2 P1, Vector2 P2)[vertices.Length -
                                              1] //If more then 1 vertex is given create n-1 line segments
        };
    }

    public static PolyLine Create(params float[] points)
    {
        if (points.Length < 2)
        {
            return Zero;
        }

        var vertexArray = points.FromPoints();
        return new PolyLine(vertexArray, vertexArray.ToBoundingRectangle());
    }

    public static readonly PolyLine Zero = Create(Vector2.Zero, Vector2.Zero);

    public static PolyLine Create(params Vector2[] points)
    {
        var vertexArray = points.ToArray();
        return new PolyLine(vertexArray, vertexArray.ToBoundingRectangle());
    }

    #endregion

    #region Transformations

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void UpdateLineSegments()
    {
        _updateLineSegments = false;

        if (Vertices.Length == 0)
        {
            //If there are no vertices then there can be no linesegments
            return;
        }

        if (Vertices.Length == 1)
        {
            //If there is one vertex there will be one line segment with length 0
            _lineSegments[0] = (Vertices[0], Vertices[0]);
            return;
        }

        Span<Vector2> vertexArrayAsSpan = Vertices;
        //Get the first vertex
        var lastVertex = vertexArrayAsSpan[0];
        //Create a line segment for each vertex neighbour pair
        for (var i = 1; i <= vertexArrayAsSpan.Length - 1; i++)
        {
            var nextVertex = vertexArrayAsSpan[i]; //Get the next vertex
            LineSegments[i - 1] =
                new ValueTuple<Vector2, Vector2>(lastVertex,
                    nextVertex); //Create a line segment from the last vertex to the next
            lastVertex = nextVertex; //Update the last vertex
        }
    }
    public override Primitive Translate(Vector2 delta)
    {
        BoundingRectangle = Vertices.Translate(delta);
        _updateLineSegments = true;
        return this;
    }

    public override Primitive Rotate(float theta)
    {
        var center = BoundingRectangle.Center;
        BoundingRectangle = Vertices.Rotate(center, theta);
        _updateLineSegments = true;
        return this;
    }

    public override Primitive Rotate(Vector2 rotationCenter, float theta)
    {
        BoundingRectangle = Vertices.Rotate(rotationCenter, theta);
        _updateLineSegments = true;
        return this;
    }

    public override Primitive ScaleAboutCenter(Vector2 delta)
    {
        BoundingRectangle = Vertices.Scale(BoundingRectangle.Center, delta);
        _updateLineSegments = true;
        return this;
    }

    public override Primitive Scale(Vector2 scaleCenter, Vector2 delta)
    {
        BoundingRectangle = Vertices.Scale(scaleCenter, delta);
        _updateLineSegments = true;
        return this;
    }

    public override Primitive Skew(Vector2 delta)
    {
        BoundingRectangle = Vertices.Skew(delta);
        _updateLineSegments = true;
        return this;
    }

    #endregion
}