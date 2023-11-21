namespace Pluto.Rover.Simulations;

public class LandingSimulations
{
    [TestCase(0, 0, ViewDirection.North)]
    [TestCase(0, 0, ViewDirection.West)]
    [TestCase(0, 0, ViewDirection.South)]
    [TestCase(0, 0, ViewDirection.East)]

    [TestCase(0, 99, ViewDirection.North)]
    [TestCase(0, 99, ViewDirection.West)]
    [TestCase(0, 99, ViewDirection.South)]
    [TestCase(0, 99, ViewDirection.East)]

    [TestCase(50, 50, ViewDirection.North)]
    [TestCase(50, 50, ViewDirection.West)]
    [TestCase(50, 50, ViewDirection.South)]
    [TestCase(50, 50, ViewDirection.East)]

    [TestCase(99, 0, ViewDirection.North)]
    [TestCase(99, 0, ViewDirection.West)]
    [TestCase(99, 0, ViewDirection.South)]
    [TestCase(99, 0, ViewDirection.East)]

    [TestCase(99, 99, ViewDirection.North)]
    [TestCase(99, 99, ViewDirection.West)]
    [TestCase(99, 99, ViewDirection.South)]
    [TestCase(99, 99, ViewDirection.East)]
    public void SuccessfulLanding(int landingX, int landingY, ViewDirection direction)
    {
        var planet = SimulationContext.CreatePlanet();

        var rover = Core.Rover.Land(planet, new Coordinate(landingX, landingY), direction);

        Assert.That(rover.Position.X, Is.EqualTo(landingX), "Should land at X position");
        Assert.That(rover.Position.Y, Is.EqualTo(landingY), "Should land at Y position");
        Assert.That(rover.Direction, Is.EqualTo(direction), "Should land with direction");
    }

    [TestCase(-1, -1, ViewDirection.North)]
    [TestCase(-1, -1, ViewDirection.West)]
    [TestCase(-1, -1, ViewDirection.South)]
    [TestCase(-1, -1, ViewDirection.East)]

    [TestCase(-1, 0, ViewDirection.North)]
    [TestCase(-1, 0, ViewDirection.West)]
    [TestCase(-1, 0, ViewDirection.South)]
    [TestCase(-1, 0, ViewDirection.East)]

    [TestCase(0, -1, ViewDirection.North)]
    [TestCase(0, -1, ViewDirection.West)]
    [TestCase(0, -1, ViewDirection.South)]
    [TestCase(0, -1, ViewDirection.East)]

    [TestCase(100, 100, ViewDirection.North)]
    [TestCase(100, 100, ViewDirection.West)]
    [TestCase(100, 100, ViewDirection.South)]
    [TestCase(100, 100, ViewDirection.East)]

    [TestCase(100, 99, ViewDirection.North)]
    [TestCase(100, 99, ViewDirection.West)]
    [TestCase(100, 99, ViewDirection.South)]
    [TestCase(100, 99, ViewDirection.East)]

    [TestCase(99, 100, ViewDirection.North)]
    [TestCase(99, 100, ViewDirection.West)]
    [TestCase(99, 100, ViewDirection.South)]
    [TestCase(99, 100, ViewDirection.East)]

    public void FailedLandingAtInvalidPostion(int landingX, int landingY, ViewDirection direction)
    {
        var planet = SimulationContext.CreatePlanet();

        Assert.Throws<ArgumentOutOfRangeException>(() => Core.Rover.Land(planet, new Coordinate(landingX, landingY), direction));
    }

    [TestCase(ViewDirection.North)]
    [TestCase(ViewDirection.West)]
    [TestCase(ViewDirection.South)]
    [TestCase(ViewDirection.East)]
    public void FailedLandingAtObstacle(ViewDirection direction)
    {
        var landingPosition = new Coordinate(50, 50);

        var planet = SimulationContext.CreatePlanet();
        planet.AddObstacle(landingPosition);

        Assert.Throws<ArgumentOutOfRangeException>(() => Core.Rover.Land(planet, landingPosition, direction));
    }
}