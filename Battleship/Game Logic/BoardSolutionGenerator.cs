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

                    Coordinates coords;

                    // Generate coordinates for the ship's origin
                    // until it can be placed based on the rules.
                    do
                    {
                        coords = new Coordinates
                        {
                            // The bound of the random numbers is adjusted, 
                            // so no elements of the ship go out of bounds.
                            // Plus one because the upper bound in Next() is exclusive.
                            Row = _rng.Next(0, _board.Length - shipLength + 1),
                            Column = _rng.Next(0, _board.Length - shipLength + 1)
                        };

                    } while (!IsShipPlaceable(coords, ship));

                    // Place ship onto board
                    for (int shipElement = 0; shipElement < ship.Length; shipElement++)
                    {
                        _board.Grid[coords.Column, coords.Row].State = FieldState.Ship;

                        if (ship.Orientation == Orientation.Horizontal)
                        {
                            coords.Column++;
                        }
                        else
                        {
                            coords.Row++;
                        }
                    }
                }
            }

            return _board;
        }

        private Orientation RandomOrientation()
        {
            return _rng.Next(0, 2) == 1 ? Orientation.Horizontal : Orientation.Vertical;
        }

        private bool IsShipPlaceable(Coordinates coords, Ship ship)
        {
            if (ship.Orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    if (!IsShipElementPlaceable(coords.WithOffset(0, i)))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    if (!IsShipElementPlaceable(coords.WithOffset(i, 0)))
                    {
                        return false;
                    }
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

    }
}
