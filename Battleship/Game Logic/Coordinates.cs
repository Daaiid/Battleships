using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game_Logic
{
    class Coordinates
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Coordinates WithOffset(int rowOffset, int columnOffset)
        {
            return new Coordinates{Column = Column + columnOffset, Row = Row + rowOffset};
        }
    }
}
