namespace Pluto.Rover.Tests;

public class RoverLandingTests
{
    [TestCase(-10, -20, ViewDirection.North)]
    [TestCase(-10, -20, ViewDirection.West)]
    [TestCase(-10, -20, ViewDirection.South)]
    [TestCase(-10, -20, ViewDirection.East)]

    [TestCase(0, 0, ViewDirection.North)]
    [TestCase(0, 0, ViewDirection.West)]
    [TestCase(0, 0, ViewDirection.South)]
    [TestCase(0, 0, ViewDirection.East)]

    [TestCase(10, 20, ViewDirection.North)]
    [TestCase(10, 20, ViewDirection.West)]
    [TestCase(10, 20, ViewDirection.South)]
    [TestCase(10, 20, ViewDirection.East)]
    public void SuccessfulLanding(int landingX, int landingY, ViewDirection direction)
    {
        var landingPosition = new Coordinate(landingX, landingY);
        var planet = CreatePlanetWithInfinitGrid();
    
        var rover = Core.Rover.Land(planet, landingPosition, direction);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position, Is.EqualTo(landingPosition), "Should land at position");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Should land with direction");
        });
    }

    [TestCase(ViewDirection.North)]
    [TestCase(ViewDirection.West)]
    [TestCase(ViewDirection.South)]
    [TestCase(ViewDirection.East)]
    public void FailedLandingAtInvalidPostion(ViewDirection direction)
    {
        var invalidPosition = new Coordinate(10, 20);

        var planet = CreatePlanetWithInvaliddGrid();

        Assert.Throws<ArgumentOutOfRangeException>(() => Core.Rover.Land(planet, invalidPosition, direction));
    }

    [TestCase(ViewDirection.North)]
    [TestCase(ViewDirection.West)]
    [TestCase(ViewDirection.South)]
    [TestCase(ViewDirection.East)]
    public void FailedLandingAtObstacle(ViewDirection direction)
    {
        var landingPosition = new Coordinate(10, 20);

        var planet = CreatePlanetWithInfinitGrid();
        planet.AddObstacle(landingPosition);

        Assert.Throws<ArgumentOutOfRangeException>(() => Core.Rover.Land(planet, landingPosition, direction));
    }

    private Planet CreatePlanetWithInfinitGrid()
    {
        var coordinateSystem = new InfinitGrid();
        return new Planet(coordinateSystem);
    }

    private Planet CreatePlanetWithInvaliddGrid()
    {
        var coordinateSystem = new InvaliddGrid();
        return new Planet(coordinateSystem);
    }
}