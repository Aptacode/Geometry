using System.Threading.Tasks;
using Aptacode.Geometry.Primitives;
using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

namespace Aptacode.Geometry.Blazor.Components.ViewModels
{
    public static class PrimitiveRenderer
    {
        public static async Task Draw(this Primitive primitive, IContext2DWithoutGetters ctx)
        {
            await ctx.BeginPathAsync();

            switch (primitive)
            {
                case Ellipse ellipse:
                    await ctx.EllipseAsync((int) ellipse.Position.X, (int) ellipse.Position.Y, (int) ellipse.Radii.X,
                        (int) ellipse.Radii.Y, 0, 0, 360);
                    await ctx.FillAsync(FillRule.NonZero);
                    await ctx.StrokeAsync();
                    break;
                case Point point:
                    await ctx.EllipseAsync((int) point.Position.X, (int) point.Position.Y, 1, 1, 0, 0, 360);
                    await ctx.FillAsync(FillRule.NonZero);
                    await ctx.StrokeAsync();
                    break;
                case Polygon polygon:
                    await ctx.MoveToAsync(polygon.Vertices[0].X, polygon.Vertices[0].Y);
                    for (var i = 1; i < polygon.Vertices.Length; i++)
                    {
                        var vertex = polygon.Vertices[i];
                        await ctx.LineToAsync((int) vertex.X, (int) vertex.Y);
                    }

                    await ctx.ClosePathAsync();
                    await ctx.FillAsync(FillRule.NonZero);
                    await ctx.StrokeAsync();
                    break;
                case PolyLine polyline:
                    await ctx.MoveToAsync(polyline.Vertices[0].X, polyline.Vertices[0].Y);
                    for (var i = 1; i < polyline.Vertices.Length; i++)
                    {
                        var vertex = polyline.Vertices[i];
                        await ctx.LineToAsync((int) vertex.X, (int) vertex.Y);
                    }

                    await ctx.StrokeAsync();
                    break;
            }
        }
    }
}