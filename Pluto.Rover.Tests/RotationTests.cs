using NUnit.Framework;
using Pluto.Rover.Core;
using System;

namespace Pluto.Rover.Tests
{
    public class RotationTests
    {
        [TestCase(ViewDirection.North, ExpectedResult = ViewDirection.West)]
        [TestCase(ViewDirection.West, ExpectedResult = ViewDirection.South)]
        [TestCase(ViewDirection.South, ExpectedResult = ViewDirection.East)]
        [TestCase(ViewDirection.East, ExpectedResult = ViewDirection.North)]
        public ViewDirection TakeLeft(ViewDirection currentDirection)
        {
            return currentDirection.TakeLeft();
        }

        [TestCase((ViewDirection)(-1))]
        [TestCase((ViewDirection)5)]
        public void TakeLeftFromUnsuportedDirrection(ViewDirection currentDirection)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => currentDirection.TakeLeft());
        }

        [TestCase(ViewDirection.North, ExpectedResult = ViewDirection.East)]
        [TestCase(ViewDirection.East, ExpectedResult = ViewDirection.South)]
        [TestCase(ViewDirection.South, ExpectedResult = ViewDirection.West)]
        [TestCase(ViewDirection.West, ExpectedResult = ViewDirection.North)]
        public ViewDirection TakeRight(ViewDirection currentDirection)
        {
            return currentDirection.TakeRight();
        }

        [TestCase((ViewDirection)(-1))]
        [TestCase((ViewDirection)5)]
        public void TakeRightFromUnsuportedDirrection(ViewDirection currentDirection)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => currentDirection.TakeLeft());
        }
    }
}