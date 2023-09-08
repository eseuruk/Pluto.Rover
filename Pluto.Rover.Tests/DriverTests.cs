using Pluto.Rover.Core;
using Moq;
using NUnit.Framework;
using System;

namespace Pluto.Rover.Tests
{
    public class DriverTests
    {
        [TestCase("f")]
        [TestCase("b")]
        [TestCase("l")]
        [TestCase("r")]
        public void DoNotSupportLowercase(string commands)
        {
            var rover = CreateRoverMock(true, true);
            var driver = new Driver(rover.Object);

            Assert.Throws<InvalidOperationException>(() => driver.Move(commands));

            rover.Verify(r => r.TryMoveForward(), Times.Never);
            rover.Verify(r => r.TryMoveBackward(), Times.Never);
            rover.Verify(r => r.TakeLeft(), Times.Never);
            rover.Verify(r => r.TakeRight(), Times.Never);
        }

        [TestCase("", 0, 0, 0, 0)]

        [TestCase("F", 1, 0, 0, 0)]
        [TestCase("B", 0, 1, 0, 0)]
        [TestCase("L", 0, 0, 1, 0)]
        [TestCase("R", 0, 0, 0, 1)]

        [TestCase("FFFFFFFF", 8, 0, 0, 0)]
        [TestCase("BBBBBBBB", 0, 8, 0, 0)]
        [TestCase("LLLLLLLL", 0, 0, 8, 0)]
        [TestCase("RRRRRRRR", 0, 0, 0, 8)]

        [TestCase("FBLR", 1, 1, 1, 1)]
        [TestCase("BLRF", 1, 1, 1, 1)]
        [TestCase("LRFB", 1, 1, 1, 1)]
        [TestCase("RFBL", 1, 1, 1, 1)]


        [TestCase("FFFBBBLLLRRR", 3, 3, 3, 3)]
        [TestCase("FBLRFBLRFBLR", 3, 3, 3, 3)]
        public void Move(string commands, int stepsForward, int stepsBackward, int turnsLeft, int turnsRight)
        {
            var rover = CreateRoverMock(true, true);
            var driver = new Driver(rover.Object);

            driver.Move(commands);

            rover.Verify(r => r.TryMoveForward(), Times.Exactly(stepsForward));
            rover.Verify(r => r.TryMoveBackward(), Times.Exactly(stepsBackward));
            rover.Verify(r => r.TakeLeft(), Times.Exactly(turnsLeft));
            rover.Verify(r => r.TakeRight(), Times.Exactly(turnsRight));
        }

        [TestCase("FRLB", 1, 0, 0, 0)]
        [TestCase("RLBF", 1, 1, 1, 1)]
        [TestCase("RLBFRLB", 1, 1, 1, 1)]

        [TestCase("FFFRLB", 1, 0, 0, 0)]
        [TestCase("RLBFFF", 1, 1, 1, 1)]
        [TestCase("RLBFFFRLB", 1, 1, 1, 1)]

        [TestCase("FA", 1, 0, 0, 0)]
        [TestCase("FRLBA", 1, 0, 0, 0)]
        [TestCase("FRLBARLB", 1, 0, 0, 0)]
        public void MoveUntilObstacleAtFront(string commands, int stepsForward, int stepsBackward, int turnsLeft, int turnsRight)
        {
            var rover = CreateRoverMock(false, true);
            var driver = new Driver(rover.Object);

            driver.Move(commands);

            rover.Verify(r => r.TryMoveForward(), Times.Exactly(stepsForward));
            rover.Verify(r => r.TryMoveBackward(), Times.Exactly(stepsBackward));
            rover.Verify(r => r.TakeLeft(), Times.Exactly(turnsLeft));
            rover.Verify(r => r.TakeRight(), Times.Exactly(turnsRight));
        }

        [TestCase("BRLF", 0, 1, 0, 0)]
        [TestCase("RLFB", 1, 1, 1, 1)]
        [TestCase("RLFBRLF", 1, 1, 1, 1)]

        [TestCase("BBBRLF", 0, 1, 0, 0)]
        [TestCase("RLFBBB", 1, 1, 1, 1)]
        [TestCase("RLFBBBRLF", 1, 1, 1, 1)]

        [TestCase("BA", 0, 1, 0, 0)]
        [TestCase("BRLFA", 0, 1, 0, 0)]
        [TestCase("BRLFARLF", 0, 1, 0, 0)]
        public void MoveUntilObstacleAtBack(string commands, int stepsForward, int stepsBackward, int turnsLeft, int turnsRight)
        {
            var rover = CreateRoverMock(true, false);
            var driver = new Driver(rover.Object);

            driver.Move(commands);

            rover.Verify(r => r.TryMoveForward(), Times.Exactly(stepsForward));
            rover.Verify(r => r.TryMoveBackward(), Times.Exactly(stepsBackward));
            rover.Verify(r => r.TakeLeft(), Times.Exactly(turnsLeft));
            rover.Verify(r => r.TakeRight(), Times.Exactly(turnsRight));
        }


        [TestCase("A", 0, 0, 0, 0)]
        [TestCase("AQWE", 0, 0, 0, 0)]
        [TestCase("AFQBWLER", 0, 0, 0, 0)]
        [TestCase("AFBLR", 0, 0, 0, 0)]

        [TestCase("FBLRA", 1, 1, 1, 1)]
        [TestCase("FBALR", 1, 1, 0, 0)]
        [TestCase("FBALRQFR", 1, 1, 0, 0)]
        public void FailWithUnknownCommand(string commands, int stepsForward, int stepsBackward, int turnsLeft, int turnsRight)
        {
            var rover = CreateRoverMock(true, true);
            var driver = new Driver(rover.Object);

            Assert.Throws<InvalidOperationException>(() => driver.Move(commands));

            rover.Verify(r => r.TryMoveForward(), Times.Exactly(stepsForward));
            rover.Verify(r => r.TryMoveBackward(), Times.Exactly(stepsBackward));
            rover.Verify(r => r.TakeLeft(), Times.Exactly(turnsLeft));
            rover.Verify(r => r.TakeRight(), Times.Exactly(turnsRight));
        }

        private Mock<IRover> CreateRoverMock(bool allowMoveForwared, bool allowMoveBackward)
        {
            var rover = new Mock<IRover>();
            rover.Setup(r => r.TryMoveForward()).Returns(allowMoveForwared);
            rover.Setup(r => r.TryMoveBackward()).Returns(allowMoveBackward);

            return rover;
        }
    }
}