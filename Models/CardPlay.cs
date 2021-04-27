using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class CardPlay
    {
        public int TileToPlay { get; set; }
        public int[] CardsUsed { get; set; }

        public CardPlay(int tileToPlay, int[] cardsUsed)
        {
            TileToPlay = tileToPlay;
            CardsUsed = cardsUsed;
        }
    }
}
