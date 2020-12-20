using System.Numerics;

namespace Aptacode.Geometry.Blazor.Utilities
{
    public static class Constants
    {
        public static float Scale = 10;

        public static Vector2 ToScale(this Vector2 input) => input * Scale;

        public static Vector2 FromScale(this Vector2 input) => input / Scale;

        public static float ToScale(this float input) => input * Scale;

        public static float FromScale(this float input) => input / Scale;

        public static Vector2[] ToScale(this Vector2[] input)
        {
            var vertices = new Vector2[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                vertices[i] = input[i] * Scale;
            }

            return vertices;
        }
    }
}