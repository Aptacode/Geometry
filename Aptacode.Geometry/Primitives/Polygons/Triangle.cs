using System.Numerics;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public class Triangle : Polygon
    {
        #region Construction

        public static Triangle Create(Vector2 p1, Vector2 p2, Vector2 p3) => new Triangle(p1, p2, p3);

        public Triangle(Vector2 p1, Vector2 p2, Vector2 p3) : base(p1, p2, p3)
        {

        }

        #endregion

        #region Properties

        #endregion
    }
}