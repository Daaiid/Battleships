using Battleship.Enum;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Battleship.UI
{
    class FieldState2ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = new SolidColorBrush(Colors.Red);

            if (value is FieldState)
            {
                switch (value)
                {
                    case FieldState.Empty:
                        result = new SolidColorBrush(Colors.White);
                        break;

                    case FieldState.Water:
                        result = new SolidColorBrush(Colors.Blue);
                        break;

                    case FieldState.Ship:
                        result = new SolidColorBrush(Colors.Black);
                        break;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
