using System;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Primitives.Extensions;

namespace Aptacode.Geometry.Collision
{
    public class FineCollisionDetector : CollisionDetector
    {
        #region Point

        public override bool CollidesWith(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public override bool CollidesWith(Point p1, PolyLine p2)
        {
            return p2.LineSegments.Any(line => line.OnLineSegment(p1.Position));
        }

        public override bool CollidesWith(Point p1, Polygon p2)
        {
            var collision = false;
            var edges = p2.Edges;
            var point = p1.Position;

            foreach (var (a, b) in edges)
            {
                if ((a, b).OnLineSegment(point)) return true;

                if ((a.Y >= point.Y && b.Y <= point.Y ||
                     a.Y <= point.Y && b.Y >= point.Y) &&
                    point.X <= (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
                    collision = !collision;
            }


            return collision;
        }

        public override bool CollidesWith(Point p1, Ellipse p2)
        {
            var f1dist = (p2.Foci.Item1 - p1.Position).Length();
            var f2dist = (p2.Foci.Item2 - p1.Position).Length();
            if (p2.Radii.X > p2.Radii.Y) //X is the major axis
                return f1dist + f2dist <= 2 * p2.Radii.X;

            if (p2.Radii.Y > p2.Radii.X) //Y is the major axis
                return f1dist + f2dist <= 2 * p2.Radii.Y;

            return p2.BoundingCircle.Contains(p1.Position);
        }

        #endregion

        #region PolyLine

        public override bool CollidesWith(PolyLine p1, Point p2)
        {
            for (var i = 0; i < p1.LineSegments.Length; i++)
            {
                var edge = p1.LineSegments[i];
                if (edge.OnLineSegment(p2.Position)) return true;
            }

            return false;
        }

        public override bool CollidesWith(PolyLine p1, PolyLine p2)
        {
            for (var i = 0; i < p1.LineSegments.Length; i++)
            {
                var edge = p1.LineSegments[i];
                for (var j = 0; j < p2.LineSegments.Length; j++)
                {
                    var lineSegment = p2.LineSegments[j];
                    if (lineSegment.LineSegmentIntersection(edge)) return true;
                }
            }

            return false;
        }

        public override bool CollidesWith(PolyLine p1, Polygon p2)
        {
            for (var i = 0; i < p1.LineSegments.Length; i++)
            {
                var edge = p1.LineSegments[i];
                for (var j = 0; j < p2.Edges.Length; j++)
                {
                    var lineSegment = p2.Edges[j];
                    if (lineSegment.LineSegmentIntersection(edge)) return true;
                }
            }

            return false;
        }

        public override bool CollidesWith(PolyLine p1, Ellipse p2)
        {
            if (p2.Radii.X == p2.Radii.Y)
            {
                foreach (var (v1, v2) in p1.LineSegments)
                {
                    if (p2.BoundingCircle.Contains(v1) || p2.BoundingCircle.Contains(v2)) return true;

                    var dot = ((p2.Position.X - v1.X) * (v2.X - v1.X) + (p2.Position.Y - v1.Y) * (v2.Y - v1.Y)) /
                              (v2 - v1).LengthSquared();
                    var closestX = v1.X + dot * (v2.X - v1.X);
                    var closestY = v1.Y + dot * (v2.Y - v1.Y);
                    var closestPoint =
                        new Vector2(closestX,
                            closestY); //The point of intersection of a line from the center of the circle perpendicular to the line segment (possibly the ray) with the line segment (or ray).
                    if (!(v1, v2).OnLineSegment(closestPoint)
                    ) //Closest intersection point may be beyond the ends of the line segment.
                        continue;

                    if (p2.BoundingCircle.Contains(closestPoint)
                    ) //Closest intersection point is inside the circle means circle intersects the line.
                        return true;
                }
            }
            else
            {
                var stdform = p2.GetStandardForm();
                foreach (var (v1, v2) in p1.LineSegments)
                {
                    if (CollidesWith(p2, v1.ToPoint()) || CollidesWith(p2, v2.ToPoint())) return true;

                    var dx = v2.X - v1.X;
                    var dy = v2.Y - v1.Y;

                    var a = stdform.A * dx * dx + stdform.B * dx * dy + stdform.C * dy * dy;
                    var b = 2 * stdform.A * v1.X * dx + stdform.B * v1.X * dy + 2 * stdform.C * v1.Y * dy +
                            stdform.D * dx + stdform.E * dy;
                    var c = stdform.A * v1.X * v1.X + stdform.B * v1.X * v1.Y + stdform.C * v1.Y * v1.Y +
                            stdform.D * v1.X + stdform.E * v1.Y + stdform.F;

                    var det = b * b - 4 * a * c;
                    if (det >= 0
                    ) //There are solutions on the ray cast by the line segment, now to see if they are on the line segment
                    {
                        var t1 = (-b + Math.Sqrt(det)) / (2 * a);
                        var t2 = (-b - Math.Sqrt(det)) / (2 * a);
                        if (t1 >= 0 && t1 <= 1 || t2 >= 0 && t2 <= 1) return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region Polygon

        public override bool CollidesWith(Polygon p1, Point p2)
        {
            var collision = false;
            var edges = p1.Edges;
            var point = p2.Position;
            foreach (var (a, b) in edges)
            {
                if ((a, b).OnLineSegment(point)) return true;

                if ((a.Y >= point.Y && b.Y <= point.Y ||
                     a.Y <= point.Y && b.Y >= point.Y) &&
                    point.X <= (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
                    collision = !collision;
            }


            return collision;
        }


        public override bool CollidesWith(Polygon p1, PolyLine p2)
        {
            for (var i = 0; i < p1.Edges.Length; i++)
            {
                var edge = p1.Edges[i];
                for (var j = 0; j < p2.LineSegments.Length; j++)
                {
                    var lineSegment = p2.LineSegments[j];
                    if (lineSegment.LineSegmentIntersection(edge)) return true;
                }
            }

            return false;
        }

        public override bool CollidesWith(Polygon p1, Polygon p2)
        {
            for (var i = 0; i < p1.Edges.Length; i++)
            {
                var edge = p1.Edges[i];
                for (var j = 0; j < p2.Edges.Length; j++)
                {
                    var lineSegment = p2.Edges[j];
                    if (lineSegment.LineSegmentIntersection(edge)) return true;
                }
            }

            return false;
        }

        public override bool CollidesWith(Polygon p1, Ellipse p2)
        {
            if (p2.Radii.X == p2.Radii.Y)
            {
                foreach (var (v1, v2) in p1.Edges)
                {
                    if (p2.BoundingCircle.Contains(v1) || p2.BoundingCircle.Contains(v2)) return true;

                    var dot = ((p2.Position.X - v1.X) * (v2.X - v1.X) + (p2.Position.Y - v1.Y) * (v2.Y - v1.Y)) /
                              (v2 - v1).LengthSquared();
                    var closestX = v1.X + dot * (v2.X - v1.X);
                    var closestY = v1.Y + dot * (v2.Y - v1.Y);
                    var closestPoint =
                        new Vector2(closestX,
                            closestY); //The point of intersection of a line from the center of the circle perpendicular to the edge (possibly the ray) with the line segment (or ray).
                    if (!(v1, v2).OnLineSegment(closestPoint)
                    ) //Closest intersection point may be beyond the ends of the edge.
                        continue;

                    if (p2.BoundingCircle.Contains(closestPoint)
                    ) //Closest intersection point is inside the circle means circle intersects the edge.
                        return true;
                }
            }
            else
            {
                var stdform = p2.GetStandardForm();
                foreach (var (v1, v2) in p1.Edges)
                {
                    if (CollidesWith(p2, v1.ToPoint()) || CollidesWith(p2, v2.ToPoint())) return true;

                    var dx = v2.X - v1.X;
                    var dy = v2.Y - v1.Y;

                    var a = stdform.A * dx * dx + stdform.B * dx * dy + stdform.C * dy * dy;
                    var b = 2 * stdform.A * v1.X * dx + stdform.B * v1.X * dy + 2 * stdform.C * v1.Y * dy +
                            stdform.D * dx + stdform.E * dy;
                    var c = stdform.A * v1.X * v1.X + stdform.B * v1.X * v1.Y + stdform.C * v1.Y * v1.Y +
                            stdform.D * v1.X + stdform.E * v1.Y + stdform.F;

                    var det = b * b - 4 * a * c;
                    if (det >= 0
                    ) //There are solutions on the ray cast by the line segment, now to see if they are on the line segment
                    {
                        var t1 = (-b + Math.Sqrt(det)) / (2 * a);
                        var t2 = (-b - Math.Sqrt(det)) / (2 * a);
                        if (t1 >= 0 && t1 <= 1 || t2 >= 0 && t2 <= 1) return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region Ellipse

        public override bool CollidesWith(Ellipse p1, Point p2)
        {
            var f1dist = (p1.Foci.Item1 - p2.Position).Length();
            var f2dist = (p1.Foci.Item2 - p2.Position).Length();
            if (p1.Radii.X > p1.Radii.Y) //X is the major axis
                return f1dist + f2dist <= 2 * p1.Radii.X;

            if (p1.Radii.Y > p1.Radii.X) //Y is the major axis
                return f1dist + f2dist <= 2 * p1.Radii.Y;

            return p1.BoundingCircle.Contains(p2.Position);
        }

        public override bool CollidesWith(Ellipse p1, PolyLine p2)
        {
            if (p1.Radii.X == p1.Radii.Y) //The ellipse is a circle
            {
                foreach (var (v1, v2) in p2.LineSegments)
                {
                    if (p1.BoundingCircle.Contains(v1) || p1.BoundingCircle.Contains(v2)) return true;

                    var dot = ((p1.Position.X - v1.X) * (v2.X - v1.X) + (p1.Position.Y - v1.Y) * (v2.Y - v1.Y)) /
                              (v2 - v1).LengthSquared();
                    var closestX = v1.X + dot * (v2.X - v1.X);
                    var closestY = v1.Y + dot * (v2.Y - v1.Y);
                    var closestPoint =
                        new Vector2(closestX,
                            closestY); //The point of intersection of a line from the center of the circle perpendicular to the line segment (possibly the ray) with the line segment (or ray).
                    if (!(v1, v2).OnLineSegment(closestPoint)
                    ) //Closest intersection point may be beyond the ends of the line segment.
                        continue;

                    if (p1.BoundingCircle.Contains(closestPoint)
                    ) //Closest intersection point is inside the circle means circle intersects the line.
                        return true;
                }
            }
            else
            {
                var stdform = p1.GetStandardForm();
                foreach (var (v1, v2) in p2.LineSegments)
                {
                    if (CollidesWith(p1, v1.ToPoint()) || CollidesWith(p1, v2.ToPoint())) return true;

                    var dx = v2.X - v1.X;
                    var dy = v2.Y - v1.Y;


                    var a = stdform.A * dx * dx + stdform.B * dx * dy + stdform.C * dy * dy;
                    var b = 2 * stdform.A * v1.X * dx + stdform.B * v1.X * dy + 2 * stdform.C * v1.Y * dy +
                            stdform.D * dx + stdform.E * dy;
                    var c = stdform.A * v1.X * v1.X + stdform.B * v1.X * v1.Y + stdform.C * v1.Y * v1.Y +
                            stdform.D * v1.X + stdform.E * v1.Y + stdform.F;

                    var det = b * b - 4 * a * c;
                    if (det >= 0
                    ) //There are solutions on the ray cast by the line segment, now to see if they are on the line segment
                    {
                        var t1 = (-b + Math.Sqrt(det)) / (2 * a);
                        var t2 = (-b - Math.Sqrt(det)) / (2 * a);
                        if (t1 >= 0 && t1 <= 1 || t2 >= 0 && t2 <= 1) return true;
                    }
                }
            }


            return false;
        }

        public override bool CollidesWith(Ellipse p1, Polygon p2)
        {
            if (p1.Radii.X == p1.Radii.Y) //The ellipse is a circle
            {
                foreach (var (v1, v2) in p2.Edges)
                {
                    if (p1.BoundingCircle.Contains(v1) || p1.BoundingCircle.Contains(v2)) return true;

                    var dot = ((p1.Position.X - v1.X) * (v2.X - v1.X) + (p1.Position.Y - v1.Y) * (v2.Y - v1.Y)) /
                              (v2 - v1).LengthSquared();
                    var closestX = v1.X + dot * (v2.X - v1.X);
                    var closestY = v1.Y + dot * (v2.Y - v1.Y);
                    var closestPoint =
                        new Vector2(closestX,
                            closestY); //The point of intersection of a line from the center of the circle perpendicular to the edge (possibly the ray) with the line segment (or ray).
                    if (!(v1, v2).OnLineSegment(closestPoint)
                    ) //Closest intersection point may be beyond the ends of the edge.
                        continue;

                    if (p1.BoundingCircle.Contains(closestPoint)
                    ) //Closest intersection point is inside the circle means circle intersects the edge.
                        return true;
                }
            }
            else
            {
                var stdform = p1.GetStandardForm();
                foreach (var (v1, v2) in p2.Edges)
                {
                    if (CollidesWith(p1, v1.ToPoint()) || CollidesWith(p1, v2.ToPoint())) return true;

                    var dx = v2.X - v1.X;
                    var dy = v2.Y - v1.Y;

                    var a = stdform.A * dx * dx + stdform.B * dx * dy + stdform.C * dy * dy;
                    var b = 2 * stdform.A * v1.X * dx + stdform.B * v1.X * dy + stdform.B * v1.Y * dx +
                            2 * stdform.C * v1.Y * dy + stdform.D * dx + stdform.E * dy;
                    var c = stdform.A * v1.X * v1.X + stdform.B * v1.X * v1.Y + stdform.C * v1.Y * v1.Y +
                            stdform.D * v1.X + stdform.E * v1.Y + stdform.F;

                    var det = b * b - 4 * a * c;
                    if (det >= 0
                    ) //There are solutions on the ray cast by the line segment, now to see if they are on the line segment
                    {
                        var t1 = (-b + Math.Sqrt(det)) / (2 * a);
                        var t2 = (-b - Math.Sqrt(det)) / (2 * a);
                        if (t1 >= 0 && t1 <= 1 || t2 >= 0 && t2 <= 1) return true;
                    }
                }
            }


            return false;
        }

        public override bool CollidesWith(Ellipse p1, Ellipse p2)
        {
            if (p1.Radii.X == p1.Radii.Y && p2.Radii.X == p2.Radii.Y
            ) //Then both ellipses are actually circles and this is definitely(?) faster
            {
                var d = (p2.Position - p1.Position).Length();
                return d < p1.Radii.X + p2.Radii.X;
            }

            var f1 = p1.GetStandardForm();
            var f2 = p2.GetStandardForm();
            var (u0, u1, u2, u3, u4) = Ellipse.GetResultantPolynomial(f1.A, f1.B, f1.C, f1.D, f1.E, f1.F, f2.A, f2.B,
                f2.C, f2.D, f2.E, f2.F);

            if (Ellipse.QuarticHasRealRoots(u0, u1, u2, u3, u4)) return true;

            if (CollidesWith(p1, p2.Position.ToPoint()) || CollidesWith(p2, p1.Position.ToPoint())
            ) //This means one ellipse is contained within the other entirely
                return true;

            return false;
        }

        #endregion
    }
}