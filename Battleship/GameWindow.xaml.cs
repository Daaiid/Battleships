using System.Windows;
using Battleship.Enum;
using Battleship.UI;

namespace Battleship
{
    public partial class GameWindow : Window
    {
        public GameWindow(Difficulty difficulty)
        {
            InitializeComponent();
            
            DataContext = new GameViewModel(difficulty, PlayBoardGrid);
        }

    }
}
