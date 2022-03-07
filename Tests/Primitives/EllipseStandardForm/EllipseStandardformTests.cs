using Aptacode.Geometry.Primitives;
using System.Numerics;
using Xunit;

namespace Aptacode.Geometry.Tests.Primitives.EllipseStandardForm
{
    public class EllipseStandardformTests
    {
        [Theory]
        [ClassData(typeof(EllipseStandardFormTestDataGenerator))]
        public void TestStandardForm(Ellipse ellipse, float a, float b, float c, float d, float e, float f)
        {
            //Arrange
            //Act
            //Assert
            Assert.Equal(a, ellipse.StandardForm.A);
            Assert.Equal(b, ellipse.StandardForm.B);
            Assert.Equal(c, ellipse.StandardForm.C);
            Assert.Equal(d, ellipse.StandardForm.D);
            Assert.Equal(e, ellipse.StandardForm.E);
            Assert.Equal(f, ellipse.StandardForm.F);
        }
    }
}
