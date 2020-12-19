using System.Numerics;
using Microsoft.AspNetCore.Components.Web;

namespace Aptacode.Geometry.Blazor.Extensions
{
    public static class MouseEventArgsExtensions
    {
        public static Vector2 FromScale(this MouseEventArgs args) => new(
            (float) args.OffsetX / Utilities.Constants.Scale, (float) args.OffsetY / Utilities.Constants.Scale);
    }
}