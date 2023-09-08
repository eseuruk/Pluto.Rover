namespace Pluto.Rover.Core
{
    public interface ICoordinateSystem
    {
        bool IsPositionValid(Coordinate c);

        Coordinate Move(Coordinate position, ViewDirection direction, int stepCount);
    }
}