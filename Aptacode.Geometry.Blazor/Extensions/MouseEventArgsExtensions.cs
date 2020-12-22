using System.Numerics;
using Microsoft.AspNetCore.Components.Web;

namespace Aptacode.Geometry.Blazor.Extensions
{
    public static class MouseEventArgsExtensions
    {
        public static Vector2 FromScale(this MouseEventArgs args) => new(
            (float) args.OffsetX / 1.5f, (float) args.OffsetY / 1.5f);
    }
}