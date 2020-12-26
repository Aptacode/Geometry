using System.Numerics;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public static class Constants
    {
        public static int[] ToIntArray(this Vector2[] input)
        {
            var vertices = new int[input.Length * 2];
            var count = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var vertex = input[i];
                vertices[count++] = (int) vertex.X;
                vertices[count++] = (int) vertex.Y;
            }

            return vertices;
        }
    }
}