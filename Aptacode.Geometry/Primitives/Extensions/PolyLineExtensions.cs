using System.Linq;
using System.Numerics;

namespace Aptacode.Geometry.Primitives.Extensions
{
    public static class PolyLineExtensions
    {
        public static PolyLine Add(this PolyLine polyLine, Vector2 p1)
        {
            var points = polyLine.Points.ToList();
            points.Add(p1);
            return new PolyLine(points);
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