namespace Pluto.Rover.Core;

public class Planet : IPlanet
{
    private readonly HashSet<Coordinate> obstacles = new HashSet<Coordinate>();

    public Planet(ICoordinateSystem coordinateSystem)
    {
        ArgumentNullException.ThrowIfNull(coordinateSystem);
        CoordinateSystem = coordinateSystem;
    }

    public ICoordinateSystem CoordinateSystem { get; }

    public IReadOnlySet<Coordinate> Obstacles { get { return obstacles; } }

    public void AddObstacle(Coordinate position)
    {
        if (!CoordinateSystem.IsPositionValid(position))
        {
            throw new ArgumentOutOfRangeException(nameof(position), position, "Position is invalid in the current coordinate systenm");
        }

        obstacles.Add(position);
    }

    public void AddObstacle(int x, int y)
    {
        AddObstacle(new Coordinate(x, y));
    }
}