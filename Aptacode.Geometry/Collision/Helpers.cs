using System.Numerics;

namespace Aptacode.Geometry.Collision
{
    public static class Helpers
    {
        public static bool OnLineSegment((Vector2, Vector2) line, Vector2 point)
        {
            var d1 = (line.Item1 - point).Length();
            var d2 = (line.Item2 - point).Length();
            var lineLength = (line.Item2 - line.Item1).Length();
            const float buffer = 0.1f; //useful so that you don't have to literally be right on the line.
            return d1 + d2 >= lineLength - buffer && d1 + d2 <= lineLength + buffer;
        }

        public static bool LineSegmentIntersection((Vector2, Vector2) l1, (Vector2, Vector2) l2)
        {
            if((l1.Item1 ==  l2.Item1 || l1.Item2 == l2.Item2) || l1.Item1 == l2.Item2 || l1.Item2 == l1.Item1) //These lines has at least one of the same endpoints
            {
                return true;
            }
            var det = (l2.Item2.Y - l2.Item1.Y) * (l1.Item2.X - l1.Item1.X) - (l2.Item2.X - l2.Item1.X) * (l1.Item2.Y - l2.Item1.Y);
            if(det == 0) //These lines are parallel and do not intersect
            {
                return false;
            }

            var A = ((l2.Item2.X - l2.Item1.X) * (l1.Item1.Y - l2.Item1.Y) - (l2.Item2.Y - l2.Item1.Y) * (l1.Item1.X - l2.Item1.X)) / det;
            var B = ((l1.Item2.X - l1.Item1.X) * (l1.Item1.Y - l2.Item1.Y) - (l1.Item2.Y - l1.Item1.Y) * (l1.Item1.X - l2.Item1.X)) / det;

            if(A >= 0 && A <=1 && B >= 0 && B <= 1)
            {
                return true;
            }

            //var xIntersect = l1.Item1.X + A * (l1.Item2.X - l1.Item1.X)
            //var yIntersect = l1.Item1.Y + B * (l1.Item2.Y - l1.Item1.Y)

            return false;
        }
    }
}