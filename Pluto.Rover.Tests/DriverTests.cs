namespace Pluto.Rover.Tests;

public class DriverTests
{
    [TestCase("f")]
    [TestCase("b")]
    [TestCase("l")]
    [TestCase("r")]
    public void DoNotSupportLowercase(string invalidCommands)
    {
        var rover = new TestRover(true, true);
        var driver = new Driver(rover);

        Assert.Multiple(() =>
        {
            Assert.Throws<InvalidOperationException>(() => driver.Move(invalidCommands));
            Assert.That(rover.GetExecutedCommands(), Is.EqualTo(""), "No commands should be executed");
        });
    }

    public void NotMoveOnEmptyComandList()
    {
        var rover = new TestRover(true, true);
        var driver = new Driver(rover);

        driver.Move("");

        Assert.That(rover.GetExecutedCommands(), Is.EqualTo(""), "No commands should be executed");
    }

    [TestCase("F")]
    [TestCase("B")]
    [TestCase("L")]
    [TestCase("R")]

    [TestCase("FFFFFFFF")]
    [TestCase("BBBBBBBB")]
    [TestCase("LLLLLLLL")]
    [TestCase("RRRRRRRR")]

    [TestCase("FBLR")]
    [TestCase("BLRF")]
    [TestCase("LRFB")]
    [TestCase("RFBL")]

    [TestCase("FFFBBBLLLRRR")]
    [TestCase("FBLRFBLRFBLR")]
    public void MoveWithoutObstacles(string requestedCommands)
    {
        var rover = new TestRover(true, true);
        var driver = new Driver(rover);

        driver.Move(requestedCommands);

        Assert.Multiple(() =>
        {
            Assert.That(rover.GetExecutedCommands(), Is.EqualTo(requestedCommands), "All command should be executed");
            Assert.That(rover.GetBlockedCommands(), Is.EqualTo(""), "No commands should be blocked");
        });
    }

    [TestCase("FRLB", "")]
    [TestCase("RLBF", "RLB")]
    [TestCase("RLBFRLB", "RLB")]

    [TestCase("FFFRLB", "")]
    [TestCase("RLBFFF", "RLB")]
    [TestCase("RLBFFFRLB", "RLB")]

    public void MoveUntilObstacleAtFront(string requestedCommands, string executedCommands)
    {
        var rover = new TestRover(false, true);
        var driver = new Driver(rover);

        driver.Move(requestedCommands);

        Assert.Multiple(() =>
        {
            Assert.That(rover.GetExecutedCommands(), Is.EqualTo(executedCommands), "Only commands from the list should be executed");
            Assert.That(rover.GetBlockedCommands(), Is.EqualTo("F"), "Should stop at first F");
        });
    }

    [TestCase("BRLF", "")]
    [TestCase("RLFB", "RLF")]
    [TestCase("RLFBRLF", "RLF")]

    [TestCase("BBBRLF", "")]
    [TestCase("RLFBBB", "RLF")]
    [TestCase("RLFBBBRLF", "RLF")]

    public void MoveUntilObstacleAtBack(string requestedCommands, string executedCommands)
    {
        var rover = new TestRover(true, false);
        var driver = new Driver(rover);

        driver.Move(requestedCommands);

        Assert.Multiple(() =>
        {
            Assert.That(rover.GetExecutedCommands(), Is.EqualTo(executedCommands), "Only commands from the list should be executed");
            Assert.That(rover.GetBlockedCommands(), Is.EqualTo("B"), "Should stop at first B");
        });
    }

    [TestCase("A", "")]
    [TestCase("AQWE", "")]
    [TestCase("AFQBWLER", "")]
    [TestCase("AFBLR", "")]

    [TestCase("FBLRA", "FBLR")]
    [TestCase("FBALR", "FB")]
    [TestCase("FBALRQFR", "FB")]
    public void FailWithUnknownCommand(string requestedCommands, string executedCommands)
    {
        var rover = new TestRover(true, true);
        var driver = new Driver(rover);

        Assert.Multiple(() =>
        {
            Assert.Throws<InvalidOperationException>(() => driver.Move(requestedCommands));
            Assert.That(rover.GetExecutedCommands(), Is.EqualTo(executedCommands), "Commands before unknown should be executed");
        });
    }
}
