using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Aptacode.Geometry.Primitives.Extensions
{
    public static class PolyLineExtensions
    {
        public static PolyLine Add(this PolyLine polyLine, params Vector2[] points)
        {
            return new PolyLine(polyLine.Points.ToList().Concat(points));
        }
        
        public static PolyLine Add(this PolyLine polyLine, IEnumerable<Vector2> points)
        {
            return new PolyLine(polyLine.Points.ToList().Concat(points));
        }

        public static PolyLine Remove(this PolyLine polyLine, int index)
        {
            var points = polyLine.Points.ToList();
            points.RemoveAt(index);
            return new PolyLine(points);
        }

        public static PolyLine Join(this PolyLine p1, PolyLine p2) => new(p1.Points.Concat(p2.Points));
    }
}