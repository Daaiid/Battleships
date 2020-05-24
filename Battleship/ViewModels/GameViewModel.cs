using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Enum;
using Battleship.Game_Logic;

namespace Battleship.ViewModels
{
    public class GameViewModel
    {
        public Game Game { get; }
        public GameViewModel(Difficulty difficulty)
        {
            Game = new Game(difficulty);
        }
    }
}
