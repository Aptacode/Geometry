﻿using System.Numerics;

namespace Aptacode.Geometry.Primitives.Polygons
{
    public class Rectangle : Polygon
    {
        #region Construction

        public static Rectangle Create(Vector2 position, Vector2 size)
        {
            return new Rectangle(position,
                new Vector2(position.X + size.X, position.Y + size.Y),
                position + size,
                new Vector2(position.X, position.Y + size.Y));
        }


        protected Rectangle(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight) : base(topLeft, topRight, bottomLeft, bottomRight) { }

        #endregion
    }
}