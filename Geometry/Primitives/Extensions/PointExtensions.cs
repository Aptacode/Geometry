using System.Numerics;
using System.Runtime.CompilerServices;

namespace Aptacode.Geometry.Primitives.Extensions;

public static class PointExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point ToPoint(this Vector2 position)
    {
        return Point.Create(position);
    }
}