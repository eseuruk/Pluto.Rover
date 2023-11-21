namespace Pluto.Rover.Core;

public class InfinitGrid : ICoordinateSystem
{
    public bool IsPositionValid(Coordinate position)
    {
        return true;
    }

    public Coordinate Move(Coordinate position, ViewDirection direction, int stepCount)
    {
        switch (direction)
        {
            case ViewDirection.North:
                return new Coordinate(position.X, position.Y + stepCount);
            case ViewDirection.West:
                return new Coordinate(position.X - stepCount, position.Y);
            case ViewDirection.South:
                return new Coordinate(position.X, position.Y - stepCount);
            case ViewDirection.East:
                return new Coordinate(position.X + stepCount, position.Y);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unsupported dirrection");
        }
    }
}
