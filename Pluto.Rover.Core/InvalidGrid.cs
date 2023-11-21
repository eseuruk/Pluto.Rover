namespace Pluto.Rover.Core;

public class InvaliddGrid : ICoordinateSystem
{
    public bool IsPositionValid(Coordinate position)
    {
        return false;
    }

    public Coordinate Move(Coordinate position, ViewDirection direction, int stepCount)
    {
        throw new InvalidOperationException("Movement is not supported");
    }
}
