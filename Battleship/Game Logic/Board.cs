using System.Collections.Generic;
using Battleship.Enum;

namespace Battleship.Game_Logic
{
    class Board
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

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Grid[i,j] = new Field();
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

            return Grid[coord.Column, coord.Row].State;
        }

        public Field[] GetRow(int rowIndex)
        {
            var row = new List<Field>();

            for (int i = 0; i < _length; i++)
            {
                row.Add(_grid[i, rowIndex]);
            }

            return row.ToArray();
        }

        public Field[] GetColumn(int columnIndex)
        {
            var column = new List<Field>();

            for (int i = 0; i < _length; i++)
            {
                column.Add(_grid[columnIndex, i]);
            }

            return column.ToArray();
        }
    }
}
