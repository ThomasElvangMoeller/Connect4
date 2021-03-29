using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public int PlayerColor { get; set; }
        public Guid? ApplicationUserId { get; set; }
        public bool IsAppUser => ApplicationUserId != null;

        public Player(string name, string connectionId, int playerColor, Guid? applicationUserId = null)
        {
            Name = name;
            ConnectionId = connectionId;
            PlayerColor = playerColor;
            ApplicationUserId = applicationUserId;
        }
        public Player() { }
    }
}
