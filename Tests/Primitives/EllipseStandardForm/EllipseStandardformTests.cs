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
            var ratio = ellipse.StandardForm.A/a; 
            Assert.Equal(a*ratio, ellipse.StandardForm.A);
            Assert.Equal(b*ratio, ellipse.StandardForm.B);
            Assert.Equal(c*ratio, ellipse.StandardForm.C);
            Assert.Equal(d*ratio, ellipse.StandardForm.D);
            Assert.Equal(e*ratio, ellipse.StandardForm.E);
            Assert.Equal(f*ratio, ellipse.StandardForm.F);
        }
    }
}
