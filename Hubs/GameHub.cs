using Connect4.Models;
using Connect4.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Hubs
{
    /// <summary>
    /// TODO:
    /// - Add a makeMove method
    /// - Add validation of the players move to check if it is legit
    /// - Add a GetAvailableMoves method. Tells the individual player what moves they can make. This will probably need the individual connectionId
    /// </summary>
    public class GameHub : Hub
    {
        private readonly GameService _gameService;
        private readonly UserService _userService;
        private readonly Dictionary<Guid, Lobby> Lobbies = new Dictionary<Guid, Lobby>();

        private const string SendLobbyToClient = "SendLobby";

        /// <summary>
        /// First argument in the "Connection" line is a Lobby Object, the clients have to check the difference between the lobby they have and the one sent from here to see who joined
        /// </summary>
        /// <param name="lobby"></param>
        /// <param name="playerName"></param>
        public async Task JoinLobby(Guid lobby, string playerName, string password = null)
        {
            if (Lobbies.TryGetValue(lobby, out Lobby gameLobby))
            {
                if (gameLobby.Password == password)
                {
                    //ApplicationUser user = await _userService.GetUserAsync(Context.User);
                    gameLobby.AddPlayer(new Player(playerName, Context.ConnectionId, -1));
                    await Groups.AddToGroupAsync(Context.ConnectionId, lobby.ToString());
                    await Clients.Group(lobby.ToString()).SendAsync(SendLobbyToClient, gameLobby);
                }
            }
        }
        public async Task LeaveLobby(Guid lobby)
        {
            if (Lobbies.TryGetValue(lobby, out Lobby gameLobby))
            {
                gameLobby.RemovePlayer(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobby.ToString());
                await Clients.Group(lobby.ToString()).SendAsync(SendLobbyToClient, gameLobby);
            }
        }

        public async Task CreateLobby(string creatorName, string password = null)
        {
            //ApplicationUser user = await _userService.GetUserAsync(Context.User);
            Lobby lobby = new Lobby(new Player(creatorName, Context.ConnectionId, -1), password);
            Lobbies.Add(lobby.Id, lobby);
            await Groups.AddToGroupAsync(Context.ConnectionId, lobby.Id.ToString());
            await Clients.Client(Context.ConnectionId).SendAsync(SendLobbyToClient, lobby);
        }

    }
}
