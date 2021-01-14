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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public List<ApplicationUser> Players { get; set; }
        public Dictionary<string, PlayerGameState> PlayerStates { get; set; }
        public BoardTile[,] GameBoard { get; set; }
        public List<int> CardDrawPile { get; set; }
        public List<int> CardDiscardPile { get; set; }

        public Game() { }

        public Game(GameSettings settings, Dictionary<string, PlayerColor> playersAndColors)
        {
            this.Id = Guid.NewGuid();
            int seed = string.IsNullOrWhiteSpace(settings.seed) ? -1 : settings.seed.GetHashCode();
            this.CreateBoard(settings.BoardWidth, settings.BoardHeight, seed);
            this.CardDrawPile = settings.Cards;
            this.CardDrawPile.Shuffle();
            this.CardDiscardPile = new List<int>(CardDrawPile.Count + 1);
            this.PlayerStates = new Dictionary<string, PlayerGameState>();
            foreach (KeyValuePair<string, PlayerColor> item in playersAndColors)
            {
                PlayerGameState state = new PlayerGameState() { Cards = CreateHand(settings.PlayerCardHoldAmount), Color = item.Value, Player = item.Key, PlayerPieces = settings.PlayerPiecesAmount };
                this.PlayerStates.Add(item.Key, state);
            }
        }
        private List<int> CreateHand(int handSize)
        {
            List<int> hand = new List<int>();
            for (int i = 0; i < handSize; i++)
            {
                hand.Add(this.CardDrawPile.Last());
            }
            return hand;

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