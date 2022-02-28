﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.CollisionTests;

public class EllipseBoundingRectangleCollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Ellipse
        new object[]
        {
            Ellipse.Create(0, 0, 1, 1, 0), BoundingRectangle.FromPositionAndSize(Vector2.Zero, Vector2.One), true
        },
        new object[]
        {
            Ellipse.Create(0, 0, 1, 1, 0), BoundingRectangle.FromPositionAndSize(new Vector2(2, 2), Vector2.One), false
        }
    };

    public IEnumerator<object[]> GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}