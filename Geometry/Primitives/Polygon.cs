﻿using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives.Polygons;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Polygon : Primitive
    {
        #region Collision Detection
        public override BoundingRectangle MinimumBoundingRectangle()
        {
            return Vertices.ToBoundingRectangle();
        }
        public override bool CollidesWith(Vector2 p)
        {
            return Vector2CollisionDetector.CollidesWith(this, p);
        }
        public override bool CollidesWith(Point p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(Ellipse p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(PolyLine p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(Rectangle p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        public override bool CollidesWith(Polygon p)
        {
            return CollisionDetectorMethods.CollidesWith(this, p);
        }

        #endregion

        #region Construction

        public Polygon(VertexArray vertices) : base(vertices)
        {
        }

        public Polygon(VertexArray vertices, BoundingRectangle? boundingRectangle) :
            base(vertices, boundingRectangle)
        {
        }


        public static Polygon Create(params float[] points)
        {
            if (points.Length < 3)
            {
                return Zero;
            }

            var vertices = new Vector2[points.Length / 2];

            var pointIndex = 0;
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vector2(points[pointIndex++], points[pointIndex++]);
            }

            return new Polygon(VertexArray.Create(vertices));
        }

        public static readonly Polygon Zero = Create(Vector2.Zero, Vector2.Zero, Vector2.Zero);

        public static Polygon Create(Vector2 p1, Vector2 p2, Vector2 p3, params Vector2[] points)
        {
            return new(VertexArray.Create(p1, p2, p3, points));
        }

        public static Polygon Create(Vector2[] points)
        {
            return new(VertexArray.Create(points));
        }

        #endregion

        #region Properties

        private (Vector2 p1, Vector2 p2)[] CalculateEdges()
        {
            var edges = new (Vector2 p1, Vector2 p2)[Vertices.Length];
            for (var i = 0; i < Vertices.Length - 1; i++)
            {
                edges[i] = (Vertices[i], Vertices[i + 1]);
            }

            edges[Vertices.Length - 1] = (Vertices[^1], Vertices[0]);

            return edges;
        }

        private (Vector2 p1, Vector2 p2)[] _edges;
        public (Vector2 p1, Vector2 p2)[] Edges => _edges ??= CalculateEdges();

        #endregion

        #region Transformations

        public override Polygon Translate(Vector2 delta)
        {
            if (_edges != null)
            {
                for (var i = 0; i < _edges.Length; i++)
                {
                    var (p1, p2) = _edges[i];
                    _edges[i] = (p1 + delta, p2 + delta);
                }
            }

            base.Translate(delta);
            return this;
        }

        public override Polygon Scale(Vector2 delta)
        {
            _edges = null;
            base.Scale(delta);
            return this;
        }

        public virtual Polygon Rotate(float theta)
        {
            _edges = null;
            base.Rotate(theta);
            return this;
        }

        public virtual Polygon Rotate(Vector2 rotationCenter, float theta)
        {
            _edges = null;
            base.Rotate(rotationCenter, theta);
            return this;
        }

        public virtual Polygon Skew(Vector2 delta)
        {
            _edges = null;
            base.Skew(delta);
            return this;
        }

        #endregion
    }
}