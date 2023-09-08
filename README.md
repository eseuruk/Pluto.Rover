# Pluto Rover

Repository contains code to solve a test task known as 'Pluto Rover Exercise'.

## The Task

After NASA's New Horizon successfully flew past Pluto, they now plan to land a Pluto Rover
to further investigate the surface. You are responsible for developing an API that will allow
the Rover to move around the planet. As you won`t get a chance to fix your code once it is
onboard, you are expected to use test driven development.

To simplify navigation, the planet has been divided up into a grid. The rover's position and
location is represented by a combination of x and y coordinates and a letter representing
one of the four cardinal compass points. An example position might be 0, 0, N, which
means the rover is in the bottom left corner and facing North. Assume that the square
directly North from (x, y) is (x, y+1).

In order to control a rover, NASA sends a simple string of letters. The only commands you
can give the rover are 'F','B','L' and 'R'
* Implement commands that move the rover forward/backward ('F','B'). The rover
may only move forward/backward by one grid point, and must maintain the same
heading.
* Implement commands that turn the rover left/right ('L','R'). These commands make
the rover spin 90 degrees left or right respectively, without moving from its current
spot.
* Implement wrapping from one edge of the grid to another. (Pluto is a sphere after
all)
* Implement obstacle detection before each move to a new square. If a given
sequence of commands encounters an obstacle, the rover moves up to the last
possible point and reports the obstacle.

Here is an example:
* Let's say that the rover is located at 0,0 facing North on a 100x100 grid.
* Given the command "FFRFF" would put the rover at 2,2 facing East

## Solution Structure

* **Pluto.Rover.Core** project contains code of core classes and interfaces
* **Pluto.Rover.Tests** project contains unit tests for the core classes
* **Pluto.Rover.Simulations** project contains simulations of the rover behaviour in the real environment (100x100 wrapped grid planet). 

## Dependencies
* .NET 6.0 LTS
* Nunit 3
* Moq 4 

## How to select coordinate system

Coordinate system of the planet is represented by:

    public interface ICoordinateSystem
    {
        bool IsPositionValid(Coordinate c);

        Coordinate Move(Coordinate position, ViewDirection direction, int stepCount);
    }

There are predefined implementations available:
* *InfiniteGrid* which represents infinite grid with all valid cells.
* *IvalidGrid* which represents infinite grid with all invalid cells. (Used mostly for testing)
* *WrappedGrid* which represents NxM grid with (0,0) ss the left bottom cell. 

## How to create the planet

To create the planet with wrapped grid coordinate system:

    var grid = new WrappedGrid(GridWidth, GridHeigth);
    var planet = new Planet(grid);

To add the obstacles:

    panet.AddObstacle(5, 100);
    panet.AddObstacle(20, 120);


Coordinate of the obstacle should be valid in the selected coordinate system. In other case exception is thrown.

## How to land the rover

To get access to the rover object it should be landed onto the planet surface.

    var rover = Rover.Land(planet, landingPosition, initialDirection);

If landing position is invalid or contains an obstacle, then the rover is not landed and exception is thrown.

## How to move the rover

As rover is landed it might be controlled via IRover interface:

    public interface IRover
    {
        public Coordinate Position { get; }

        public ViewDirection Direction { get; }

        bool TryMoveForward();

        bool TryMoveBackward();

        void TakeLeft();

        void TakeRight();
    }

*Position* and *Direction* properties describe the current state of the rover and are changed only after the successful movement.

*TryMoveForward* and *TryMoveBackward* operations may return false if they are blocked by the obstacle in the destination location. 
In this case position of the rover is not changed.

## How to control the rover via commands 

To control the rover via string commands:

    var driver = new Driver(rover);
    driver.Move("FFRFRBBLL")

Driver translates string commands into the sequence of Rover operations:
* 'F' to call TryMoveForward()
* 'B' to call TryMoveBackward()
* 'L' to TakeLeft()
* 'R' to TakeRight()

Any unrecognised command causes an exception. All commands before are executed and Rover stops.

If rover is blocked by an obstacle, then commands are executed till the last possible step. 


