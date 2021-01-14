using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class GameSettings
    {
        public int BoardWidth = 6;
        public int BoardHeight = 6;
        public List<int> Cards = new List<int>() {
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
        };
        public int PlayerPiecesAmount = 20;
        public int PlayerCardHoldAmount = 4;
        public string seed = "default";
    }
}
