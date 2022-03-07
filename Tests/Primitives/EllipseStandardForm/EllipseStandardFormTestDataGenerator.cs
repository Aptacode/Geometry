using System.Collections;
using System.Collections.Generic;

namespace Aptacode.Geometry.Tests.Primitives.EllipseStandardForm 
{ 
    public class EllipseStandardFormTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
            new object[] { Geometry.Primitives.Ellipse.Create(System.Numerics.Vector2.Zero, 1), 1, 0, 1, 0, 0, -1 },
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
}