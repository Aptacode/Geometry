using Aptacode.Geometry.Primitives;
using Xunit;

namespace Aptacode.Geometry.Tests.Primitives.EllipseStandardForm;

public class EllipseStandardformTests
{
    [Theory]
    [ClassData(typeof(EllipseStandardFormTestDataGenerator))]
    public void TestStandardForm(Ellipse ellipse, float a, float b, float c, float d, float e, float f)
    {
        //Arrange
        //Act
        //Assert

        var ratio = ellipse.StandardForm.A / a;
        Assert.Equal(a * ratio, ellipse.StandardForm.A, 6);
        Assert.Equal(b * ratio, ellipse.StandardForm.B, 6);
        Assert.Equal(c * ratio, ellipse.StandardForm.C, 6);
        Assert.Equal(d * ratio, ellipse.StandardForm.D, 6);
        Assert.Equal(e * ratio, ellipse.StandardForm.E, 6);
        Assert.Equal(f * ratio, ellipse.StandardForm.F, 6);
    }
}