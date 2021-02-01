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
        public string[] Players = new string[MaxPlayers]; //max 4 players
        public int[] PlayerColors = new int[MaxPlayers];
        public string[] PlayerNames = new string[MaxPlayers];
        public readonly string Password;

        public Lobby(ApplicationUser user = null, string password = null)
        {
            if (user != null)
            {
                Players[0] = user.Id.ToString();
                PlayerNames[0] = user.UserName;
            }
            else
            {
                Players[0] = GenerateRandomName();
                Random random = new Random();
                PlayerNames[0] = Constants.Animals[random.Next(Constants.Animals.Length)];
            }
            PlayerColors = new int[] { -1, -1, -1, -1 };
            Password = password;
            Id = Guid.NewGuid();
        }

        /// <param name="user">optional</param>
        /// <returns>The generated playerId, or null if there is no more room</returns>
        public string AddPlayer(ApplicationUser user = null)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i] != null)
                {
                    continue;
                }
                else if (user != null)
                {
                    Players[i] = user.Id.ToString();
                    PlayerNames[i] = user.UserName;
                    return Players[i];
                }
                else
                {
                    Players[i] = GenerateRandomName();
                    Random random = new Random();
                    PlayerNames[i] = Constants.Animals[random.Next(Constants.Animals.Length)];
                    return Players[i];
                }

            }
            return null;
        }

        public bool RemovePlayer(string player)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i] == player)
                {
                    Players[i] = null;
                    return true;
                }
            }
            return false;
        }

        public void SetPlayerName(string player, string name)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if(Players[i] == player)
                {
                    PlayerNames[i] = name;
                    break;
                }
            }
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
