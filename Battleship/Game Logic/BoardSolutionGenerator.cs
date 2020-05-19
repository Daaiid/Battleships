using System;
using System.Windows.Controls;
using Battleship.Enum;

namespace Battleship.Game_Logic
{
    class BoardSolutionGenerator
    {
        private readonly Random _rng = new Random();
        private readonly Difficulty _difficulty;
        private Board _board;

        public BoardSolutionGenerator(Difficulty difficulty)
        {
            _difficulty = difficulty;
        }

        public Board GenerateNewSolution()
        {
            _board = BoardFactory.GenerateBoard(_difficulty);

            var shipCounter = _board.ShipCounter.TotalShips;

            // For each ship type:
            for (int i = shipCounter.Count - 1; i >= 0; i--)
            {
                int shipLength = i + 1;

                // For each ship of the current type:
                for (int j = 0; j < shipCounter[i]; j++)
                {
                    // Make a ship
                    var ship = new Ship(shipLength, RandomOrientation());

                    Coordinates shipOrigin;

                    // Generate coordinates for the ship's origin
                    // until it can be placed based on the rules.
                    do
                    {
                        shipOrigin = new Coordinates
                        {
                            // The bound of the random numbers is adjusted, 
                            // so no elements of the ship go out of bounds.
                            // Plus one because the upper bound in Next() is exclusive.
                            Row = _rng.Next(0, _board.Length - shipLength + 1),
                            Column = _rng.Next(0, _board.Length - shipLength + 1)
                        };

                    } while (!IsShipPlaceable(shipOrigin, ship));

                    PlaceShipOntoTheBoard(shipOrigin, ship);
                }
            }

            return _board;
        }

        private Orientation RandomOrientation()
        {
            return _rng.Next(0, 2) == 1 ? Orientation.Horizontal : Orientation.Vertical;
        }

        private bool IsShipPlaceable(Coordinates shipOrigin, Ship ship)
        {
            // For each element of ship:
            for (int element = 0; element < ship.Length; element++)
            {
                // Adjust the coordinates from the origin to the current element
                // based on orientation.
                var elementCoords = ship.Orientation == Orientation.Horizontal
                    ? shipOrigin.WithOffset(0, element)
                    : shipOrigin.WithOffset(element, 0);

                if (!IsShipElementPlaceable(elementCoords))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsShipElementPlaceable(Coordinates coords)
        {
            // Checks both the bounding fields and the field on the coordinates, if there is a ship element present
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (_board.GetFieldState(coords.WithOffset(i, j)) == FieldState.Ship)
                    {
                        // A single ship field means that the element can't be placed.
                        return false;
                    }
                }
            }

            return true;
        }


        private void PlaceShipOntoTheBoard(Coordinates shipOrigin, Ship ship)
        {
            // For each element of the ship:
            for (int element = 0; element < ship.Length; element++)
            {
                var elementCoords = ship.Orientation == Orientation.Horizontal
                    ? shipOrigin.WithOffset(0, element)
                    : shipOrigin.WithOffset(element, 0);

                _board.Grid[elementCoords.Column, elementCoords.Row].State = FieldState.Ship;
            }
        }
    }
}
