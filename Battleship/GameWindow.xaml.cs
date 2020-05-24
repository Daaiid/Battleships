using System.Windows;
using Battleship.Enum;
using Battleship.ViewModels;

namespace Battleship
{
    public partial class GameWindow : Window
    {
        public GameWindow(Difficulty difficulty)
        {
            DataContext = new GameViewModel(difficulty);
            InitializeComponent();
        }
    }
}
