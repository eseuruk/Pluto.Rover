using System;

namespace Pluto.Rover.Core
{
    public static class Rotations
    {
        public static ViewDirection TakeLeft(this ViewDirection dirrection)
        {
            switch(dirrection)
            {
                case ViewDirection.North:
                    return ViewDirection.West;
                case ViewDirection.West:
                    return ViewDirection.South;
                case ViewDirection.South:
                    return ViewDirection.East;
                case ViewDirection.East:
                    return ViewDirection.North;
                default:
                    throw new ArgumentOutOfRangeException("dirrection", dirrection, "Unsupported dirrection");
            }
        }

        public static ViewDirection TakeRight(this ViewDirection dirrection)
        {
            switch (dirrection)
            {
                case ViewDirection.North:
                    return ViewDirection.East;
                case ViewDirection.East:
                    return ViewDirection.South;
                case ViewDirection.South:
                    return ViewDirection.West;
                case ViewDirection.West:
                    return ViewDirection.North;
                default:
                    throw new ArgumentOutOfRangeException("dirrection", dirrection, "Unsupported dirrection");
            }
        }
    }
}
