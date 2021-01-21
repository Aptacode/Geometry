using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Collision.Rectangles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;
using Aptacode.Geometry.Primitives.Polygons;
using Aptacode.Geometry.Utilities;

namespace Aptacode.Geometry.Collision
{
    public static class CollisionDetectorMethods
    {
        #region Point

        public static bool CollidesWith(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public static bool CollidesWith(Point p1, PolyLine p2)
        {
            for (var i = 0; i < p2.LineSegments.Length; i++)
            {
                if (p2.LineSegments[i].OnLineSegment(p1.Position))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CollidesWith(Point p1, Polygon p2)
        {
            var collision = false;
            var vertices = p2.Vertices.Vertices;
            var point = p1.Position;

            for (int i = 0, j = vertices.Length - 1; i < vertices.Length; j = i++)
            {
                var a = vertices[i];
                var b = vertices[j];
                if ((a, b).OnLineSegment(point))
                {
                    return true;
                }

                if (a.Y > point.Y != b.Y > point.Y &&
                    point.X < (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
                {
                    collision = !collision;
                }
            }

            return collision;
        }

        public static bool CollidesWith(Point p1, Ellipse p2)
        {
            var f1dist = (p2.Foci.Item1 - p1.Position).Length();
            var f2dist = (p2.Foci.Item2 - p1.Position).Length();
            if (p2.Radii.X > p2.Radii.Y) //X is the major axis
            {
                return f1dist + f2dist <= 2 * p2.Radii.X;
            }

            if (p2.Radii.Y > p2.Radii.X) //Y is the major axis
            {
                return f1dist + f2dist <= 2 * p2.Radii.Y;
            }

            return p2.BoundingCircle.Contains(p1.Position);
        }

        #endregion

        #region PolyLine

        public static bool CollidesWith(PolyLine p1, PolyLine p2)
        {
            for (var i = 0; i < p1.LineSegments.Length; i++)
            {
                var edge = p1.LineSegments[i];
                for (var j = 0; j < p2.LineSegments.Length; j++)
                {
                    var lineSegment = p2.LineSegments[j];
                    if (lineSegment.LineSegmentIntersection(edge))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CollidesWith(PolyLine p1, Polygon p2)
        {
            for (var i = 0; i < p1.LineSegments.Length; i++)
            {
                var edge = p1.LineSegments[i];
                for (var j = 0; j < p2.Edges.Length; j++)
                {
                    var lineSegment = p2.Edges[j];
                    if (lineSegment.LineSegmentIntersection(edge))
                    {
                        return true;
                    }
                }
            }

            if (p2.CollidesWith(p1.Vertices[0])) //If they don't intersect but a point of the polyline is inside the polygon then that polygon must contain the polyline
            {
                return true;
            }

            return false;
        }

        public static bool CollidesWith(PolyLine p1, Ellipse p2)
        {
            if (p2.Radii.X == p2.Radii.Y)
            {
                foreach (var (v1, v2) in p1.LineSegments)
                {
                    if (p2.BoundingCircle.Contains(v1) || p2.BoundingCircle.Contains(v2))
                    {
                        return true;
                    }

                    var dot = ((p2.Position.X - v1.X) * (v2.X - v1.X) + (p2.Position.Y - v1.Y) * (v2.Y - v1.Y)) /
                              (v2 - v1).LengthSquared();
                    var closestX = v1.X + dot * (v2.X - v1.X);
                    var closestY = v1.Y + dot * (v2.Y - v1.Y);
                    var closestPoint =
                        new Vector2(closestX,
                            closestY); //The point of intersection of a line from the center of the circle perpendicular to the line segment (possibly the ray) with the line segment (or ray).
                    if (!(v1, v2).OnLineSegment(closestPoint)
                    ) //Closest intersection point may be beyond the ends of the line segment.
                    {
                        continue;
                    }

                    if (p2.BoundingCircle.Contains(closestPoint)
                    ) //Closest intersection point is inside the circle means circle intersects the line.
                    {
                        return true;
                    }
                }
            }
            else
            {
                var stdform = p2.GetStandardForm();
                foreach (var (v1, v2) in p1.LineSegments)
                {
                    if (p2.CollidesWith(v1) || p2.CollidesWith(v2))
                    {
                        return true;
                    }

                    return Helpers.LineSegmentEllipseIntersection((v1, v2), stdform);
                }
            }

            return false;
        }



        #endregion

        #region Polygon

        public static bool CollidesWith(Polygon p1, Polygon p2)
        {
            for (var i = 0; i < p1.Edges.Length; i++)
            {
                var edge = p1.Edges[i];
                for (var j = 0; j < p2.Edges.Length; j++)
                {
                    var lineSegment = p2.Edges[j];
                    if (lineSegment.LineSegmentIntersection(edge))
                    {
                        return true;
                    }
                }
            }

            if (p2.CollidesWith(p1.Vertices[0]) || p1.CollidesWith(p2.Vertices[0])) //If they don't intersect but one point is inside the other one then that polygon must contain the other.
            {
                return true;
            }

            return false;
        }

        public static bool CollidesWith(Polygon p1, Ellipse p2)
        {
            if (p2.Radii.X == p2.Radii.Y)
            {
                foreach (var (v1, v2) in p1.Edges)
                {
                    if (p2.BoundingCircle.Contains(v1) || p2.BoundingCircle.Contains(v2))
                    {
                        return true;
                    }

                    var dot = ((p2.Position.X - v1.X) * (v2.X - v1.X) + (p2.Position.Y - v1.Y) * (v2.Y - v1.Y)) /
                              (v2 - v1).LengthSquared();
                    var closestX = v1.X + dot * (v2.X - v1.X);
                    var closestY = v1.Y + dot * (v2.Y - v1.Y);
                    var closestPoint =
                        new Vector2(closestX,
                            closestY); //The point of intersection of a line from the center of the circle perpendicular to the edge (possibly the ray) with the line segment (or ray).
                    if (!(v1, v2).OnLineSegment(closestPoint)
                    ) //Closest intersection point may be beyond the ends of the edge.
                    {
                        continue;
                    }

                    if (p2.BoundingCircle.Contains(closestPoint)
                    ) //Closest intersection point is inside the circle means circle intersects the edge.
                    {
                        return true;
                    }
                }
            }
            else
            {
                if(p1.CollidesWith(p2.Position)) //This checks containment
                {
                    return true;
                }
                var stdform = p2.GetStandardForm();
                foreach (var (v1, v2) in p1.Edges)
                {
                    if (p2.CollidesWith(v1) || p2.CollidesWith(v2))
                    {
                        return true;
                    }

                    Helpers.LineSegmentEllipseIntersection((v1, v2), stdform);
                }
            }

            return false;
        }


        #endregion

        #region Ellipse

        public static bool CollidesWith(Ellipse p1, Ellipse p2)
        {
            if (p1.Radii.X == p1.Radii.Y && p2.Radii.X == p2.Radii.Y
            ) //Then both ellipses are actually circles and  is definitely(?) faster
            {
                var d = (p2.Position - p1.Position).Length();
                return d < p1.Radii.X + p2.Radii.X;
            }

            var f1 = p1.GetStandardForm();
            var f2 = p2.GetStandardForm();
            var (u0, u1, u2, u3, u4) = Ellipse.GetResultantPolynomial(f1.A, f1.B, f1.C, f1.D, f1.E, f1.F, f2.A, f2.B,
                f2.C, f2.D, f2.E, f2.F);

            if (Ellipse.QuarticHasRealRoots(u0, u1, u2, u3, u4))
            {
                return true;
            }

            if (p1.CollidesWith(p2) || p2.CollidesWith(p1)
            ) //This means one ellipse is contained within the other entirely
            {
                return true;
            }

            return false;
        }



        #endregion
    }
}