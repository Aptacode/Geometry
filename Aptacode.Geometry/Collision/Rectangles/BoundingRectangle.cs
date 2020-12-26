using System.Numerics;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Collision.Rectangles
{
    public readonly struct BoundingRectangle
    {
        public readonly Vector2 TopLeft;
        public readonly Vector2 TopRight;
        public readonly Vector2 BottomRight;
        public readonly Vector2 BottomLeft;
        public readonly Vector2 Size;

        public BoundingRectangle(Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
            Size = BottomRight - TopLeft;
        }

        public static BoundingRectangle FromTwoPoints(Vector2 topLeft, Vector2 bottomRight)
        {
            var topRight = new Vector2(bottomRight.X, topLeft.Y);
            var bottomLeft = new Vector2(topLeft.X, bottomRight.Y);

            return new BoundingRectangle(topLeft, topRight, bottomRight, bottomLeft);
        }

        public static BoundingRectangle FromVertexArray(VertexArray vertexArray)
        {
            var maxX = 0.0f;
            var maxY = 0.0f;
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            for (var i = 0; i < vertexArray.Length; i++)
            {
                var vertex = vertexArray[i];
                if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }

                if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }

                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }

                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }
            }

            return FromTwoPoints(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }
    }
}