using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Battleship.Enum;
using Battleship.Game_Logic;

namespace Battleship.UI
{
    public class GameViewModel
    {
        private readonly Grid _uiGrid;

        public Game Game { get; }

        public ICommand CycleFieldStateCommand { get; }

        public GameViewModel(Difficulty difficulty, Grid uiGrid)
        {
            CycleFieldStateCommand = 
                new RelayCommand(CycleFieldState);


            Game = new Game(difficulty);
            _uiGrid = uiGrid;
            InitializeUIGrid();
        }

        private void InitializeUIGrid()
        {
            int gridAxis = Game.PlayerBoard.Length;

            for (int i = 0; i <= gridAxis; i++)
            {
                _uiGrid.RowDefinitions.Add(new RowDefinition());
                _uiGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < gridAxis; row++)
            {
                for (int column = 0; column < gridAxis; column++)
                { 
                    var button = new Button();
                    var field = Game.PlayerBoard.Grid[row, column];

                    var binding = new Binding
                    {
                        Converter = new FieldState2ColorConverter(), 
                        Source = field,
                        Path = new PropertyPath("State"),
                        Mode = BindingMode.OneWay,
                    };

                    var style = new Style(typeof(Button));
                    style.Setters.Add(new Setter(Control.BackgroundProperty, binding));
                    button.Style = style;

                    button.Command = CycleFieldStateCommand;
                    button.CommandParameter = field;

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);

                    _uiGrid.Children.Add(button);
                }
            }

            // Ships per Column numbers
            for (int column = 0; column < gridAxis; column++)
            {
                var textBlock = new TextBlock
                {
                    Text = Game.ShipsPerColumn[column].ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Grid.SetRow(textBlock, gridAxis);
                Grid.SetColumn(textBlock, column);

                _uiGrid.Children.Add(textBlock);
            }

            // Ships per Row numbers
            for (int row = 0; row < gridAxis; row++)
            {
                var textBlock = new TextBlock
                {
                    Text = Game.ShipsPerRow[row].ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Grid.SetRow(textBlock, row);
                Grid.SetColumn(textBlock, gridAxis);

                _uiGrid.Children.Add(textBlock);
            }
        }

        private void CycleFieldState(object parameter)
        {
            if (parameter is Field field)
            {
                field.CycleFieldState();

                if (Game.IsPlayerBoardDone())
                {
                    MessageBox.Show("You won!");
                }
            }
        }

    }
}
