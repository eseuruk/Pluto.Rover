namespace Pluto.Rover.Simulations;

public class MoveSimulations
{
    [TestCase(ViewDirection.North)]
    [TestCase(ViewDirection.West)]
    [TestCase(ViewDirection.South)]
    [TestCase(ViewDirection.East)]
    public void MoveNowhere(ViewDirection direction)
    {
        var landingPosition = new Coordinate(50, 50);

        var planet = SimulationContext.CreatePlanet();
        var rover = Core.Rover.Land(planet, landingPosition, direction);
        var driver = new Driver(rover);

        driver.Move("");

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position, Is.EqualTo(landingPosition), "Position should not change");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Direction should not change");
        });
    }

    [TestCase(ViewDirection.North, "L", ViewDirection.West)]
    [TestCase(ViewDirection.North, "LL", ViewDirection.South)]
    [TestCase(ViewDirection.North, "LLL", ViewDirection.East)]
    [TestCase(ViewDirection.North, "LLLL", ViewDirection.North)]
    [TestCase(ViewDirection.North, "LLLLL", ViewDirection.West)]
    [TestCase(ViewDirection.North, "LLLLLL", ViewDirection.South)]
    [TestCase(ViewDirection.North, "LLLLLLL", ViewDirection.East)]
    [TestCase(ViewDirection.North, "LLLLLLLL", ViewDirection.North)]

    [TestCase(ViewDirection.North, "R", ViewDirection.East)]
    [TestCase(ViewDirection.North, "RR", ViewDirection.South)]
    [TestCase(ViewDirection.North, "RRR", ViewDirection.West)]
    [TestCase(ViewDirection.North, "RRRR", ViewDirection.North)]
    [TestCase(ViewDirection.North, "RRRRR", ViewDirection.East)]
    [TestCase(ViewDirection.North, "RRRRRR", ViewDirection.South)]
    [TestCase(ViewDirection.North, "RRRRRRR", ViewDirection.West)]
    [TestCase(ViewDirection.North, "RRRRRRRR", ViewDirection.North)]

    [TestCase(ViewDirection.North, "LR", ViewDirection.North)]
    [TestCase(ViewDirection.North, "LLRR", ViewDirection.North)]
    [TestCase(ViewDirection.North, "LLLRRR", ViewDirection.North)]
    [TestCase(ViewDirection.North, "LLLLRRRR", ViewDirection.North)]

    [TestCase(ViewDirection.North, "RL", ViewDirection.North)]
    [TestCase(ViewDirection.North, "RRLL", ViewDirection.North)]
    [TestCase(ViewDirection.North, "RRRLLL", ViewDirection.North)]
    [TestCase(ViewDirection.North, "RRRRLLLL", ViewDirection.North)]
    public void Rotate(ViewDirection direction, string commands, ViewDirection expectedDirection)
    {
        var landingPosition = new Coordinate(50, 50);

        var planet = SimulationContext.CreatePlanet();
        var rover = Core.Rover.Land(planet, landingPosition, direction);
        var driver = new Driver(rover);

        driver.Move(commands);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position, Is.EqualTo(landingPosition), "Position should not change");
            Assert.That(rover.Direction, Is.EqualTo(expectedDirection), "Should finish with direction");
        });
    }


    [TestCase(50, 50, ViewDirection.North, 5, 50, 55)]
    [TestCase(50, 50, ViewDirection.West, 5, 45, 50)]
    [TestCase(50, 50, ViewDirection.South, 5, 50, 45)]
    [TestCase(50, 50, ViewDirection.East, 5, 55, 50)]

    public void MoveForward(int landingX, int landingY, ViewDirection direction, int stepsCount, int expectedX, int expectedY)
    {
        var landingPosition = new Coordinate(landingX, landingY);
        var expectedPosition = new Coordinate(expectedX, expectedY);
        var commands = new string('F', stepsCount);

        var planet = SimulationContext.CreatePlanet();
        var rover = Core.Rover.Land(planet, landingPosition, direction);
        var driver = new Driver(rover);

        driver.Move(commands);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position, Is.EqualTo(expectedPosition), "Should finish at position");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Direction should not change");
        });
    }

    [TestCase(50, 50, ViewDirection.North, 5, 50, 45)]
    [TestCase(50, 50, ViewDirection.West, 5, 55, 50)]
    [TestCase(50, 50, ViewDirection.South, 5, 50, 55)]
    [TestCase(50, 50, ViewDirection.East, 5, 45, 50)]

    public void MoveBackward(int landingX, int landingY, ViewDirection direction, int stepsCount, int expectedX, int expectedY)
    {
        var landingPosition = new Coordinate(landingX, landingY);
        var expectedPosition = new Coordinate(expectedX, expectedY);
        var commands = new string('B', stepsCount);

        var planet = SimulationContext.CreatePlanet();
        var rover = Core.Rover.Land(planet, landingPosition, direction);
        var driver = new Driver(rover);

        driver.Move(commands);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position, Is.EqualTo(expectedPosition), "Should finish at position");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Direction should not change");
        });
    }

    [TestCase(50, 50, ViewDirection.North)]
    [TestCase(50, 50, ViewDirection.West)]
    [TestCase(50, 50, ViewDirection.South)]
    [TestCase(50, 50, ViewDirection.East)]

    public void MoveFullCircleLeft(int landingX, int landingY, ViewDirection direction)
    {
        var landingPosition = new Coordinate(landingX, landingY);
        var commands = "FFFFFLFFFFFLFFFFFLFFFFFL";

        var planet = SimulationContext.CreatePlanet();
        var rover = Core.Rover.Land(planet, landingPosition, direction);
        var driver = new Driver(rover);

        driver.Move(commands);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position, Is.EqualTo(landingPosition), "Should finish at position");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Should finish with direction");
        });
    }

    [TestCase(50, 50, ViewDirection.North)]
    [TestCase(50, 50, ViewDirection.West)]
    [TestCase(50, 50, ViewDirection.South)]
    [TestCase(50, 50, ViewDirection.East)]

    public void MoveFullCircleRight(int landingX, int landingY, ViewDirection direction)
    {
        var landingPosition = new Coordinate(landingX, landingY);
        var commands = "FFFFFRFFFFFRFFFFFRFFFFFR";

        var planet = SimulationContext.CreatePlanet();
        var rover = Core.Rover.Land(planet, landingPosition, direction);
        var driver = new Driver(rover);

        driver.Move(commands);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position, Is.EqualTo(landingPosition), "Should finish at position");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Should finish with direction");
        });
    }


    [TestCase(0, 0, ViewDirection.North, "FFRFF", 2, 2, ViewDirection.East)]
    [TestCase(50, 50, ViewDirection.North, "BLFFFRRFBFRFFFFFFLLFR", 48, 44, ViewDirection.East)]

    public void FreeMove(int landingX, int landingY, ViewDirection direction, string commands, int expectedX, int expectedY, ViewDirection expectedDirection)
    {
        var landingPosition = new Coordinate(landingX, landingY);
        var expectedPosition = new Coordinate(expectedX, expectedY);

        var planet = SimulationContext.CreatePlanet();
        var rover = Core.Rover.Land(planet, landingPosition, direction);
        var driver = new Driver(rover);

        driver.Move(commands);

        Assert.Multiple(() =>
        {
            Assert.That(rover.Position, Is.EqualTo(expectedPosition), "Should finish at position");
            Assert.That(rover.Direction, Is.EqualTo(expectedDirection), "Should finish with direction");
        });
    }
}