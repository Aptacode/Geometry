using System.Numerics;
using Aptacode.Geometry.Blazor.Utilities;
using Microsoft.AspNetCore.Components.Web;

namespace Aptacode.Geometry.Blazor.Extensions
{
    public static class MouseEventArgsExtensions
    {
        public static Vector2 FromScale(this MouseEventArgs args)
        {
            return new(
                (float) args.OffsetX / Scale.Value, (float) args.OffsetY / Scale.Value);
        }
    }
}