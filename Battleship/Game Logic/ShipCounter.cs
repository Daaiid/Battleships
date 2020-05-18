using System.Collections.Generic;

namespace Battleship.Game_Logic
{
    class ShipCounter
    {
        // This array stores the total available ships per type (length)
        // Index 0 contains the one length boat, index 1 countains 2 length boats etc.
        // Index of counter = ship length - 1
        private IReadOnlyCollection<int> _totalShips;
        // This array stores the counts of the currently placed ships on the board.
        // Indexing logic is identical to the one above.
        private int[] _foundShips;

        public ShipCounter(int[] totalShips)
        {
            _totalShips = totalShips;
            _foundShips = new int[totalShips.Length];
        }


        public IReadOnlyCollection<int> TotalShips
        {
            get { return _totalShips; }
        }

        public int[] FoundShips
        {
            get { return _foundShips; }
        }
    }
}
