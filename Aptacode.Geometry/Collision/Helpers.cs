using System.Numerics;

namespace Aptacode.Geometry.Collision
{
    public static class Helpers
    {
        public static bool OnLineSegment((Vector2, Vector2) line, Vector2 point)
        {
            var (lineA, lineB) = line;

            var d1 = (lineA - point).Length();
            var d2 = (lineB - point).Length();
            var lineLength = (lineB - lineA).Length();
            const float buffer = 0.1f; //useful so that you don't have to literally be right on the line.
            return d1 + d2 >= lineLength - buffer && d1 + d2 <= lineLength + buffer;
        }

        public static bool LineSegmentIntersection((Vector2, Vector2) line1, (Vector2, Vector2) line2)
        {
            var (line1A, line1B) = line1;
            var (line2A, line2B) = line2;

            if (line1A == line2A || line1B == line2B || line1A == line2B || line1B == line1A
            ) //These lines has at least one of the same endpoints
            {
                return true;
            }

            var det = (line2B.Y - line2A.Y) * (line1B.X - line1A.X) - (line2B.X - line2A.X) * (line1B.Y - line2A.Y);
            if (det == 0) //These lines are parallel and do not intersect
            {
                return false;
            }

            var A = ((line2B.X - line2A.X) * (line1A.Y - line2A.Y) - (line2B.Y - line2A.Y) * (line1A.X - line2A.X)) /
                    det;
            var B = ((line1B.X - line1A.X) * (line1A.Y - line2A.Y) - (line1B.Y - line1A.Y) * (line1A.X - line2A.X)) /
                    det;

            return A >= 0 && A <= 1 && B >= 0 && B <= 1;

            //var xIntersect = l1.Item1.X + A * (l1.Item2.X - l1.Item1.X)
            //var yIntersect = l1.Item1.Y + B * (l1.Item2.Y - l1.Item1.Y)
        }
    }
}