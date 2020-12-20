using System.Numerics;
using Aptacode.Geometry.Vertices;
using BenchmarkDotNet.Attributes;

namespace Aptacode.Geometry.Benchmarks
{
    public class TransformationBenchmarks
    {
        private readonly VertexArray _vertexArray1 = VertexArray.Create(new Vector2(0, 0));
        private readonly VertexArray _vertexArray4 = VertexArray.Create(
            new Vector2(0, 0), 
            new Vector2(0, 0),
            new Vector2(0, 0), 
            new Vector2(0, 0));
        
        private readonly VertexArray _vertexArray20 = VertexArray.Create(
            new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0),
            new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0),
            new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0),
            new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0));
        private readonly Vector2 _translation = new Vector2(10, 10);
        
        [Benchmark] 
        public VertexArray Translate1()
        {
            return _vertexArray1.Translate(_translation);
        }
        [Benchmark]
        public VertexArray Translate4()
        {
            return _vertexArray4.Translate(_translation);
        }
        [Benchmark]
        public VertexArray Translate20()
        {
            return _vertexArray20.Translate(_translation);
        }
    }
}