using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision.Rectangles;

namespace Aptacode.Geometry.Vertices;

public static class VertexArrayExtensions
{
    public static VertexArray Concat(this VertexArray vertexArray, params Vector2[] vertices)
    {
        return Concat(vertexArray, new VertexArray(vertices));
    }

    public static VertexArray Concat(this VertexArray vertexArrayA, VertexArray vertexArrayB)
    {
        var newVertices = new Vector2[vertexArrayA.Length + vertexArrayB.Length];
        if (newVertices.Length == 0) return new VertexArray(Array.Empty<Vector2>());

        var count = 0;

        //Assign first vertex
        var lastVertex = newVertices[count++] = vertexArrayA.Length > 0 ? vertexArrayA[0] : vertexArrayB[0];

        for (var i = 1; i < vertexArrayA.Length; i++)
        {
            var nextVertex = vertexArrayA[i];
            if (lastVertex != nextVertex) newVertices[count++] = nextVertex;
        }

        for (var i = 0; i < vertexArrayB.Length; i++)
        {
            var nextVertex = vertexArrayB[i];
            if (lastVertex != nextVertex) newVertices[count++] = nextVertex;
        }

        //Shrink the array
        if (newVertices.Length != count) Array.Resize(ref newVertices, count);

        return new VertexArray(newVertices);
    }

    public static VertexArray Aggregate(this IEnumerable<VertexArray> vertexArrays)
    {
        var totalVertices = vertexArrays.Aggregate(0, (current, v) => current += v.Length);
        var newVertices = new Vector2[totalVertices];
        var count = 0;
        foreach (var array in vertexArrays)
            for (var i = 0; i < array.Length; i++)
                newVertices[count++] = array[i];

        return new VertexArray(newVertices);
    }

    public static VertexArray Remove(this VertexArray vertexArray, int index)
    {
        var vertices = vertexArray.Vertices.ToList();
        vertices.RemoveAt(index);
        return VertexArray.Create(vertices.ToArray());
    }

    public static VertexArray ToConvexHull(this VertexArray vertexArray, float margin)
    {
        var newVertices = new Vector2[vertexArray.Length * 4];
        var count = 0;

        for (var i = 0; i < vertexArray.Length; i++)
        {
            var vertex = vertexArray[i];
            newVertices[count++] = new Vector2(vertex.X - margin, vertex.Y);
            newVertices[count++] = new Vector2(vertex.X + margin, vertex.Y);
            newVertices[count++] = new Vector2(vertex.X, vertex.Y + margin);
            newVertices[count++] = new Vector2(vertex.X, vertex.Y - margin);
        }

        return new VertexArray(newVertices.ToConvexHull(newVertices.Length));
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

        if (val == 0) return 0; // collinear 

        return val > 0 ? 1 : 2; // clock or counterclock wise 
    }

    // finds the Vector2 array that defines the convex hull of a set of n points. 
    public static Vector2[] ToConvexHull(this Vector2[] points, int n)
    {
        // There must be at least 3 points 
        if (n < 3) return Array.Empty<Vector2>();

        // Initialize Result 
        var hull = new List<Vector2>();

        // Find the leftmost point 
        var l = 0;
        for (var i = 1; i < n; i++)
            if (points[i].X < points[l].X)
                l = i;

        // Start from leftmost point, keep moving  
        // counterclockwise until reach the start point 
        // again. This loop runs O(h) times where h is 
        // number of points in result or output. 
        int p = l, q;
        do
        {
            // Add current point to result 
            hull.Add(points[p]);

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
                if (Orientation(points[p], points[i], points[q])
                    == 2)
                    q = i;

            // Now q is the most counterclockwise with 
            // respect to p. Set p as q for next iteration,  
            // so that q is added to result 'hull' 
            p = q;

            // point 
        } while (p != l); // While we don't come to first  

        return hull.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VertexArray OrderClockwiseFromLeastX(this VertexArray vertexArray)
    {
        var minX = float.MaxValue;
        var minXindex = -1;
        for (var i = 0; i < vertexArray.Length; i++)
            if (vertexArray[i].X <= minX)
            {
                if (!(Math.Abs(vertexArray[i].X - minX) < Constants.Tolerance))
                {
                    minX = vertexArray[i].X;
                    minXindex = i;
                }
                else if
                    (vertexArray[i].Y <
                     vertexArray[minXindex]
                         .Y) //Two points may lie on a horizontal line, we want the one with the lesser y coord then.
                {
                    minX = vertexArray[i].X;
                    minXindex = i;
                }
            }

        var clockwiseArray = new Vector2[vertexArray.Length];
        for (var i = 0;
             i < clockwiseArray.Length;
             i++) //cyclically permute the vertexArray elements so that the vertex with the least X is first.
        {
            //probably a better way to do this though.
            var j = (minXindex + i) % clockwiseArray.Length; //Need to check this
            clockwiseArray[i] = vertexArray[j];
        }

        return new VertexArray(clockwiseArray);
    }

    #region Transformation


    public static BoundingRectangle Transform(this VertexArray vertexArray, Matrix3x2 transformationMatrix)
    {
        //Transform first vertex
        var first = vertexArray[0];
        vertexArray[0] = first = Vector2.Transform(first, transformationMatrix);

        //Set min / max values to the first vertex
        var minX = first.X;
        var maxX = first.X;
        var minY = first.Y;
        var maxY = first.Y;

        for (var i = 1; i < vertexArray.Length; i++)
        {
            //Transform vertex
            var vertex = Vector2.Transform(vertexArray[i], transformationMatrix);
            vertexArray[i] = vertex;

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

        return new BoundingRectangle(new Vector2(minX, minY), new Vector2(maxX, maxY));
    }

    public static BoundingRectangle Translate(this VertexArray vertexArray, Vector2 delta)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        return Transform(vertexArray, Matrix3x2.CreateTranslation(delta));
    }

    public static BoundingRectangle Rotate(this VertexArray vertexArray, Vector2 rotationCenter, float theta)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        return Transform(vertexArray, Matrix3x2.CreateRotation(theta, rotationCenter));
    }

    public static BoundingRectangle Scale(this VertexArray vertexArray, Vector2 scaleCenter, Vector2 delta)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        return Transform(vertexArray, Matrix3x2.CreateScale(delta, scaleCenter));
    }

    public static BoundingRectangle Skew(this VertexArray vertexArray, Vector2 delta)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        return Transform(vertexArray, new Matrix3x2(1, delta.Y, delta.X, 1, 0, 0));
    }

    #endregion

    public static BoundingRectangle ToBoundingRectangle(this VertexArray vertexArray)
    {
        //Exit early if vertex array is empty
        if (vertexArray.Length == 0)
        {
            return BoundingRectangle.Zero;
        }

        //Set min / max values to the first vertex
        var first = vertexArray[0];
        var minX = first.X;
        var maxX = first.X;
        var minY = first.Y;
        var maxY = first.Y;

        for (var i = 1; i < vertexArray.Length; i++)
        {
            //Transform vertex
            var vertex = vertexArray[i];

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

        return new BoundingRectangle(new Vector2(minX, minY), new Vector2(maxX, maxY));
    }
}