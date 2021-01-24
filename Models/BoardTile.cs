using System;
using System.Collections.Generic;

namespace Connect4.Models
{
    public class BoardTile
    {
        public int TileValue { get; set; }
        public Dictionary<string, int> PlayerPresence { get; set; }
    }
}