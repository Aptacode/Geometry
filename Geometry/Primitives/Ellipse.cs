using System;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives.Polygons;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives
{
    public record Ellipse : Primitive
    {
        public readonly float Rotation;
        public Vector2 Radii; //The x width(height) and the y height(width)
        public Vector2 Position => Vertices[0];
        public (Vector2, Vector2) Foci => GetFoci();
        public VertexArray EllipseVertices => GetEllipseVertices();
        public VertexArray EllipseExtrema => GetEllipseExtrema();

        protected BoundingCircle? _boundingCircle;

        public BoundingCircle BoundingCircle =>
            _boundingCircle ?? (_boundingCircle = this.MinimumBoundingCircle()).Value;

        #region IEquatable

        public virtual bool Equals(Ellipse other)
        {
            if (other is null)
            {
                return false;
            }

            var delta = Position - other.Position;
            var radiusDelta = Radii - other.Radii;
            return Math.Abs(delta.X + delta.Y) < Constants.Tolerance &&
                   Math.Abs(radiusDelta.X + radiusDelta.Y) < Constants.Tolerance;
        }

        #endregion

        private VertexArray GetEllipseExtrema()
        {
            var asquared = Radii.X * Radii.X;
            var bsquared = Radii.Y * Radii.Y;

            var costheta = Math.Cos(Rotation);
            var costhetasquared = costheta * costheta;

            var sintheta = Math.Sin(Rotation);
            var sinthetasquared = sintheta * sintheta;

            var xdelta = (float) Math.Sqrt(asquared * costhetasquared + bsquared * sinthetasquared);
            var ydelta = (float) Math.Sqrt(asquared * sinthetasquared + bsquared * costhetasquared);

            var topLeft = Position - new Vector2(xdelta, ydelta);
            var topRight = Position + new Vector2(xdelta, -ydelta);
            var bottomLeft = Position + new Vector2(-xdelta, ydelta);
            var bottomRight = Position + new Vector2(xdelta, ydelta);
            return new VertexArray(new[] {topLeft, topRight, bottomRight, bottomLeft});
        }

        private VertexArray GetEllipseVertices()
        {
            var a = Vector2.Transform(new Vector2(Radii.X, 0.0f), Matrix3x2.CreateRotation(Rotation));
            var b = Vector2.Transform(new Vector2(0.0f, Radii.Y), Matrix3x2.CreateRotation(Rotation));

            return new VertexArray(new[] {Position - a, Position + a, Position - b, Position + b});
        }

        private (Vector2, Vector2) GetFoci()
        {
            if (Radii.X > Radii.Y)
            {
                var c = Vector2.Transform(new Vector2((float) Math.Sqrt(Radii.X * Radii.X - Radii.Y * Radii.Y), 0.0f),
                    Matrix3x2.CreateRotation(Rotation));
                var f1 = Position - c;
                var f2 = Position + c;
                return (f1, f2);
            }

            if (Radii.X < Radii.Y)
            {
                var c = Vector2.Transform(new Vector2(0.0f, (float) Math.Sqrt(Radii.Y * Radii.Y - Radii.X * Radii.X)),
                    Matrix3x2.CreateRotation(Rotation));
                var f1 = Position - c;
                var f2 = Position + c;
                return (f1, f2);
            }

            return (Position, Position);
        }

        #region Collision Detection

        public override BoundingRectangle MinimumBoundingRectangle()
        {
            return EllipseExtrema.ToBoundingRectangle();
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

        public static (double u0, double u1, double u2, double u3, double u4) GetResultantPolynomial(double A1,
            double B1, double C1, double D1, double E1, double F1, double A2, double B2, double C2, double D2,
            double E2, double F2) //Takes the coefficients of 2 ellipses (conics) and returns the Bezout determinant R(y) = u0 + u1y + u2y^2 + u3y^3 + u4y^4, is is the equation for the solutions for the y points of intersection, they may be complex.
        {
            var v0 = A1 * B2 - A2 * B1;
            var v1 = A1 * C2 - A2 * C1;
            var v2 = A1 * D2 - A2 * D1;
            var v3 = A1 * E2 - A2 * E1;
            var v4 = A1 * F2 - A2 * F1;
            var v5 = B1 * C2 - B2 * C1;
            var v6 = B1 * E2 - B2 * E1;
            var v7 = B1 * F2 - B2 * F1;
            var v8 = C1 * D2 - C2 * D1;
            var v9 = D1 * E2 - D2 * E1;
            var v10 = D1 * F2 - D2 * F1;

            var u0 = v2 * v10 - v4 * v4;
            var u1 = v0 * v10 + v2 * (v7 + v9) - 2 * v3 * v4;
            var u2 = v0 * (v7 + v9) + v2 * (v6 - v8) - v3 * v3 - 2 * v1 * v4;
            var u3 = v0 * (v6 - v8) + v2 * v5 - 2 * v1 * v3;
            var u4 = v0 * v5 - v1 * v1;

            return (u0, u1, u2, u3, u4);
        }

        public static bool QuarticHasRealRoots(double u0, double u1, double u2, double u3, double u4)
        {
            if (Math.Abs(u4) < Constants.Tolerance && Math.Abs(u3) > Constants.Tolerance)
            {
                return true;
            }

            if (Math.Abs(u4) < Constants.Tolerance && Math.Abs(u3) < Constants.Tolerance && u2 != 0)
            {
                var det = u1 * u1 - 4 * u2 * u0;
                if (Math.Abs(det) < Constants.Tolerance)
                {
                    return true;
                }

                return det >= 0;
            }

            var delta = 256 * u4 * u4 * u4 * u0 * u0 * u0
                        - 192 * u4 * u4 * u3 * u1 * u0 * u0
                        - 128 * u4 * u4 * u2 * u2 * u0 * u0
                        + 144 * u4 * u4 * u2 * u1 * u1 * u0
                        - 27 * u4 * u4 * u1 * u1 * u1 * u1
                        + 144 * u4 * u3 * u3 * u1 * u0 * u0
                        - 6 * u4 * u3 * u3 * u1 * u1 * u0
                        - 80 * u4 * u3 * u2 * u2 * u1 * u0
                        + 18 * u4 * u3 * u2 * u1 * u1 * u1
                        + 16 * u4 * u2 * u2 * u2 * u2 * u0
                        - 4 * u4 * u2 * u2 * u2 * u1 * u1
                        - 27 * u3 * u3 * u3 * u3 * u0 * u0
                        + 18 * u3 * u3 * u3 * u2 * u1 * u0
                        - 4 * u3 * u3 * u3 * u1 * u1 * u1
                        - 4 * u3 * u3 * u2 * u2 * u2 * u0
                        + u3 * u3 * u2 * u2 * u1 * u1;

            if (delta < 0 && Math.Abs(delta) > 0.01f)
            {
                return true;
            }

            var p = 8 * u4 * u2 - 3 * u3 * u3;
            var d = 64 * u4 * u4 * u4 * u0
                    - 16 * u4 * u4 * u2 * u2
                    + 16 * u4 * u3 * u3 * u2
                    - 16 * u4 * u4 * u3 * u1
                    - 3 * u3 * u3 * u3 * u3;

            if (p > 0 && Math.Abs(p) > Constants.Tolerance || d > 0)
            {
                return false;
            }

            return true;
        }

        #region Construction

        public Ellipse(Vector2 position, Vector2 radii, float rotation) : base(VertexArray.Create(position))
        {
            Radii = radii;
            Rotation = rotation;
        }

        protected Ellipse(VertexArray vertexArray, Vector2 radii, float rotation) : base(vertexArray)
        {
            Radii = radii;
            Rotation = rotation;
        }

        public static Ellipse Create(float x, float y, float a, float b, float rotation)
        {
            return new(new Vector2(x, y), new Vector2(a, b), rotation);
        }

        public Ellipse(Vector2 position, Vector2 radii, float rotation,
            BoundingRectangle? boundingRectangle) : base(VertexArray.Create(position), boundingRectangle)
        {
            Radii = radii;
            Rotation = rotation;
        }

        public static readonly Ellipse Zero = new(Vector2.Zero, Vector2.Zero, 0.0f);
        public static readonly Ellipse Unit = new(Vector2.Zero, Vector2.One, 0.0f); //This is a circle

        #endregion
    }
}