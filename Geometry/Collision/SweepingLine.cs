using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Aptacode.Geometry.Collision
{
    public class SweepingLine
    {

        private List<(Vector2, Vector2)> SLSp = new();
        private List<(Vector2, Vector2)> SLSq = new();

        private readonly SortedDictionary<float, List<Vector2>> vertexEventQueue = new(); //Sorted by X coord of the vertex that is the value of the first item of the tuple in the list.
        

        public bool RunSweepingLine(Polygon P, Polygon Q)
        {
            SLSp.Clear();
            SLSq.Clear();
            vertexEventQueue.Clear();


            PopulateVertexEventQueue(P, Q);
            var pVertices = P.Vertices.Vertices;
            var qVertices = Q.Vertices.Vertices;
            foreach(var vertexEvent in vertexEventQueue) //Sweep along the X axis
            {
                for(var i = 0; i < vertexEvent.Value.Count; i++) //For every vertex we hit along this sweepline (probably gunna be 1 at most usually in our case) we update the SweepLineStatus accordingly.
                {
                    var currentVertex = vertexEvent.Value[i];
                    var pIndex = Array.IndexOf(pVertices, currentVertex); //Returns =1 if it's not in the list. O(N).
                    var qIndex = Array.IndexOf(qVertices, currentVertex);

                    if (pIndex != -1)//only one of these should have values unless the polygons have a shared point.
                    {
                        var currentPVertex = pVertices[pIndex];
                        var nextPVertex = pVertices[(pIndex + 1) % pVertices.Length]; //remainder division to go through array in a cicular way, would mean PolyLines don't work so need to think about it.
                        var previousPVertex = pVertices[(pIndex - 1 + pVertices.Length) % pVertices.Length];
                        if(currentPVertex.X <= nextPVertex.X) //Add edge if the current Vertex is the start of the edge (Also includes vertical edges)
                        {
                            SLSp.Add((currentPVertex, nextPVertex));
                        }
                        SLSp.Remove((nextPVertex, currentPVertex)); //Remove the edge if this is the end point.
                        if(currentPVertex.X <= previousPVertex.X)
                        {
                            SLSp.Add((currentPVertex, previousPVertex));
                        }
                        SLSp.Remove((previousPVertex, currentPVertex));
                    }
                    if (qIndex != -1)//only one of these should have values unless the polygons have a shared point.
                    {
                        var currentQVertex = qVertices[qIndex];
                        var nextQVertex = qVertices[(qIndex + 1) % qVertices.Length];
                        var previousQVertex = qVertices[(qIndex - 1 + qVertices.Length) % qVertices.Length];
                        if(currentQVertex.X <= nextQVertex.X) //Add edge if the current Vertex is the start of the edge (Also includes vertical edges)
                        {
                            SLSq.Add((currentQVertex, nextQVertex));
                        }
                        SLSq.Remove((nextQVertex, currentQVertex)); //Remove the edge if this is the end point.
                        if(currentQVertex.X <= previousQVertex.X)
                        {
                            SLSq.Add((currentQVertex, previousQVertex));
                        }
                        SLSp.Remove((previousQVertex, currentQVertex));
                    }                       
                }

                if(SLSp.Count == 0 || SLSq.Count == 0)//No need to check intersection on empty lists.
                {
                    SLSp.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance); //Now remove the vertical edges.
                    SLSq.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance);
                    continue;
                }

                for (var i = 0; i < SLSp.Count; i++) //Now just check the relevant edges
                {
                    var pEdge = SLSp[i];
                    for (var j = 0; j < SLSq.Count; j++)
                    {
                        var qEdge = SLSq[j];
                        if(Helpers.newLineSegmentIntersection(pEdge, qEdge))
                        {
                            return true;
                        }
                    }
                }

                SLSp.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance); //Now remove the vertical edges.
                SLSq.RemoveAll(e => Math.Abs(e.Item1.X - e.Item2.X) < Constants.Tolerance);
                
                
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
                if (vertexEventQueue.TryGetValue(polyVertices[i].X, out List<Vector2> vertices))
                {
                    vertices.Add(polyVertices[i]);
                }
                else
                {
                    vertexEventQueue.Add(polyVertices[i].X, new List<Vector2>() { polyVertices[i] });
                }
            }
        }
    }

}
