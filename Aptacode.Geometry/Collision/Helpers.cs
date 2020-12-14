using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Aptacode.Geometry.Collision
{
    public static class Helpers
    {
        public static bool OnLineSegment( (Vector2, Vector2) line, Vector2 point)
        {
            var d1 = (line.Item1 - point).Length();
            var d2 = (line.Item2 - point).Length();
            var lineLength = (line.Item2 - line.Item1).Length();
            var buffer = 0.1f; //useful so that you don't have to literally be right on the line.
            if (d1 + d2 >= lineLength - buffer && d1 + d2 <= lineLength + buffer)
            {
                return true;
            }
            return false;
        }
    }
}
