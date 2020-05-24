using System.Windows.Controls;
using Battleship.Enum;
using Battleship.Game_Logic;

namespace Battleship.ViewModels
{
    public class GameViewModel
    {
        private readonly Grid _uiGrid;
        public Game Game { get; }

        public GameViewModel(Difficulty difficulty, Grid uiGrid)
        {
            Game = new Game(difficulty);
            _uiGrid = uiGrid;
            InitializeUIGrid();
        }

        private void InitializeUIGrid()
        {
            int gridAxis = Game.PlayerBoard.Length + 1;

            for (int i = 0; i < gridAxis; i++)
            {
                _uiGrid.RowDefinitions.Add(new RowDefinition());
                _uiGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < Game.PlayerBoard.Length; i++)
            {
                for (int j = 0; j < Game.PlayerBoard.Length; j++)
                {
                    
                }
            }
        }
    }
}
