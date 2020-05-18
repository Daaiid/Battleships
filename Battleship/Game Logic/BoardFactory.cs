using System;
using Battleship.Enum;

namespace Battleship.Game_Logic
{
    static class BoardFactory
    {
        public static Board GenerateBoard(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return new Board(6, new ShipCounter(new []{3, 2, 1}));
                case Difficulty.Medium:
                    return new Board(8, new ShipCounter(new []{4, 3, 2, 1}));
                case Difficulty.Hard:
                    return new Board(10, new ShipCounter(new []{4,3,2,1}));
            }

            throw new Exception("Difficulty not correct.");
        }
    }
}
