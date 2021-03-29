using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Models
{
    /// <summary>
    /// The Lobby is created before the game, one player creates the lobby and can adjust the settings of the game from there.
    /// While in the lobby, other players can join the "host"'s lobby. Once all are ready the "host" presses "start" and then a game is actually created with the given players
    /// </summary>
    public class Lobby
    {
        public const int MaxPlayers = 4;
        public Guid Id { get; set; }
        public string Name { get; set; } // Name of the lobby
        public Player[] Players = new Player[MaxPlayers]; //max 4 players
        public readonly string Password;

        public Lobby(Player player, string password = null)
        {
            Players[0] = player;
            Name = string.Concat(Constants.Animals.Rand(), " ", Constants.Animals.Rand(), " ", Constants.Animals.Rand()); // Makes the name 3 random animal name
            Password = password;
            Id = Guid.NewGuid();
        }

        /// <param name="user">optional</param>
        /// <returns>The generated playerId, or null if there is no more room</returns>
        public Player AddPlayer(string connectionId, ApplicationUser user)//currently not used until the persistent user system is up
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i] != null)
                {
                    continue;
                }
                else
                {
                    Players[i] = new Player(user.UserName, connectionId, -1, user.Id);
                    return Players[i];
                }
            }
            return null;
        }
        /// <param name="connectionId">Used in Hub context</param>
        /// <returns>The connectionId of the given player was successfully added, otherwise null</returns>
        public Player AddPlayer(Player player)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i] != null)
                {
                    continue;
                }
                else if (player != null)
                {
                    Players[i] = player;
                    return Players[i];
                }

            }
            return null;
        }
        /// <summary>
        /// Checks if <paramref name="player"/> matches the players connectionId or name
        /// </summary>
        /// <param name="player"></param>
        /// <returns>True if successful</returns>
        public bool RemovePlayer(string player)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i].ConnectionId == player || Players[i].Name == player)
                {
                    Players[i] = null;
                    return true;
                }
            }
            return false;
        }

        const string letters = "abcdefghijklmnopqrstuvwxyz0123456789";

        /// <returns>A 16 Char string made of a-z and 0-9</returns>
        private string GenerateRandomName()
        {
            StringBuilder sb = new StringBuilder(16);
            Random random = new Random();
            for (int i = 0; i < 16; i++)
            {
                sb.Append(letters[random.Next(letters.Length)]);
            }
            return sb.ToString();
        }
    }
}
