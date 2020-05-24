namespace Battleship.Game_Logic
{
    public class Coordinates
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Coordinates WithOffset(int rowOffset, int columnOffset)
        {
            return new Coordinates{Column = Column + columnOffset, Row = Row + rowOffset};
        }
    }
}
