using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision;
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


    public override PolyLine Transform(Matrix3x2 matrix)
    {
        for(int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i] = Vector2.Transform(Vertices[i], matrix);
        }
        return this;
    }

    public override PolyLine Copy()
    {
        var copy = new Vector2[Vertices.Length];
        Vertices.CopyTo(copy,0);
        return new PolyLine(copy);
    }

    public void CopyTo(Polygon destination)
    {
        Vertices.CopyTo(destination.Vertices, 0);
        _updateLineSegments = true;
    }
    public void CopyAndTransformTo(Polygon destination, Matrix3x2 matrix)
    {
        for (int i = 0; i < Vertices.Length; i++)
        {
            destination.Vertices[i] = Vector2.Transform(destination.Vertices[i], matrix);
        }

        _updateLineSegments = true;
    }

    public override bool AreEqual(Primitive other)
    {
        return other != null && other is PolyLine otherPolyline && Vertices.AreEqual(otherPolyline.Vertices);
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

    public override Vector2 GetCentroid()
    {
        if(Vertices.Length == 0)
        {
            return Vector2.Zero;
        }

        Span<Vector2> verticesAsSpan = Vertices;
        Vector2 v = verticesAsSpan[0];

        for (int i = 1; i < verticesAsSpan.Length; i++)
        {
            v += verticesAsSpan[i];
        }

        return v / verticesAsSpan.Length;
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

    public override bool CollidesWith(Circle p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(PolyLine p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    public override bool CollidesWith(Polygon p)
    {
        return PrimitiveCollisionDetectionMethods.CollidesWith(this, p);
    }

    #endregion

    #region Construction

    public PolyLine(Vector2[] vertices)
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

    public PolyLine(params float[] points)
    {
        Vertices = points.FromPoints();
        _lineSegments = Vertices.Length switch
        {
            0 => new (Vector2 P1, Vector2 P2)[0], //If no verticies are given there will be no linesegments
            1 => new (Vector2 P1, Vector2 P2)[1], //If one vertex is given make one linesegment with length 0
            _ => new (Vector2 P1, Vector2 P2)[Vertices.Length -
                                              1] //If more then 1 vertex is given create n-1 line segments
        };
    }

    public static readonly PolyLine Zero = Create(Vector2.Zero, Vector2.Zero);

    public static PolyLine Create(params Vector2[] points)
    {
        var vertexArray = points.ToArray();
        return new PolyLine(vertexArray);
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
    public override PolyLine Translate(Vector2 delta)
    {
        Vertices.Translate(delta);
        _updateLineSegments = true;
        return this;
    }

    public override PolyLine Rotate(float theta)
    {
        var center = GetCentroid();
        Vertices.Rotate(center, theta);
        _updateLineSegments = true;
        return this;
    }

    public override PolyLine Rotate(Vector2 rotationCenter, float theta)
    {
        Vertices.Rotate(rotationCenter, theta);
        _updateLineSegments = true;
        return this;
    }

    public override PolyLine ScaleAboutCenter(Vector2 delta)
    {
        var center = GetCentroid();
        Vertices.Scale(center, delta);
        _updateLineSegments = true;
        return this;
    }

    public override PolyLine Scale(Vector2 scaleCenter, Vector2 delta)
    {
        Vertices.Scale(scaleCenter, delta);
        _updateLineSegments = true;
        return this;
    }

    public override PolyLine Skew(Vector2 delta)
    {
        Vertices.Skew(delta);
        _updateLineSegments = true;
        return this;
    }

    #endregion
}