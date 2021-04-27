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
        //private readonly Dictionary<Guid, Lobby> Lobbies = new Dictionary<Guid, Lobby>(); // TODO: Change the dictionary to a storage interface

        private const string SendLobbyToClient = "SendLobby";
        private const string UpdateSettingsResponse = "UpdateSettingsResponse";
        private const string StartGameMethod = "StartGame";

        /// <summary>
        /// First argument in the "Connection" line is a Lobby Object, the clients have to check the difference between the lobby they have and the one sent from here to see who joined
        /// </summary>
        /// <param name="lobby"></param>
        /// <param name="playerName"></param>
        public async Task JoinLobby(Guid lobby, string playerName, string password = null)
        {
            Lobby l = await _gameService.GetLobbyAsync(lobby);
            if (l != null)
            {
                if (l.Password == password)
                {
                    //ApplicationUser user = await _userService.GetUserAsync(Context.User);
                    l.AddPlayer(new Player(playerName, Context.ConnectionId, -1));
                    await Groups.AddToGroupAsync(Context.ConnectionId, lobby.ToString());
                    await Clients.Group(lobby.ToString()).SendAsync(SendLobbyToClient, l);
                }
            }
            await _gameService.SaveLobbyAsync(l);
        }
        public async Task LeaveLobby(Guid lobby)
        {
            Lobby l = await _gameService.GetLobbyAsync(lobby);
            if (l != null)
            {
                l.RemovePlayer(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobby.ToString());
                await Clients.Group(lobby.ToString()).SendAsync(SendLobbyToClient, l);
            }
            await _gameService.SaveLobbyAsync(l);
        }

        [HubMethodName("CreateLobby")]
        public async Task CreateLobby(string creatorName, string password = null)
        {
            //ApplicationUser user = await _userService.GetUserAsync(Context.User);
            Lobby lobby = new Lobby(new Player(creatorName, Context.ConnectionId, -1), password);
            await _gameService.SaveLobbyAsync(lobby);
            await Groups.AddToGroupAsync(Context.ConnectionId, lobby.Id.ToString());
            await Clients.Client(Context.ConnectionId).SendAsync(SendLobbyToClient, lobby);
        }

        public async Task UpdateGameSettings(Guid lobby, GameSettings settings)
        {
            Lobby l = await _gameService.GetLobbyAsync(lobby);
            if (l != null)
            {
                l.GameSettings = settings;
                await Clients.Client(Context.ConnectionId).SendAsync(UpdateSettingsResponse, true, l);
                await _gameService.SaveLobbyAsync(l);
            }
            else
            {
                await Clients.Client(Context.ConnectionId).SendAsync(UpdateSettingsResponse, false, "Failure: Could not find Lobby");
            }
        }

        public async Task StartGame(Guid lobbyId)
        {
            Lobby lobby = await _gameService.GetLobbyAsync(lobbyId);
            if (lobby != null)
            {

                if (!lobby.Players.All(p => p.PlayerColor >= 0 && p.PlayerColor <= 4))
                {
                    await Clients.Client(Context.ConnectionId).SendAsync(StartGameMethod, false, "Not all players has a color");
                }
                else
                {
                    Game game = await _gameService.CreateGame(lobby.GameSettings, lobby.Players, lobbyId);
                    await Clients.Group(lobby.Id.ToString()).SendAsync(StartGameMethod, true, game);
                }
            }
        }

    }
}
