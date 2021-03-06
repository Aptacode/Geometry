﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Aptacode.Geometry.Collision.Rectangles;

namespace Aptacode.Geometry.Vertices
{
    public static class VertexArrayExtensions
    {
        public static VertexArray Concat(this VertexArray vertexArray, params Vector2[] vertices)
        {
            var newVertices = new Vector2[vertexArray.Length + vertices.Length];
            var count = 0;
            for (var i = 0; i < vertexArray.Length; i++)
            {
                newVertices[count++] = vertexArray[i];
            }

            for (var i = 0; i < vertices.Length; i++)
            {
                newVertices[count++] = vertices[i];
            }

            return new VertexArray(newVertices);
        }

        public static VertexArray Concat(this VertexArray vertexArrayA, VertexArray vertexArrayB)
        {
            var newVertices = new Vector2[vertexArrayA.Length + vertexArrayB.Length];
            var count = 0;
            for (var i = 0; i < vertexArrayA.Length; i++)
            {
                newVertices[count++] = vertexArrayA[i];
            }

            for (var i = 0; i < vertexArrayB.Length; i++)
            {
                newVertices[count++] = vertexArrayB[i];
            }

            return new VertexArray(newVertices);
        }

        public static VertexArray Aggregate(this IEnumerable<VertexArray> vertexArrays)
        {
            var totalVertices = vertexArrays.Aggregate(0, (current, v) => current += v.Length);
            var newVertices = new Vector2[totalVertices];
            var count = 0;
            foreach (var array in vertexArrays)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    newVertices[count++] = array[i];
                }
            }

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
            for (var i = 1; i < n; i++)
            {
                if (points[i].X < points[l].X)
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
                hull.Add(points[p]);

                // Search for a point 'q' such that  
                // orientation(p, x, q) is counterclockwise  
                // for all points 'x'. The idea is to keep  
                // track of last visited most counterclock- 
                // wise point in q. If any point 'i' is more  
                // counterclock-wise than q, then update q. 
                q = (p + 1) % n;

                for (var i = 0; i < n; i++)
                {
                    // If i is more counterclockwise than  
                    // current q, then update q 
                    if (Orientation(points[p], points[i], points[q])
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
        public static VertexArray OrderClockwiseFromLeastX(this VertexArray vertexArray)
        {
            var minX = float.MaxValue;
            var minXindex = -1;
            for (var i = 0; i < vertexArray.Length; i++)
            {
                if (vertexArray[i].X <= minX)
                {
                    if (!(Math.Abs(vertexArray[i].X - minX) < Constants.Tolerance))
                    {
                        minX = vertexArray[i].X;
                        minXindex = i;
                    }
                    else if (vertexArray[i].Y < vertexArray[minXindex].Y) //Two points may lie on a horizontal line, we want the one with the lesser y coord then.
                    {
                        minX = vertexArray[i].X;
                        minXindex = i;
                    }
                }
            }

            var clockwiseArray = new Vector2[vertexArray.Length];
            for (var i = 0; i < clockwiseArray.Length; i++) //cyclically permute the vertexArray elements so that the vertex with the least X is first.
            {
                //probably a better way to do this though.
                var j = (minXindex + i) % clockwiseArray.Length; //Need to check this
                clockwiseArray[i] = vertexArray[j];
            }

            return new VertexArray(clockwiseArray);
        }

        #region Transformation

        public static BoundingRectangle Translate(this VertexArray vertexArray, Vector2 delta)
        {
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minY = float.MaxValue;
            var maxY = float.MinValue;

            var translationMatrix = Matrix3x2.CreateTranslation(delta);
            for (var i = 0; i < vertexArray.Length; i++)
            {
                var vertex = Vector2.Transform(vertexArray[i], translationMatrix);
                vertexArray[i] = vertex;
                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }
                if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }
                if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }
            }

            return BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        public static BoundingRectangle Rotate(this VertexArray vertexArray, Vector2 rotationCenter, float theta)
        {
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minY = float.MaxValue;
            var maxY = float.MinValue;

            var rotationMatrix = Matrix3x2.CreateRotation(theta, rotationCenter);
            for (var i = 0; i < vertexArray.Length; i++)
            {
                var vertex = Vector2.Transform(vertexArray[i], rotationMatrix);
                vertexArray[i] = vertex;
                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }
                if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }
                if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }
            }

            return BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        public static BoundingRectangle Scale(this VertexArray vertexArray, Vector2 scaleCenter, Vector2 delta)
        {
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minY = float.MaxValue;
            var maxY = float.MinValue;

            var scaleMatrix = Matrix3x2.CreateScale(delta, scaleCenter);
            for (var i = 0; i < vertexArray.Length; i++)
            {
                var vertex = Vector2.Transform(vertexArray[i], scaleMatrix);
                vertexArray[i] = vertex;
                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }
                if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }
                if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }
            }

            return BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        public static BoundingRectangle Skew(this VertexArray vertexArray, Vector2 delta)
        {
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minY = float.MaxValue;
            var maxY = float.MinValue;

            var shearMatrix = new Matrix3x2(1, delta.Y, delta.X, 1, 0, 0); //Not 100% on this one.
            for (var i = 0; i < vertexArray.Length; i++)
            {
                var vertex = Vector2.Transform(vertexArray[i], shearMatrix);
                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }
                if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }
                if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }
            }

            return BoundingRectangle.FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        #endregion
    }
}