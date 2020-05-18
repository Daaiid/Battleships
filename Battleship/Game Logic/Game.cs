using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Battleship.Enum;

namespace Battleship.Game_Logic
{
    class Game
    {
        private Difficulty _difficulty;
        private readonly Board _solutionBoard;
        private readonly Board _playerBoard;
        private IReadOnlyCollection<int> _rowNumbers;
        private IReadOnlyCollection<int> _columnNumbers;



        public Game(Difficulty difficulty)
        {
            _difficulty = difficulty;

            _solutionBoard = new BoardSolutionGenerator(_difficulty).GenerateNewSolution();


        }

        private void GenerateRowNumbers()
        {

        }

        private void GenerateColumnNumbers()
        {

        }

    }
}
