namespace Pluto.Rover.Core
{
    public interface IRover
    {
        public Coordinate Position { get;  }

        public ViewDirection Direction { get; }

        bool TryMoveForward();

        bool TryMoveBackward();

        void TakeLeft();

        void TakeRight();
    }
}
