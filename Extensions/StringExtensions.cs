using Connect4.Models;
using System;
using System.Collections.Generic;

namespace Connect4.Extensions
{
    public static class StringExtensions
    {
        public static List<int> ParseAll(this string[] sInts)
        {
            List<int> newInts = new List<int>();
            foreach (var item in sInts)
            {
                if (int.TryParse(item, out int oInt))
                {
                    newInts.Add(oInt);
                }
            }

            return newInts;
        }

        public static string PlayerColorName(this PlayerColor playerColor, bool uppercase = false)
        {
            if (uppercase)
            {
                return playerColor switch
                {
                    PlayerColor.White => "White",
                    PlayerColor.Black => "Black",
                    PlayerColor.Grey => "Grey",
                    PlayerColor.Red => "Red",
                    _ => "",
                };
            }
            else
            {
                return playerColor switch
                {
                    PlayerColor.White => "white",
                    PlayerColor.Black => "black",
                    PlayerColor.Grey => "grey",
                    PlayerColor.Red => "red",
                    _ => "",
                };
            }
        }
    }
}