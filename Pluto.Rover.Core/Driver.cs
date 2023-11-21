namespace Pluto.Rover.Core;

public class Driver : IDriver
{
    public Driver(IRover rover)
    {
        ArgumentNullException.ThrowIfNull(rover);
        Rover = rover;
    }

    public IRover Rover { get; }

    public bool Move(string commands)
    {
        ArgumentNullException.ThrowIfNull(commands);

        foreach (var command in commands)
        {
            bool moved = Move(command);
            if (!moved) return false;
        }

        return true;
    }

    private bool Move(char command)
    {
        switch (command)
        {
            case 'F':
                return Rover.TryMoveForward();
            case 'B':
                return Rover.TryMoveBackward();
            case 'L':
                Rover.TakeLeft();
                return true;
            case 'R':
                Rover.TakeRight();
                return true;
            default:
                throw new InvalidOperationException($"Unrecognised command: {command}");
        }
    }
}
