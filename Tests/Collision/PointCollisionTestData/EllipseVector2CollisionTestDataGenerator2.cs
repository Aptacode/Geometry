﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PointCollisionTestData;

public class EllipseVector2CollisionTestDataGenerator2 : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Ellipse
        new object[] { Ellipse.Create(new Vector2(0,0), 4),
            new[,]
            {
                { 0, 0, 0, 0, 1, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 1, 0, 0, 0, 0 },
            }
        },
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