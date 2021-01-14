using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Connect4.Models
{
    public class PlayerGameState
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Not needed if this model gets converted to json, does not need it's own table
        //public int Id { get; set; }

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

    }

    public enum PlayerColor
    {
        White,
        Black,
        Grey,
        Red
    }
}