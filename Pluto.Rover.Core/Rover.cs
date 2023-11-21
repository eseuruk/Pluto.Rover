namespace Pluto.Rover.Core;

public class Rover : IRover
{
    private readonly IPlanet planet;

    private Rover(IPlanet planet, Coordinate landngPosition, ViewDirection dirrection)
    {
        ArgumentNullException.ThrowIfNull(planet);
        this.planet = planet;

        Position = landngPosition;
        Direction = dirrection;
    }

    public Coordinate Position { get; private set; }

    public ViewDirection Direction { get; private set; }

    public static IRover Land(IPlanet planet, Coordinate landngPosition, ViewDirection dirrection)
    {
        ArgumentNullException.ThrowIfNull(planet);

        if (!planet.CoordinateSystem.IsPositionValid(landngPosition))
        {
            throw new ArgumentOutOfRangeException(nameof(landngPosition), landngPosition, "Landing position is invalid in the current coordinate systenm");
        }

        if (planet.Obstacles.Contains(landngPosition))
        {
            throw new ArgumentOutOfRangeException(nameof(landngPosition), landngPosition, "Landing position is blocked by obstacle");
        }

        return new Rover(planet, landngPosition, dirrection);
    }

    public bool TryMoveForward()
    {
        var newPosition = planet.CoordinateSystem.Move(Position, Direction, 1);
        if (planet.Obstacles.Contains(newPosition))
        {
            return false;
        }

        Position = newPosition;
        return true;
    }

    public bool TryMoveBackward()
    {
        var newPosition = planet.CoordinateSystem.Move(Position, Direction, -1);
        if (planet.Obstacles.Contains(newPosition))
        {
            return false;
        }

        Position = newPosition;
        return true;
    }

    public void TakeLeft()
    {
        Direction = Direction.TakeLeft();
    }

    public void TakeRight()
    {
        Direction = Direction.TakeRight();
    }
}