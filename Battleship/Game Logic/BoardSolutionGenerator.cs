using System;
using System.Linq;
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

            var shipCounter = _board.ShipCounter.TotalShips.ToArray();

            // For each ship type:
            for (int i = shipCounter.Length - 1; i >= 0; i--)
            {
                // For each ship of the current type:
                for (int j = 0; j < shipCounter[i]; j++)
                {
                    // Make a ship
                    var ship = new Ship(i + 1, RandomOrientation());

                    Coordinates coords;

                    // Generate coordinates until they are valid
                    do
                    {
                        coords = new Coordinates
                        {
                            Row = _rng.Next(0, (int)(_board.Length - ship.Length + 1)),
                            Column = _rng.Next(0, (int)(_board.Length - ship.Length + 1))
                        };


                    } while (!IsShipPlaceable(coords, ship));

                    // Place ship onto board
                    for (int k = 0; k < ship.Length; k++)
                    {
                        _board.Grid[coords.Column, coords.Row].State = FieldState.Ship;

                        if (ship.Orientation == Orientation.Horizontal)
                        {
                            coords.Column++;
                        }

                        if (ship.Orientation == Orientation.Vertical)
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
            for (int i = 0; i < ship.Length; i++)
            {
                if (IsShipElementPlaceable(coords))
                {
                    return false;
                }

                if (ship.Orientation == Orientation.Horizontal)
                {
                    coords.Column++;
                }

                if (ship.Orientation == Orientation.Vertical)
                {
                    coords.Row++;
                }
            }

            return true;
        }

        private bool IsShipElementPlaceable(Coordinates coords)
        {
            // Checks both the bounding fields and the field on the coordinates, if there is a ship element already
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (_board.GetFieldState(coords.WithOffset(i, j)) == FieldState.Ship)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
