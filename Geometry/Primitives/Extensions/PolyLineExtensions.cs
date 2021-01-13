using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives.Extensions
{
    public static class PolyLineExtensions
    {
        public static PolyLine Add(this PolyLine polyLine, params Vector2[] points)
        {
            return new(polyLine.Vertices.Concat(points));
        }

        public static PolyLine Add(this PolyLine polyLine, IEnumerable<Vector2> points)
        {
            return new(polyLine.Vertices.Concat(points.ToArray()));
        }

        public static PolyLine Remove(this PolyLine polyLine, int index)
        {
            return new(polyLine.Vertices.Remove(index));
        }

        public static PolyLine Concat(this PolyLine p1, PolyLine p2)
        {
            return new(p1.Vertices.Concat(p2.Vertices));
        }
    }
}