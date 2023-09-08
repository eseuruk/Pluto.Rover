namespace Pluto.Rover.Core
{
    public interface IDriver
    {
        IRover Rover { get; }

        bool Move(string commands);
    }
}
