using System.Collections.Generic;
using Battleship.Enum;

namespace Battleship.Game_Logic
{
    public class Board
    {
        // First index are the columns (x), second are rows(y)
        private Field[,] _grid;
        private ShipCounter _shipCounter;
        private int _length;

        internal Board(int fieldsPerAxis, ShipCounter shipCounter)
        {
            _length = fieldsPerAxis;
            _shipCounter = shipCounter;

            _grid = new Field[Length, Length];

            for (int row = 0; row < Length; row++)
            {
                for (int column = 0; column < Length; column++)
                {
                    Grid[row,column] = new Field();
                }
            }

        }
        
        public ShipCounter ShipCounter
        {
            get { return _shipCounter; }
        }

        public int Length
        {
            get { return _length; }
        }

        public Field[,] Grid
        {
            get { return _grid; }
        }

        public FieldState GetFieldState(Coordinates coord)
        {
            if (coord.Row >= _length || coord.Column >= _length || coord.Row < 0 || coord.Column < 0)
            {
                return FieldState.Undefined;
            }

            return Grid[coord.Row, coord.Column].State;
        }

        public Field[] GetRow(int rowIndex)
        {
            var row = new List<Field>();

            for (int column = 0; column < _length; column++)
            {
                row.Add(_grid[rowIndex, column]);
            }

            return row.ToArray();
        }

        public Field[] GetColumn(int columnIndex)
        {
            var column = new List<Field>();

            for (int row = 0; row < _length; row++)
            {
                column.Add(_grid[row, columnIndex]);
            }

            return column.ToArray();
        }
    }
}
