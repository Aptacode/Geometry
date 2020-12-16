using System;
using System.Numerics;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Primitives;

namespace Aptacode.Geometry.Collision
{
    public class FineCollisionDetector : CollisionDetector
    {
        #region Point

        public override bool CollidesWith(Point p1, Point p2) => p1.Equals(p2);

        public override bool CollidesWith(Point p1, PolyLine p2)
        {
            foreach (var (v1, v2) in p2.LineSegments)
            {
                if (!Helpers.OnLineSegment((v1, v2), p1.Position))
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public override bool CollidesWith(Point p1, Polygon p2)
        {
            var collision = false;
            var edges = p2.Edges;
            var point = p1.Position;
            foreach (var (a, b) in edges)
            {
                if (Math.Abs(b.Y - a.Y) < Constants.Tolerance && //It is a horizontal line
                    Math.Abs(point.Y - a.Y) < Constants.Tolerance && //And we are on it
                    (point.X >= Math.Min(a.X, b.X) && point.X <= Math.Max(a.X, b.X))) //Within the line segment
                {
                    return true;
                }
                if (Math.Abs(b.X - a.X) < Constants.Tolerance && //It is a horizontal line
                    Math.Abs(point.X - a.X) < Constants.Tolerance && //And we are on it
                    (point.Y >= Math.Min(a.Y, b.Y) && point.Y <= Math.Max(a.Y, b.Y))) //Within the line segment
                {
                    return true;
                }

                if ((a.Y >= point.Y && b.Y <= point.Y || 
                     a.Y <= point.Y && b.Y >= point.Y) &&
                    point.X <= (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
                {
                    collision = !collision;
                }
            }


            return collision;
        }

        public override bool CollidesWith(Point p1, Circle p2) => p2.BoundingCircle.Contains(p1.Position);

#endregion

                #region PolyLine

        public override bool CollidesWith(PolyLine p1, Point p2)
        {
            foreach (var (v1, v2) in p1.LineSegments)
            {
                if (!Helpers.OnLineSegment((v1, v2), p2.Position))
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public override bool CollidesWith(PolyLine p1, PolyLine p2)
        {
            foreach (var (v1, v2) in p1.LineSegments)
            {
                foreach (var (v3, v4) in p2.LineSegments)
                {
                    if (Helpers.LineSegmentIntersection((v1, v2), (v3, v4)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool CollidesWith(PolyLine p1, Polygon p2)
        {
            foreach (var (v1, v2) in p1.LineSegments)
            {
                foreach (var (v3, v4) in p2.Edges)
                {
                    if (Helpers.LineSegmentIntersection((v1, v2), (v3, v4)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool CollidesWith(PolyLine p1, Circle p2)
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
                if (!Helpers.OnLineSegment((v1, v2), closestPoint)
                ) //Closest intersection point may be beyond the ends of the line segment.
                {
                    return false;
                }

                if (p2.BoundingCircle.Contains(closestPoint)
                ) //Closest intersection point is inside the circle means circle intersects the line.
                {
                    return true;
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
                if (Math.Abs(b.Y - a.Y) < Constants.Tolerance &&
                    (a.X >= point.X && b.X <= point.X || a.X <= point.X && b.X >= point.X))
                {
                    collision = !collision;
                    break;
                }

                if ((a.Y >= point.Y && b.Y <= point.Y || a.Y <= point.Y && b.Y >= point.Y) &&
                    point.X < (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X)
                {
                    collision = !collision;
                }
            }


            return collision;
        }


        public override bool CollidesWith(Polygon p1, PolyLine p2)
        {
            foreach (var (v1, v2) in p1.Edges)
            {
                foreach (var (v3, v4) in p2.LineSegments)
                {
                    if (Helpers.LineSegmentIntersection((v1, v2), (v3, v4)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool CollidesWith(Polygon p1, Polygon p2)
        {
            foreach (var (v1, v2) in p1.Edges)
            {
                foreach (var (v3, v4) in p2.Edges)
                {
                    if (Helpers.LineSegmentIntersection((v1, v2), (v3, v4)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool CollidesWith(Polygon p1, Circle p2)
        {
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
                    if (!Helpers.OnLineSegment((v1, v2), closestPoint)
                    ) //Closest intersection point may be beyond the ends of the edge.
                    {
                        return false;
                    }

                    if (p2.BoundingCircle.Contains(closestPoint)
                    ) //Closest intersection point is inside the circle means circle intersects the edge.
                    {
                        return true;
                    }
                }

                return false;
            }
        }

                #endregion

                #region Circle

        public override bool CollidesWith(Circle p1, Point p2) => p1.BoundingCircle.Contains(p2.Position);

        public override bool CollidesWith(Circle p1, PolyLine p2)
        {
            foreach (var (v1, v2) in p2.LineSegments)
            {
                if (p1.BoundingCircle.Contains(v1) || p1.BoundingCircle.Contains(v2))
                {
                    return true;
                }

                var dot = ((p1.Position.X - v1.X) * (v2.X - v1.X) + (p1.Position.Y - v1.Y) * (v2.Y - v1.Y)) /
                          (v2 - v1).LengthSquared();
                var closestX = v1.X + dot * (v2.X - v1.X);
                var closestY = v1.Y + dot * (v2.Y - v1.Y);
                var closestPoint =
                    new Vector2(closestX,
                        closestY); //The point of intersection of a line from the center of the circle perpendicular to the line segment (possibly the ray) with the line segment (or ray).
                if (!Helpers.OnLineSegment((v1, v2), closestPoint)
                ) //Closest intersection point may be beyond the ends of the line segment.
                {
                    return false;
                }

                if (p1.BoundingCircle.Contains(closestPoint)
                ) //Closest intersection point is inside the circle means circle intersects the line.
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CollidesWith(Circle p1, Polygon p2)
        {
            foreach (var (v1, v2) in p2.Edges)
            {
                if (p1.BoundingCircle.Contains(v1) || p1.BoundingCircle.Contains(v2))
                {
                    return true;
                }

                var dot = ((p1.Position.X - v1.X) * (v2.X - v1.X) + (p1.Position.Y - v1.Y) * (v2.Y - v1.Y)) /
                          (v2 - v1).LengthSquared();
                var closestX = v1.X + dot * (v2.X - v1.X);
                var closestY = v1.Y + dot * (v2.Y - v1.Y);
                var closestPoint =
                    new Vector2(closestX,
                        closestY); //The point of intersection of a line from the center of the circle perpendicular to the edge (possibly the ray) with the line segment (or ray).
                if (!Helpers.OnLineSegment((v1, v2), closestPoint)
                ) //Closest intersection point may be beyond the ends of the edge.
                {
                    return false;
                }

                if (p1.BoundingCircle.Contains(closestPoint)
                ) //Closest intersection point is inside the circle means circle intersects the edge.
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CollidesWith(Circle p1, Circle p2)
        {
            var d = (p2.Position - p1.Position).Length();
            return d < p1.Radius + p2.Radius;
        }

                #endregion
    }
}