using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aptacode.Geometry.Collision;
using Aptacode.Geometry.Collision.Circles;
using Aptacode.Geometry.Vertices;

namespace Aptacode.Geometry.Primitives { }
//{
//    public record PrimitiveGroup : Primitive
//    {
//        private IEnumerable<Primitive> _primitives;
//        public IEnumerable<Primitive> Primitives => _primitives;


//        private IEnumerable<(Vector2 p1, Vector2 p2)> CalculateEdges()
//        {
//            var edges = Vertices.Zip(Vertices.Skip(1), (a, b) => (a, b)).ToList();
//            edges.Add((Vertices.Last(), Vertices.First()));
//            return edges;
//        }

//        private IEnumerable<(Vector2 p1, Vector2 p2)> _edges;
//        public IEnumerable<(Vector2 p1, Vector2 p2)> Edges => _edges ??= CalculateEdges();
        
//        private IEnumerable<Vector2> _vertices;
//        public IEnumerable<Vector2> Vertices => _vertices ??= GetVertices();

//        private IEnumerable<Vector2> GetVertices()
//        {
//            var vertices = new HashSet<Vector2>();
//            foreach(var vertex in Primitives.SelectMany(p => p.Vertices.Vertices))
//            {
//                vertices.Add(vertex);
//            }
//            return vertices;
//        }

//        public override Primitive Translate(Vector2 delta)
//        {
//            throw new NotImplementedException();
//        }

//        public override Primitive Rotate(float theta)
//        {
//            throw new NotImplementedException();
//        }

//        public override Primitive Scale(Vector2 delta)
//        {
//            throw new NotImplementedException();
//        }

//        public override Primitive Skew(Vector2 delta)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}