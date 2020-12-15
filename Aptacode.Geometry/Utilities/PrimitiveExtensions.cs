using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Utilities
{
    public static class PrimitiveExtensions
    {
        public static (Vector2 p1, Vector2 p2) FurthestPoints(this Primitive primitive)
        {
            //Todo Implement Planar case
            //https://en.wikipedia.org/wiki/Closest_pair_of_points_problem
            var maxDistance = 0.0f;
            var p1 = Vector2.Zero;
            var p2 = Vector2.Zero;
            for (var i = 0; i < primitive.Vertices.Count() - 1; i++)
            {
                for (var j = i + 1; j < primitive.Vertices.Count(); j++)
                {
                    var p = primitive.Vertices.ElementAt(i);
                    var q = primitive.Vertices.ElementAt(j);
                    var length = (p - q).Length();
                    if (!(length > maxDistance))
                    {
                        continue;
                    }

                    maxDistance = length;
                    p1 = p;
                    p2 = q;
                }
            }

            return (p1, p2);
        }
    }
}