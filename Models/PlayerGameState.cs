using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Connect4.Models
{
    public class PlayerGameState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ApplicationUser Player { get; set; }
        public Game Game { get; set; }
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