using System;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;
using Priority_Queue;

namespace Aptacode.Geometry.Collision
{
    public static class SweepingLine
    {
        public static bool CheckCollision(Polygon p, Polygon q)
        {
            if (p.Vertices.Length < 10 && q.Vertices.Length < 10)
            {
                return CollisionDetectorMethods.CollidesWith(p, q);
            }

            List<(Vector2, Vector2)> slSp = new();
            List<(Vector2, Vector2)> slSq = new();

            FastPriorityQueue<VertexEvent> vertexEventQueue = new(p.Vertices.Length + q.Vertices.Length + 1); //Sorted by X coord of the vertex that is the value of the first item of the tuple in the list.
            vertexEventQueue.AddVertexEvents(p);
            vertexEventQueue.AddVertexEvents(q);

            var pVertices = p.Vertices.Vertices;
            var qVertices = q.Vertices.Vertices;
            while (vertexEventQueue.Count > 0) //Sweep along the X axis
            {
                var vertexEvent = vertexEventQueue.Dequeue();
                var vertex = vertexEvent.Vertex;

                var pIndex = Array.IndexOf(pVertices, vertex); //Returns =1 if it's not in the list. O(N).
                var qIndex = Array.IndexOf(qVertices, vertex);

                if (pIndex != -1) //only one of these should have values unless the polygons have a shared point.
                {
                    var currentPVertex = pVertices[pIndex];
                    var nextPVertex = pVertices[(pIndex + 1) % pVertices.Length]; //remainder division to go through array in a cicular way, would mean PolyLines don't work so need to think about it.
                    var previousPVertex = pVertices[(pIndex - 1 + pVertices.Length) % pVertices.Length];
                    if (currentPVertex.X <= nextPVertex.X) //Add edge if the current Vertex is the start of the edge (Also includes vertical edges)
                    {
                        slSp.Add((currentPVertex, nextPVertex));
                    }

                    slSp.Remove((nextPVertex, currentPVertex)); //Remove the edge if this is the end point.
                    if (currentPVertex.X <= previousPVertex.X)
                    {
                        slSp.Add((currentPVertex, previousPVertex));
                    }

                    slSp.Remove((previousPVertex, currentPVertex));
                }

                if (qIndex != -1) //only one of these should have values unless the polygons have a shared point.
                {
                    var currentQVertex = qVertices[qIndex];
                    var nextQVertex = qVertices[(qIndex + 1) % qVertices.Length];
                    var previousQVertex = qVertices[(qIndex - 1 + qVertices.Length) % qVertices.Length];
                    if (currentQVertex.X <= nextQVertex.X) //Add edge if the current Vertex is the start of the edge (Also includes vertical edges)
                    {
                        slSq.Add((currentQVertex, nextQVertex));
                    }

                    slSq.Remove((nextQVertex, currentQVertex)); //Remove the edge if this is the end point.
                    if (currentQVertex.X <= previousQVertex.X)
                    {
                        slSq.Add((currentQVertex, previousQVertex));
                    }

                    slSp.Remove((previousQVertex, currentQVertex));
                }


                if (slSp.Count == 0 || slSq.Count == 0) //No need to check intersection on empty lists.
                {
                    slSp.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance); //Now remove the vertical edges.
                    slSq.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance);
                    continue;
                }

                for (var i = 0; i < slSp.Count; i++) //Now just check the relevant edges
                {
                    var pEdge = slSp[i];
                    for (var j = 0; j < slSq.Count; j++)
                    {
                        var qEdge = slSq[j];
                        if (pEdge.NewLineSegmentIntersection(qEdge))
                        {
                            return true;
                        }
                    }
                }

                slSp.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance); //Now remove the vertical edges.
                slSq.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance);
            }

            return false;
        }

        private static void AddVertexEvents(this IPriorityQueue<VertexEvent, float> vertexEventQueue, Polygon polygon)
        {
            var polyVertices = polygon.Vertices.Vertices;
            for (var i = 0; i < polyVertices.Length; i++)
            {
                var vertex = polyVertices[i];
                var vertexEvent = new VertexEvent(vertex);
                vertexEventQueue.Enqueue(vertexEvent, vertex.X);
            }
        }
    }

    public class VertexEvent : FastPriorityQueueNode
    {
        public Vector2 Vertex;

        public VertexEvent(Vector2 vertex)
        {
            Vertex = vertex;
        }
    }
}