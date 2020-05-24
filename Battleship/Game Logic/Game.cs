using System.Collections.Generic;
using System.Linq;
using Battleship.Enum;

namespace Battleship.Game_Logic
{
    public class Game
    {
        private Difficulty _difficulty;
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

            // TODO: Initialize playerboard
            _playerBoard = BoardFactory.GenerateBoard(_difficulty);
            
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


        private void CountShipsPerRow()
        {
            var shipsPerRow = new List<int>();

            for (int rowIndex = 0; rowIndex < SolutionBoard.Length; rowIndex++)
            {
                shipsPerRow.Add(SolutionBoard.GetRow(rowIndex).Count(field => field.State == FieldState.Ship));
            }

            _shipsPerRow = shipsPerRow;
        }

        private void CountShipsPerColumn()
        {
            var shipsPerColumn = new List<int>();

            for (int columnIndex = 0; columnIndex < SolutionBoard.Length; columnIndex++)
            {
                shipsPerColumn.Add(SolutionBoard.GetColumn(columnIndex).Count(field => field.State == FieldState.Ship));
            }

            _shipsPerColumn = shipsPerColumn;
        }

    }
}
