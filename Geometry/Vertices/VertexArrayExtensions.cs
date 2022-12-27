using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision.Rectangles;

namespace Aptacode.Geometry.Vertices;

public static class VertexArrayExtensions
{

    public static string Print(this Vector2[] verticies)
    {
        return string.Join(", ", verticies.Select(v => $"({v.X},{v.Y})"));
    }

    public static Vector2[] FromPoints(this float[] points)
    {
        var vertexArray = new Vector2[points.Length / 2];
        var vertexIndex = 0;
        Span<float> verticesAsSpan = points;
        for (var i = 0; i < verticesAsSpan.Length; i += 2)
        {
            vertexArray[vertexIndex++] = new Vector2(verticesAsSpan[i], verticesAsSpan[i + 1]);
        }

        return vertexArray;
    }

    public static Vector2[] Concat(this Vector2[] vertexArrayA, Vector2[] vertexArrayB)
    {
        var newVertices = new Vector2[vertexArrayA.Length + vertexArrayB.Length];
        if (newVertices.Length == 0)
        {
            return newVertices;
        }

        var count = 0;
        Span<Vector2> vertexArrayAAsSpan = vertexArrayA;
        Span<Vector2> vertexArrayBAsSpan = vertexArrayB;

        //Assign first vertex
        var lastVertex = newVertices[count++] = vertexArrayA.Length > 0 ? vertexArrayAAsSpan[0] : vertexArrayBAsSpan[0];
        for (var i = 1; i < vertexArrayAAsSpan.Length; i++)
        {
            var nextVertex = vertexArrayAAsSpan[i];
            if (lastVertex != nextVertex)
            {
                newVertices[count++] = nextVertex;
            }
        }

        for (var i = 0; i < vertexArrayBAsSpan.Length; i++)
        {
            var nextVertex = vertexArrayBAsSpan[i];
            if (lastVertex != nextVertex)
            {
                newVertices[count++] = nextVertex;
            }
        }

        //Shrink the array
        if (newVertices.Length != count)
        {
            Array.Resize(ref newVertices, count);
        }

        return newVertices;
    }

    public static Vector2[] Remove(this Vector2[] vertexArray, int index)
    {
        var vertices = vertexArray.ToList();
        vertices.RemoveAt(index);
        return vertices.ToArray();
    }

    public static Vector2[] ToConvexHull(this Vector2[] vertexArray, float margin)
    {
        var newVertices = new Vector2[vertexArray.Length * 4];
        var count = 0;

        Span<Vector2> vertexArrayAsSpan = vertexArray;
        for (var i = 0; i < vertexArrayAsSpan.Length; i++)
        {
            var vertex = vertexArrayAsSpan[i];
            newVertices[count++] = vertex with { X = vertex.X - margin };
            newVertices[count++] = vertex with { X = vertex.X + margin };
            newVertices[count++] = vertex with { Y = vertex.Y + margin };
            newVertices[count++] = vertex with { Y = vertex.Y - margin };
        }

        return newVertices.ToConvexHull(newVertices.Length);
    }

    // To find orientation of ordered triplet (p, q, r). 
    // The function returns following values 
    // 0 --> p, q and r are colinear 
    // 1 --> Clockwise 
    // 2 --> Counterclockwise 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Orientation(Vector2 p, Vector2 q, Vector2 r)
    {
        var val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

        if (val == 0)
        {
            return 0; // collinear 
        }

        return val > 0 ? 1 : 2; // clock or counterclock wise 
    }

    // finds the Vector2 array that defines the convex hull of a set of n points. 
    public static Vector2[] ToConvexHull(this Vector2[] points, int n)
    {
        // There must be at least 3 points 
        if (n < 3)
        {
            return Array.Empty<Vector2>();
        }

        // Initialize Result 
        var hull = new List<Vector2>();

        // Find the leftmost point 
        var l = 0;
        Span<Vector2> pointsAsSpan = points;
        for (var i = 1; i < n; i++)
        {
            if (pointsAsSpan[i].X < pointsAsSpan[l].X)
            {
                l = i;
            }
        }

        // Start from leftmost point, keep moving  
        // counterclockwise until reach the start point 
        // again. This loop runs O(h) times where h is 
        // number of points in result or output. 
        int p = l, q;
        do
        {
            // Add current point to result 
            hull.Add(pointsAsSpan[p]);

            // Search for a point 'q' such that  
            // orientation(p, x, q) is counterclockwise  
            // for all points 'x'. The idea is to keep  
            // track of last visited most counterclock- 
            // wise point in q. If any point 'i' is more  
            // counterclock-wise than q, then update q. 
            q = (p + 1) % n;

            for (var i = 0; i < n; i++)
                // If i is more counterclockwise than  
                // current q, then update q 
            {
                if (Orientation(pointsAsSpan[p], pointsAsSpan[i], pointsAsSpan[q])
                    == 2)
                {
                    q = i;
                }
            }

            // Now q is the most counterclockwise with 
            // respect to p. Set p as q for next iteration,  
            // so that q is added to result 'hull' 
            p = q;

            // point 
        } while (p != l); // While we don't come to first  

        return hull.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2[] OrderClockwiseFromLeastX(this Vector2[] vertexArray)
    {
        var minX = float.MaxValue;
        var minXindex = -1;
        Span<Vector2> vertexArrayAsSpan = vertexArray;
        for (var i = 0; i < vertexArrayAsSpan.Length; i++)
        {
            if (vertexArrayAsSpan[i].X > minX)
            {
                continue;
            }

            if (Math.Abs(vertexArrayAsSpan[i].X - minX) >= Constants.Tolerance)
            {
                minX = vertexArrayAsSpan[i].X;
                minXindex = i;
            }
            else if
                (vertexArrayAsSpan[i].Y <
                 vertexArray[minXindex]
                     .Y) //Two points may lie on a horizontal line, we want the one with the lesser y coord then.
            {
                minX = vertexArrayAsSpan[i].X;
                minXindex = i;
            }
        }

        var clockwiseArray = new Vector2[vertexArray.Length];
        Span<Vector2> clockwiseArrayAsSpan = clockwiseArray;

        for (var i = 0;
             i < clockwiseArray.Length;
             i++) //cyclically permute the vertexArray elements so that the vertex with the least X is first.
        {
            //probably a better way to do this though.
            var j = (minXindex + i) % clockwiseArray.Length; //Need to check this
            clockwiseArrayAsSpan[i] = vertexArrayAsSpan[j];
        }

        return clockwiseArray;
    }

    public static BoundingRectangle ToBoundingRectangle(this Vector2[] vertexArray)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        Span<Vector2> vertexArrayAsSpan = vertexArray;

        //Set min / max values to the first vertex
        var first = vertexArrayAsSpan[0];
        var minX = first.X;
        var maxX = first.X;
        var minY = first.Y;
        var maxY = first.Y;

        for (var i = 1; i < vertexArrayAsSpan.Length; i++)
        {
            //Transform vertex
            var vertex = vertexArrayAsSpan[i];

            //update min / max X values
            if (vertex.X < minX)
            {
                minX = vertex.X;
            }
            else if (vertex.X > maxX)
            {
                maxX = vertex.X;
            }

            //update min / max Y values
            if (vertex.Y < minY)
            {
                minY = vertex.Y;
            }
            else if (vertex.Y > maxY)
            {
                maxY = vertex.Y;
            }
        }

        return new BoundingRectangle
        {
            BottomLeft = new Vector2(minX, minY),
            TopRight = new Vector2(maxX, maxY)
        };
    }

    #region Transformation

    public static BoundingRectangle Transform(this Vector2[] vertexArray, Matrix3x2 transformationMatrix)
    {
        Span<Vector2> vertexArrayAsSpan = vertexArray;

        //Transform first vertex
        var first = vertexArrayAsSpan[0];
        vertexArray[0] = first = Vector2.Transform(first, transformationMatrix);

        //Set min / max values to the first vertex
        var minX = first.X;
        var maxX = first.X;
        var minY = first.Y;
        var maxY = first.Y;

        for (var i = 1; i < vertexArray.Length; i++)
        {
            //Transform vertex
            var vertex = Vector2.Transform(vertexArrayAsSpan[i], transformationMatrix);
            vertexArrayAsSpan[i] = vertex;

            //update min / max X values
            if (vertex.X < minX)
            {
                minX = vertex.X;
            }
            else if (vertex.X > maxX)
            {
                maxX = vertex.X;
            }

            //update min / max Y values
            if (vertex.Y < minY)
            {
                minY = vertex.Y;
            }
            else if (vertex.Y > maxY)
            {
                maxY = vertex.Y;
            }
        }

        return new BoundingRectangle
        {
            BottomLeft = new Vector2(minX, minY),
            TopRight = new Vector2(maxX, maxY)
        };
    }

    public static BoundingRectangle Translate(this Vector2[] vertexArray, Vector2 delta)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        return Transform(vertexArray, Matrix3x2.CreateTranslation(delta));
    }

    public static BoundingRectangle Rotate(this Vector2[] vertexArray, Vector2 rotationCenter, float theta)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        return Transform(vertexArray, Matrix3x2.CreateRotation(theta, rotationCenter));
    }

    public static BoundingRectangle Scale(this Vector2[] vertexArray, Vector2 scaleCenter, Vector2 delta)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        return Transform(vertexArray, Matrix3x2.CreateScale(delta, scaleCenter));
    }

    public static BoundingRectangle Skew(this Vector2[] vertexArray, Vector2 delta)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        return Transform(vertexArray, new Matrix3x2(1, delta.Y, delta.X, 1, 0, 0));
    }


    #endregion


    public static bool AreEqual(this Vector2[] lhs, Vector2[] rhs)
    {
        if (lhs.Length != rhs.Length)
        {
            return false;
        }

        Span<Vector2> lhsVertices = lhs;
        Span<Vector2> rhsVertices = rhs;
        for (var i = 0; i < lhsVertices.Length; i++)
        {
            var delta = lhsVertices[i] - rhsVertices[i];
            if (Math.Abs(delta.X) + Math.Abs(delta.Y) > Constants.Tolerance)
            {
                return false;
            }
        }

        return true;
    }
}