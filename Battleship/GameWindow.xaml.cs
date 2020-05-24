using System.Windows;
using Battleship.Enum;

namespace Battleship
{
    public partial class GameWindow : Window
    {
        public readonly Difficulty Difficulty;

        public GameWindow(Difficulty difficulty)
        {
            Difficulty = difficulty;

            InitializeComponent();
        }
    }
}
