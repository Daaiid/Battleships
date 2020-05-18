using System.Windows.Controls;

namespace Battleship.Game_Logic
{
    class Ship
    {
        public int Length { get; }
        public Orientation Orientation { get; }

        public Ship(int length, Orientation orientation)
        {
            Length = length;
            Orientation = orientation;
        }
    }
}
