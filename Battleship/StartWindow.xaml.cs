using System.Windows;
using Battleship.Enum;

namespace Battleship
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void EasyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            OpenGameWindow(Difficulty.Easy);
        }

        private void MediumBtn_OnClick(object sender, RoutedEventArgs e)
        {
            OpenGameWindow(Difficulty.Medium);
        }

        private void HardBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
            OpenGameWindow(Difficulty.Hard);
        }

        private void OpenGameWindow(Difficulty difficulty)
        {
            var gameWindow = new GameWindow(difficulty);
            gameWindow.Show();
            Close();
        }
    }
}
