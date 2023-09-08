using System;
using Pluto.Rover.Core;

namespace Pluto.Rover.Simulations
{
    public static class SimulationContext
    {
        public const int GridWidth = 100;
        public const int GridHeigth  = 100;

        public static Planet CreatePlanet()
        {
            var grid = new WrappedGrid(GridWidth, GridHeigth);

            return new Planet(grid);
        }
    }
}