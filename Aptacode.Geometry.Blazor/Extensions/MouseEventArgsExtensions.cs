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
                (int)(args.OffsetX / SceneScale.Value), (int)(args.OffsetY / SceneScale.Value));
        }
    }
}