using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    /// <summary>
    /// Settings for an individual game. All fields have a default value
    /// </summary>
    public class GameSettings
    {
        public int BoardWidth { get; set; } = 6;
        public int BoardHeight { get; set; } = 6;
        public Stack<int> Cards { get; set; } = new Stack<int>(new List<int>() {
            1,1,1,1,1,
            2,2,2,2,2,
            3,3,3,3,3,
            4,4,4,4,4,
            5,5,5,5,5,
            6,6,6,6,6,
            7,7,7,7,7,
            8,8,8,8,8,
            9,9,9,9,9,
            10,10,10,10,10,
            11,11,11,
            12,12,12,
            13,13,13,
            14,14,14,
            15,15,15,
            16,16,16,
            17,17,17,
            18,18,18,
            19,19,19,
            20,20,20
        });
        public int PlayerPiecesAmount { get; set; } = 20;
        public int PlayerCardHoldAmount { get; set; } = 4;
        public string seed { get; set; }  = "default";
    }
}
