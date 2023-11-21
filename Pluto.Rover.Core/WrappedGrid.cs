namespace Pluto.Rover.Core;

public class WrappedGrid : ICoordinateSystem
{
    public WrappedGrid(int width, int heigth)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(heigth);

        Width = width;
        Heigth = heigth;
    }

    public int Width { get;  }
    public int Heigth { get; }

    public bool IsPositionValid(Coordinate position)
    {
        return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Heigth;
    }

    public Coordinate Move(Coordinate position, ViewDirection direction, int stepCount)
    {
        if (!IsPositionValid(position))
        {
            throw new ArgumentOutOfRangeException(nameof(position), position, "Position is invalid in the current coordinate systenm");
        }

        switch (direction)
        {
            case ViewDirection.North:
                return new Coordinate(position.X, MoveAndRotate(position.Y, Heigth, stepCount));
            case ViewDirection.West:
                return new Coordinate(MoveAndRotate(position.X, Width, -stepCount), position.Y);
            case ViewDirection.South:
                return new Coordinate(position.X, MoveAndRotate(position.Y, Heigth, -stepCount));
            case ViewDirection.East:
                return new Coordinate(MoveAndRotate(position.X, Width, stepCount), position.Y);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unsupported dirrection");
        }
    }

    private int MoveAndRotate(int value, int size, int steps)
    {
        if (steps == 0) return value;
        else
        {
            steps %= size;
           if (steps < 0) steps += size;

            return (value + steps) % size;
        }
    }
}
