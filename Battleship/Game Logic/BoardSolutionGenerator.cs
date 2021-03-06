﻿using System;
using System.Windows.Controls;
using Battleship.Enum;

namespace Battleship.Game_Logic
{
    class BoardSolutionGenerator
    {
        private const int MaxAttempts = 1000;

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

            // First fill everything with water
            foreach (var field in _board.Grid)
            {
                field.State =  FieldState.Water;
            }

            var shipCounter = _board.ShipCounter.TotalShips;

            // Monitors the amount of attempts we tried to place any ship.
            // There is a possibility that a board can't be finished at some point.
            // In that case we throw away the current board and try again.
            int attempts = 0;

            // For each ship type:
            // We go backwards, because the big ships are harder to place.
            // That way they get placed first and there are fewer missed attempts.
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
                        if (attempts++ > MaxAttempts)
                        {
                            // Breaks the current generation and tries again.
                            return GenerateNewSolution();
                        }

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
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                {
                    if (_board.GetFieldState(coords.WithOffset(rowOffset, columnOffset)) == FieldState.Ship)
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

                _board.Grid[elementCoords.Row, elementCoords.Column].State = FieldState.Ship;
            }
        }
    }
}
