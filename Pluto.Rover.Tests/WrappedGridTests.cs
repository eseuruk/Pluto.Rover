using NUnit.Framework;
using Pluto.Rover.Core;
using System;

namespace Pluto.Rover.Tests
{
    public class WrappedGridTests
    {
        [TestCase(-1, -1, ViewDirection.North)]
        [TestCase(-1, -1, ViewDirection.West)]
        [TestCase(-1, -1, ViewDirection.South)]
        [TestCase(-1, -1, ViewDirection.East)]
        [TestCase(100, 100, ViewDirection.North)]
        [TestCase(100, 100, ViewDirection.West)]
        [TestCase(100, 100, ViewDirection.South)]
        [TestCase(100, 100, ViewDirection.East)]
        public void MoveFromInvalidPosition(int currentX, int currentY, ViewDirection direction)
        {
            var grid = new WrappedGrid(100, 100);
            var currentPosition = new Coordinate(currentX, currentY);

            Assert.Throws<ArgumentOutOfRangeException>(() => grid.Move(currentPosition, direction, 1));
        }

        [TestCase(50, 50, ViewDirection.North, 1, 50, 51)]
        [TestCase(50, 50, ViewDirection.West, 1, 49, 50)]
        [TestCase(50, 50, ViewDirection.South, 1, 50, 49)]
        [TestCase(50, 50, ViewDirection.East, 1, 51, 50)]
        public void MoveForward(int currentX, int currentY, ViewDirection dirrection, int stepCount, int expectedX, int expectedY)
        {
            var grid = new WrappedGrid(100, 100);
            var currentPosition = new Coordinate(currentX, currentY);

            var newPosition = grid.Move(currentPosition, dirrection, stepCount);

            Assert.That(newPosition.X, Is.EqualTo(expectedX));
            Assert.That(newPosition.Y, Is.EqualTo(expectedY));
        }

        [TestCase(0, 99, ViewDirection.North, 1, 0, 0)]
        [TestCase(0, 0, ViewDirection.West, 1, 99, 0)]
        [TestCase(0, 0, ViewDirection.South, 1, 0, 99)]
        [TestCase(99, 0, ViewDirection.East, 1, 0, 0)]

        [TestCase(0, 99, ViewDirection.North, 101, 0, 0)]
        [TestCase(0, 0, ViewDirection.West, 101, 99, 0)]
        [TestCase(0, 0, ViewDirection.South, 101, 0, 99)]
        [TestCase(99, 0, ViewDirection.East, 101, 0, 0)]

        [TestCase(0, 99, ViewDirection.North, 201, 0, 0)]
        [TestCase(0, 0, ViewDirection.West, 201, 99, 0)]
        [TestCase(0, 0, ViewDirection.South, 201, 0, 99)]
        [TestCase(99, 0, ViewDirection.East, 201, 0, 0)]
        public void MoveForwardWithRotation(int currentX, int currentY, ViewDirection dirrection, int stepCount, int expectedX, int expectedY)
        {
            var grid = new WrappedGrid(100, 100);
            var currentPosition = new Coordinate(currentX, currentY);

            var newPosition = grid.Move(currentPosition, dirrection, stepCount);

            Assert.That(newPosition.X, Is.EqualTo(expectedX));
            Assert.That(newPosition.Y, Is.EqualTo(expectedY));
        }

        [TestCase(50, 50, ViewDirection.North, 1, 50, 49)]
        [TestCase(50, 50, ViewDirection.West, 1, 51, 50)]
        [TestCase(50, 50, ViewDirection.South, 1, 50, 51)]
        [TestCase(50, 50, ViewDirection.East, 1, 49, 50)]
        public void MoveBackwards(int currentX, int currentY, ViewDirection direction, int stepCount, int expectedX, int expectedY)
        {
            var grid = new WrappedGrid(100, 100);
            var currentPosition = new Coordinate(currentX, currentY);

            var newPosition = grid.Move(currentPosition, direction, -stepCount);

            Assert.That(newPosition.X, Is.EqualTo(expectedX));
            Assert.That(newPosition.Y, Is.EqualTo(expectedY));
        }

        [TestCase(0, 0, ViewDirection.North, 1, 0, 99)]
        [TestCase(99, 0, ViewDirection.West, 1, 0, 0)]
        [TestCase(0, 99, ViewDirection.South, 1, 0, 0)]
        [TestCase(0, 0, ViewDirection.East, 1, 99, 0)]

        [TestCase(0, 0, ViewDirection.North, 101, 0, 99)]
        [TestCase(99, 0, ViewDirection.West, 101, 0, 0)]
        [TestCase(0, 99, ViewDirection.South, 101, 0, 0)]
        [TestCase(0, 0, ViewDirection.East, 101, 99, 0)]

        [TestCase(0, 0, ViewDirection.North, 201, 0, 99)]
        [TestCase(99, 0, ViewDirection.West, 201, 0, 0)]
        [TestCase(0, 99, ViewDirection.South, 201, 0, 0)]
        [TestCase(0, 0, ViewDirection.East, 201, 99, 0)]
        public void MoveBackwardsWithRotation(int currentX, int currentY, ViewDirection direction, int stepCount, int expectedX, int expectedY)
        {
            var grid = new WrappedGrid(100, 100);
            var currentPosition = new Coordinate(currentX, currentY);

            var newPosition = grid.Move(currentPosition, direction, -stepCount);

            Assert.That(newPosition.X, Is.EqualTo(expectedX));
            Assert.That(newPosition.Y, Is.EqualTo(expectedY));
        }
    }
}