using System.Numerics;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public record Triangle(Vector2 P1, Vector2 P2, Vector2 P3) : Polygon(new Vector2[]{ P1, P2, P3 })
    {
        #region Construction

        public static Triangle Create(Vector2 p1, Vector2 p2, Vector2 p3) => new(p1, p2, p3);

        #endregion

        #region Properties

        #endregion
    }
}