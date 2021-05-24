using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Connect4.Models
{
    /// <summary>
    /// Holds the state of an individual Player
    /// <para>A <see cref="PlayerGameState"/> is not an <see cref="ApplicationUser"/>, but it can hold the Guid of an <see cref="ApplicationUser"/> </para>
    /// </summary>
    public class PlayerGameState
    {
        /// <summary>
        /// If player is an <see cref="ApplicationUser"/>, this field will be the Guid Id of that user.
        /// <para>Player is also the key to the dictionary holding the <see cref="PlayerGameState"/></para>
        /// </summary>
        public string Player { get; private set; }
        public string ConnectionId { get; private set; }
        //public Game Game { get; set; } Not needed if it does not have it's own db table
        public int PlayerPieces { get; set; }
        public PlayerColor Color { get; set; }
        public List<int> Cards { get; set; }

        public PlayerGameState(string player, string connectionId, int playerPieces, PlayerColor color, List<int> cards)
        {
            Player = player;
            ConnectionId = connectionId;
            PlayerPieces = playerPieces;
            Color = color;
            Cards = cards;
        }



        /*
        public List<int> GetAllCardPlays() // TODO: change the return to a list of objects like: { Tileplay: int, CardCombination: int[] }
        {
            sumHolder = new List<int>();
            SubsetSum(Cards, 0, Cards.Count - 1, 0);
            return sumHolder;
        }
        private List<int> sumHolder;
        private void SubsetSum(List<int> lst, int left, int right, int sum)
        {
            if (left > right)
            {
                sumHolder.Add(sum);
            }
            SubsetSum(lst, left + 1, right, sum + lst[left]);
            SubsetSum(lst, left + 1, right, sum);
        }
        */

        public List<CardPlay> GetAllCardPlays()
        {
            List<CardPlay> cardPlays = new List<CardPlay>();
            int total = 1 << Cards.Count;

            for (int i = 0; i < total; i++)
            {
                int sum = 0;
                List<int> sum_parts = new List<int>();

                for (int j = 0; j < Cards.Count; j++)
                {
                    if((i & (1 << j)) != 0)
                    {
                        sum += Cards[j];
                        sum_parts.Add(Cards[j]);
                    }
                }
                cardPlays.Add(new CardPlay(sum, sum_parts.ToArray()));
            }
            return cardPlays;
        }
    }

    public enum PlayerColor
    {
        White,
        Black,
        Grey,
        Red
    }
}