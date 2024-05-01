using System.Text;

namespace Pluto.Rover.Tests;

public class TestRover : IRover
{
    private StringBuilder executedCommands = new StringBuilder();
    private StringBuilder blockedCommands = new StringBuilder();

    public bool AllowMoveForwared { get; }

    public bool AllowMoveBackward { get; }

    public Coordinate Position { get; } = new Coordinate(0, 0);

    public ViewDirection Direction { get; } = ViewDirection.North;

    public TestRover(bool allowMoveForwared, bool allowMoveBackward)
    {
        AllowMoveForwared = allowMoveForwared;
        AllowMoveBackward = allowMoveBackward;
    }

    public string GetExecutedCommands() { return executedCommands.ToString(); }

    public string GetBlockedCommands() { return blockedCommands.ToString(); }

    public void TakeLeft()
    {
        executedCommands.Append('L');
    }

    public void TakeRight()
    {
        executedCommands.Append('R');
    }

    public bool TryMoveBackward()
    {
        if (!AllowMoveBackward)
        {
            blockedCommands.Append('B');
            return false;
        }

        executedCommands.Append('B');
        return true;
    }

    public bool TryMoveForward()
    {
        if (!AllowMoveForwared)
        {
            blockedCommands.Append('F');
            return false;
        }

        executedCommands.Append('F');
        return true;
    }
}
