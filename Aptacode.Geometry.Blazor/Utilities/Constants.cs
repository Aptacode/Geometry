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
    }
}