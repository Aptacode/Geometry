using Aptacode.Geometry.Primitives;
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
        //Some kind of PriorityQueue for the 'Sweep Line Status' (SLS) probably load in the vertices of both polygons ordered by
        //smallest x coord. Points that are vertically collinear will of course have the same priority, maybe we need to break ties by
        //y coord??

        //Vertex arrays already constructed in a clockwise manner which is good. Given a vertexArray V, the vertices adjacent to V[x]
        //are V[(x - 1) % v.Count()] & V[(x + 1) % v.Count()] (stops indexing problems too).




    }
}
