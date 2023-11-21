namespace Pluto.Rover.Core;

public record Coordinate
{
    public int X { get; init; }
    public int Y { get; init; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}