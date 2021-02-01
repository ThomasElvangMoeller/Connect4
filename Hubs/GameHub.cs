using Connect4.Models;
using Connect4.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameService _gameService;
        private readonly UserService _userService;
        private readonly Dictionary<Guid, Lobby> Lobbies = new Dictionary<Guid, Lobby>();

        public async Task AddToGroup(Guid lobby)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobby.ToString());
            await Clients.Group(lobby.ToString()).SendAsync("Connection", true, Lobbies[lobby]);
        }

        public async Task RemoveFromGroup(Guid lobby)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobby.ToString());
            await Clients.Group(lobby.ToString()).SendAsync("Connection", false, Lobbies[lobby]);
        }

        public async Task CreateLobby(string password = null)
        {
            ApplicationUser user = await _userService.GetUserAsync(Context.User);
            Lobby lobby = new Lobby(user, password);
            Lobbies.Add(lobby.Id, lobby);
            await Clients.Client(Context.ConnectionId).SendAsync("JoinLobby", lobby);
        }
    }
}
