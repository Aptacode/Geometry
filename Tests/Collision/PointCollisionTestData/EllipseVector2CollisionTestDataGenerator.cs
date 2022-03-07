﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Tests.Collision.PointCollisionTestData;

public class EllipseVector2CollisionTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        //Ellipse
        new object[] { Ellipse.Create(0, 0, 10, 10, 0), Vector2.One, true },
        new object[] { Ellipse.Create(0, 0, 10, 10, 0), new Vector2(20, 20), false },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(0,0), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(0,1), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(1,1), false },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(0,1), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(1,-1), false },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(0,-1), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(-1,-1), false },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(-1,0), true },
        new object[] { Ellipse.Create(Vector2.Zero, 1), new Vector2(-1,1), false },
        new object[] { Ellipse.Create(0, 0, 10, 10, 100), Vector2.One, true },
        new object[] { Ellipse.Create(0, 0, 10, 10, -100), Vector2.One, true },
        new object[] { Ellipse.Create(0, 0, 10, 10, MathF.PI), Vector2.One, true },
        new object[] { Ellipse.Create(10, 0, 10, 10, MathF.PI), new Vector2(20, 0), true },
        new object[] { Ellipse.Create(0, 0, 10, 20, (MathF.PI)/2), new Vector2(20, 0), true },
        new object[] { Ellipse.Create(20, 0, 20, 10, 0), new Vector2(40, 0), true },
        new object[] { Ellipse.Create(20, 0, 20, 10, 0), new Vector2(20, 10), true },
        new object[] { Ellipse.Create(20, 0, 20, 10, 0), new Vector2(-20, 0), false },
        new object[] { Ellipse.Create(20, 0, 20, 10, (MathF.PI)/2), new Vector2(20, 0), true },
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