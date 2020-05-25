using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Battleship.Enum;

namespace Battleship.Game_Logic
{
    public class Game
    {
        private const int HintFieldCount = 5;

        private readonly Difficulty _difficulty;
        private readonly Board _solutionBoard;
        private readonly Board _playerBoard;
        private IReadOnlyList<int> _shipsPerRow;
        private IReadOnlyList<int> _shipsPerColumn;

        public Game(Difficulty difficulty)
        {
            _difficulty = difficulty;

            _solutionBoard = new BoardSolutionGenerator(_difficulty).GenerateNewSolution();

            CountShipsPerColumn();
            CountShipsPerRow();

            _playerBoard = BoardFactory.GenerateBoard(_difficulty);
            SetHintFieldsOnPlayerBoard();
        }

        public Board PlayerBoard
        {
            get { return _playerBoard; }
        }



        public IReadOnlyList<int> ShipsPerRow
        {
            get { return _shipsPerRow; }
        }

        public IReadOnlyList<int> ShipsPerColumn
        {
            get { return _shipsPerColumn; }
        }

        public Board SolutionBoard
        {
            get { return _solutionBoard; }
        }

        public bool IsPlayerBoardDone()
        {
            for (int row = 0; row < PlayerBoard.Length; row++)
            {
                for (int column = 0; column < PlayerBoard.Length; column++)
                {
                    if (PlayerBoard.Grid[row, column].State != SolutionBoard.Grid[row, column].State)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void CountShipsPerRow()
        {
            var shipsPerRow = new List<int>();

            for (int row = 0; row < SolutionBoard.Length; row++)
            {
                shipsPerRow.Add(SolutionBoard.GetRow(row).Count(field => field.State == FieldState.Ship));
            }

            _shipsPerRow = shipsPerRow;
        }

        private void CountShipsPerColumn()
        {
            var shipsPerColumn = new List<int>();

            for (int column = 0; column < SolutionBoard.Length; column++)
            {
                shipsPerColumn.Add(SolutionBoard.GetColumn(column).Count(field => field.State == FieldState.Ship));
            }

            _shipsPerColumn = shipsPerColumn;
        }

        private void SetHintFieldsOnPlayerBoard()
        {
            var rng = new Random();

            for (int i = 0; i < HintFieldCount; i++)
            {
                Field fieldToShow;
                Coordinates randomCoords;

                do
                {
                    randomCoords = new Coordinates
                    {
                        Row = rng.Next(PlayerBoard.Length),
                        Column = rng.Next(PlayerBoard.Length)
                    };

                    fieldToShow = SolutionBoard.Grid[randomCoords.Row, randomCoords.Column]; 

                } while (fieldToShow.State == FieldState.Empty);

                var readonlyField = new Field(fieldToShow.State);

                PlayerBoard.Grid[randomCoords.Row, randomCoords.Column] = readonlyField;
            }
        }
    }
}
