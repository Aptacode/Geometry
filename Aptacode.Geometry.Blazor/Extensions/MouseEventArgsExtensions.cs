using System.Numerics;
using Microsoft.AspNetCore.Components.Web;

namespace Aptacode.Geometry.Blazor.Extensions
{
    public static class MouseEventArgsExtensions
    {
        public static Vector2 FromScale(this MouseEventArgs args) => new(
            (float) args.OffsetX / 2.0f, (float) args.OffsetY / 2.0f);
    }
}