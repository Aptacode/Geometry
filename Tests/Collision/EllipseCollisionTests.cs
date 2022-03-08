using System.Numerics;
using Aptacode.Geometry.Primitives;
using Aptacode.Geometry.Tests.Collision.PointCollisionTestData;
using Xunit;
using Xunit.Abstractions;

namespace Aptacode.Geometry.Tests.Collision;

public class EllipseCollisionTests
{
    private readonly ITestOutputHelper _output;

    public EllipseCollisionTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [ClassData(typeof(EllipseVector2CollisionTestDataGenerator2))]
    [ClassData(typeof(EllipseVector2CollisionTestDataGenerator3))]
    public void CircleVector2CollisionTests(Ellipse p1, int[,] collisionMatrix)
    {
        //Arrange
        //Act
        //Assert
        for (var i = 0; i < collisionMatrix.GetLength(0); i++)
        {
            for (var j = 0; j < collisionMatrix.GetLength(1); j++)
            {
                var position = new Vector2(j - 4, i - 4);
                var expectedCollisionResult = collisionMatrix[i, j] == 1;
                var actualCollisionResult = p1.CollidesWith(position);

                if (expectedCollisionResult == actualCollisionResult)
                {
                    _output.WriteLine($"({position.X},{position.Y}) collides 🗸");
                }
                else
                {
                    _output.WriteLine($"({position.X},{position.Y}) does not collide ✗");
                }
                Assert.Equal(expectedCollisionResult, actualCollisionResult);
            }
        }
    }
}