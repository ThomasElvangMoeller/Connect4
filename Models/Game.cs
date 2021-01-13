using Connect4.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Connect4.Models
{
    public class Game
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public List<ApplicationUser> Players { get; set; }
        public List<PlayerGameState> PlayerStates { get; set; }
        public BoardTile[,] GameBoard { get; set; }
        public List<int> CardDrawPile { get; set; }
        public List<int> CardDiscardPile { get; set; }

        public Game() { }

        public Game(int width, int height)
        {
            this.CreateBoard(width, height);
        }

        private void CreateBoard(int width, int height, int seed = -1)
        {
            GameBoard = new BoardTile[width, height];
            List<int> tileValues = Enumerable.Range(1, width * height).ToList();
            if (seed > 0)
            {
                tileValues.Shuffle(seed);
            }
            else
            {
                tileValues.Shuffle();
            }
            Queue<int> removeValues = new Queue<int>(tileValues);
            for (int i = 0; i < GameBoard.GetUpperBound(0); i++)
            {
                for (int j = 0; j < GameBoard.GetUpperBound(1); j++)
                {
                    GameBoard[i, j] = new BoardTile() { TileValue = removeValues.Dequeue() };
                }
            }
        }
    }
}