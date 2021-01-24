using System;

namespace Connect4.Models
{
    public class Tile
    {
        int X { get; set; }
        int Y { get; set; }
        int Value { get; set; }

        public Tile() { }

        public Tile(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Tile tile &&
                   X == tile.X &&
                   Y == tile.Y &&
                   Value == tile.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Value);
        }
    }
}