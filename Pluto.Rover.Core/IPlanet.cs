using System.Collections.Generic;

namespace Pluto.Rover.Core
{
    public interface IPlanet
    {
        public ICoordinateSystem CoordinateSystem { get; }

        public IReadOnlySet<Coordinate> Obstacles { get; }
    }
}