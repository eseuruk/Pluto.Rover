using NUnit.Framework;
using Pluto.Rover.Core;

namespace Pluto.Rover.Tests
{
    public class RoverMoveTests
    {
        [TestCase(50, 50, ViewDirection.North, 50, 51)]
        [TestCase(50, 50, ViewDirection.West, 49, 50)]
        [TestCase(50, 50, ViewDirection.South, 50, 49)]
        [TestCase(50, 50, ViewDirection.East, 51, 50)]
        public void MoveForward(int landingX, int landingY, ViewDirection direction, int expectedX, int expectedY)
        {
            var planet = CreatePlanetWithInfinitGrid();

            var rover = Core.Rover.Land(planet, new Coordinate(landingX, landingY), direction);
            bool result = rover.TryMoveForward();

            Assert.IsTrue(result, "Should report successful movement");

            Assert.That(rover.Position.X, Is.EqualTo(expectedX), "Should finish at X position");
            Assert.That(rover.Position.Y, Is.EqualTo(expectedY), "Should finish at Y position");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Direction should not change");
        }

        [TestCase(50, 50, ViewDirection.North, 50, 51)]
        [TestCase(50, 50, ViewDirection.West, 49, 50)]
        [TestCase(50, 50, ViewDirection.South, 50, 49)]
        [TestCase(50, 50, ViewDirection.East, 51, 50)]
        public void CanNotMoveForwardBecauseOfObstacle(int landingX, int landingY, ViewDirection direction, int expectedX, int expectedY)
        {
            var planet = CreatePlanetWithInfinitGrid();
            planet.AddObstacle(new Coordinate(expectedX, expectedY));

            var rover = Core.Rover.Land(planet, new Coordinate(landingX, landingY), direction);
            bool result = rover.TryMoveForward();

            Assert.IsFalse(result, "Should report failed movement");

            Assert.That(rover.Position.X, Is.EqualTo(landingX), "X position shold not change");
            Assert.That(rover.Position.Y, Is.EqualTo(landingY), "Y position shold not change");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Direction should not change");
        }


        [TestCase(50, 50, ViewDirection.North, 50, 49)]
        [TestCase(50, 50, ViewDirection.West, 51, 50)]
        [TestCase(50, 50, ViewDirection.South, 50, 51)]
        [TestCase(50, 50, ViewDirection.East, 49, 50)]
        public void MoveBackward(int landingX, int landingY, ViewDirection direction, int expectedX, int expectedY)
        {
            var planet = CreatePlanetWithInfinitGrid();

            var rover = Core.Rover.Land(planet, new Coordinate(landingX, landingY), direction);
            bool result = rover.TryMoveBackward();

            Assert.IsTrue(result, "Should report successful movement");

            Assert.That(rover.Position.X, Is.EqualTo(expectedX), "Should finish at X position");
            Assert.That(rover.Position.Y, Is.EqualTo(expectedY), "Should finish at Y position");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Direction should not change");
        }

        [TestCase(50, 50, ViewDirection.North, 50, 49)]
        [TestCase(50, 50, ViewDirection.West, 51, 50)]
        [TestCase(50, 50, ViewDirection.South, 50, 51)]
        [TestCase(50, 50, ViewDirection.East, 49, 50)]
        public void CanNotMoveBackwardBecauseOfObstacle(int landingX, int landingY, ViewDirection direction, int expectedX, int expectedY)
        {
            var planet = CreatePlanetWithInfinitGrid();
            planet.AddObstacle(new Coordinate(expectedX, expectedY));

            var rover = Core.Rover.Land(planet, new Coordinate(landingX, landingY), direction);
            bool result = rover.TryMoveBackward();

            Assert.IsFalse(result, "Should report failed movement");

            Assert.That(rover.Position.X, Is.EqualTo(landingX), "X position shold not change");
            Assert.That(rover.Position.Y, Is.EqualTo(landingY), "Y position shold not change");
            Assert.That(rover.Direction, Is.EqualTo(direction), "Direction should not change");
        }

        [TestCase(50, 50, ViewDirection.North, ViewDirection.West)]
        [TestCase(50, 50, ViewDirection.West, ViewDirection.South)]
        [TestCase(50, 50, ViewDirection.South, ViewDirection.East)]
        [TestCase(50, 50, ViewDirection.East, ViewDirection.North)]
        public void TakeLerft(int landingX, int landingY, ViewDirection direction, ViewDirection expectedDirection)
        {
            var planet = CreatePlanetWithInfinitGrid();
            var rover = Core.Rover.Land(planet, new Coordinate(landingX, landingY), direction);

            rover.TakeLeft();

            Assert.That(rover.Position.X, Is.EqualTo(landingX), "X position should not change");
            Assert.That(rover.Position.Y, Is.EqualTo(landingY), "Y position should not change");
            Assert.That(rover.Direction, Is.EqualTo(expectedDirection), "Should finish with direction");
        }

        [TestCase(50, 50, ViewDirection.North, ViewDirection.East)]
        [TestCase(50, 50, ViewDirection.East, ViewDirection.South)]
        [TestCase(50, 50, ViewDirection.South, ViewDirection.West)]
        [TestCase(50, 50, ViewDirection.West, ViewDirection.North)]
        public void TakeRight(int landingX, int landingY, ViewDirection direction, ViewDirection expectedDirection)
        {
            var planet = CreatePlanetWithInfinitGrid();
            var rover = Core.Rover.Land(planet, new Coordinate(landingX, landingY), direction);

            rover.TakeRight();

            Assert.That(rover.Position.X, Is.EqualTo(landingX), "X position should not change");
            Assert.That(rover.Position.Y, Is.EqualTo(landingY), "Y position should not change");
            Assert.That(rover.Direction, Is.EqualTo(expectedDirection), "Should finish with direction");
        }

        private Planet CreatePlanetWithInfinitGrid()
        {
            var coordinateSystem = new InfinitGrid();
            return new Planet(coordinateSystem);
        }
    }
}