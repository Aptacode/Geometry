using Aptacode.Geometry.Primitives;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Aptacode.Geometry.Collision
{
    public class SweepingLine
    {

        private readonly List<(Vector2, Vector2)> _slSp = new();
        private readonly List<(Vector2, Vector2)> _slSq = new();

        private readonly FastPriorityQueue<VertexEvent> _vertexEventQueue = new(100); //Sorted by X coord of the vertex that is the value of the first item of the tuple in the list.
        

        public bool RunSweepingLine(Polygon P, Polygon Q)
        {
            _slSp.Clear();
            _slSq.Clear();
            _vertexEventQueue.Clear();

            PopulateVertexEventQueue(P, Q);
            var pVertices = P.Vertices.Vertices;
            var qVertices = Q.Vertices.Vertices;
            while(_vertexEventQueue.Count > 0) //Sweep along the X axis
            {
                var vertexEvent = _vertexEventQueue.Dequeue();
                var vertex = vertexEvent.Vertex;

                var pIndex = Array.IndexOf(pVertices, vertex); //Returns =1 if it's not in the list. O(N).
                var qIndex = Array.IndexOf(qVertices, vertex);

                if (pIndex != -1)//only one of these should have values unless the polygons have a shared point.
                {
                    var currentPVertex = pVertices[pIndex];
                    var nextPVertex = pVertices[(pIndex + 1) % pVertices.Length]; //remainder division to go through array in a cicular way, would mean PolyLines don't work so need to think about it.
                    var previousPVertex = pVertices[(pIndex - 1 + pVertices.Length) % pVertices.Length];
                    if(currentPVertex.X <= nextPVertex.X) //Add edge if the current Vertex is the start of the edge (Also includes vertical edges)
                    {
                        _slSp.Add((currentPVertex, nextPVertex));
                    }
                    _slSp.Remove((nextPVertex, currentPVertex)); //Remove the edge if this is the end point.
                    if(currentPVertex.X <= previousPVertex.X)
                    {
                        _slSp.Add((currentPVertex, previousPVertex));
                    }
                    _slSp.Remove((previousPVertex, currentPVertex));
                }
                if (qIndex != -1)//only one of these should have values unless the polygons have a shared point.
                {
                    var currentQVertex = qVertices[qIndex];
                    var nextQVertex = qVertices[(qIndex + 1) % qVertices.Length];
                    var previousQVertex = qVertices[(qIndex - 1 + qVertices.Length) % qVertices.Length];
                    if(currentQVertex.X <= nextQVertex.X) //Add edge if the current Vertex is the start of the edge (Also includes vertical edges)
                    {
                        _slSq.Add((currentQVertex, nextQVertex));
                    }
                    _slSq.Remove((nextQVertex, currentQVertex)); //Remove the edge if this is the end point.
                    if(currentQVertex.X <= previousQVertex.X)
                    {
                        _slSq.Add((currentQVertex, previousQVertex));
                    }
                    _slSp.Remove((previousQVertex, currentQVertex));
                }                       
                

                if(_slSp.Count == 0 || _slSq.Count == 0)//No need to check intersection on empty lists.
                {
                    _slSp.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance); //Now remove the vertical edges.
                    _slSq.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance);
                    continue;
                }

                for (var i = 0; i < _slSp.Count; i++) //Now just check the relevant edges
                {
                    var pEdge = _slSp[i];
                    for (var j = 0; j < _slSq.Count; j++)
                    {
                        var qEdge = _slSq[j];
                        if(pEdge.newLineSegmentIntersection(qEdge))
                        {
                            return true;
                        }
                    }
                }

                _slSp.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance); //Now remove the vertical edges.
                _slSq.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance);
                
                
            }
            return false;
        }

        private void PopulateVertexEventQueue(Polygon P, Polygon Q)
        {
            AddVertexEvents(P);
            AddVertexEvents(Q);
        }

        private void AddVertexEvents(Polygon polygon)
        {
            var polyVertices = polygon.Vertices.Vertices;
            for (var i = 0; i < polyVertices.Length; i++)
            {
                var vertex = polyVertices[i];
                var vertexEvent = new VertexEvent(vertex);
                _vertexEventQueue.Enqueue(vertexEvent, vertex.X);
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
