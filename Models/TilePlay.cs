using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class TilePlay : Tile
    {
        public int[] CardsUsed { get; set; }

        public TilePlay(int x, int y, int value, int[] cardsUsed) : base(x, y, value)
        {
            CardsUsed = cardsUsed;
        }
    }
}
