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
        public string Player { get; set; }
        //public Game Game { get; set; } Not needed if it does not have it's own db table
        public int PlayerPieces { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayerColor Color { get; set; }
        public List<int> Cards { get; set; }

        public List<int> GetAllCardSums()
        {
            sumHolder = new List<int>();

            subsetSum(Cards, 0, Cards.Count - 1, 0);

            return sumHolder;

        }

        private List<int> sumHolder;
        private void subsetSum(List<int> lst, int left, int right, int sum)
        {
            if(left > right)
            {
                sumHolder.Add(sum);
            }

            subsetSum(lst, left + 1, right, sum + lst[left]);
            
            subsetSum(lst, left + 1, right, sum);

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