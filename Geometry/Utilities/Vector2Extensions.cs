﻿using System.Numerics;
using System.Runtime.CompilerServices;

namespace Aptacode.Geometry.Utilities;

public static class Vector2Extensions
{
    private static readonly Matrix3x2 RotationMatrix = new(0, -1, 1, 0, 0, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float VectorCross(this Vector2 a, Vector2 b)
    {
        return a.X * b.Y - a.Y * b.X;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Perp(this Vector2 a)
    {
        return Vector2.Transform(a, RotationMatrix);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float
        PerpDot(this Vector2 a,
            Vector2 b) //This is worse than the VectorCross method performance wise for the same result.
    {
        return Vector2.Dot(a.Perp(), b);
    }
}