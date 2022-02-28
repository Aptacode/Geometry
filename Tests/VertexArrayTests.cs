using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Aptacode.Geometry.Vertices;
using Xunit;

namespace Aptacode.Geometry.Tests
{
    public class VertexArrayTests
    {
        public class VertexArrayEqualityTestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new()
            {
                //v1, v2, isEqual

                //Equal
                new object[] { VertexArray.Create(Vector2.Zero), VertexArray.Create(Vector2.Zero), true },
                new object[] { VertexArray.Create(Vector2.Zero, Vector2.One), VertexArray.Create(Vector2.Zero, Vector2.One), true },

                //Not Equal
                new object[] { VertexArray.Create(Vector2.Zero), VertexArray.Create(Vector2.One), false },
                new object[] { VertexArray.Create(Vector2.Zero, Vector2.One), VertexArray.Create(Vector2.Zero), false },
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
        
        public class VertexArrayConcatTestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new()
            {
                //Concat
                //v1.Concat(v2) == v3
                new object[] { VertexArray.Create(Vector2.Zero), VertexArray.Create(Vector2.Zero), VertexArray.Create(Vector2.Zero) },
                new object[] { VertexArray.Create(Vector2.Zero), VertexArray.Create(Vector2.One), VertexArray.Create(Vector2.Zero, Vector2.One) },
                new object[] { VertexArray.Create(Vector2.Zero), VertexArray.Create(), VertexArray.Create(Vector2.Zero) },
                new object[] { VertexArray.Create(), VertexArray.Create(), VertexArray.Create() },
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
        public class VertexArrayTranslateTestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new()
            {
                //Concat
                //v1.Translate(delta) == expected
                new object[] { VertexArray.Create(Vector2.Zero), Vector2.Zero, VertexArray.Create(Vector2.Zero) },
                new object[] { VertexArray.Create(Vector2.Zero, Vector2.One), Vector2.One, VertexArray.Create(Vector2.One, new Vector2(2,2)) },
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

        [Theory]
        [ClassData(typeof(VertexArrayEqualityTestDataGenerator))]
        public void VertexEquality(VertexArray v1, VertexArray v2, bool areEqual)
        {
            //Arrange
            //Act
            var equalityOperatorResult = v1 == v2;
            var inequalityOperatorResult = v1 != v2;
            var equalsMethodResult = v1.Equals(v2);
            var equalsObjectMethodResult = v1.Equals((object)v2);

            //Assert
            Assert.Equal(areEqual, equalityOperatorResult);
            Assert.Equal(!areEqual, inequalityOperatorResult);
            Assert.Equal(areEqual, equalsMethodResult);
            Assert.Equal(areEqual, equalsObjectMethodResult);
        }

        [Theory]
        [ClassData(typeof(VertexArrayConcatTestDataGenerator))]
        public void VertexArray_Concat(VertexArray v1, VertexArray v2, VertexArray expected)
        {
            //Arrange
            //Act
            var sut = v1.Concat(v2);

            //Assert
            Assert.Equal(expected, sut);
        }

        [Theory]
        [ClassData(typeof(VertexArrayTranslateTestDataGenerator))]
        public void VertexArray_Translate(VertexArray v1, Vector2 delta, VertexArray expected)
        {
            //Arrange
            //Act
            v1.Translate(delta);

            //Assert
            Assert.Equal(expected, v1);
        }
    }
}
