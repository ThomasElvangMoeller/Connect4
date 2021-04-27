using Connect4.Data;
using Connect4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Services
{
    public class GameService
    {
        private readonly AppSettings _appSettings;
        private readonly IGameStorage _gameStorage;
        private readonly IUserStorage _userStorage;
        private readonly ILobbyStorage _lobbyStorage;

        public GameService(AppSettings appSettings, IGameStorage gameStorage, IUserStorage userStorage, ILobbyStorage lobbyStorage)
        {
            _appSettings = appSettings;
            _gameStorage = gameStorage;
            _userStorage = userStorage;
            _lobbyStorage = lobbyStorage;
        }


        public async Task<Game> GetGameAsync(Guid id)
        {
            return await _gameStorage.GetGameAsync(id);
        }

        public async Task<Lobby> GetLobbyAsync(Guid id)
        {
            return await _lobbyStorage.GetLobbyAsync(id);
        }
        public async Task SaveLobbyAsync(Lobby lobby)
        {
            await _lobbyStorage.SaveLobbyAsync(lobby);
        }

        public async Task<Game> CreateGame(GameSettings settings, Player[] players, Guid id)
        {
            Game game = new Game(settings, players, id);

            foreach (Player p in players)
            {
                // We check the keys to see if they match any of the users in the database. If user is null the current player is a guest without an user
                if (p.IsAppUser)
                {
                    ApplicationUser user = await _userStorage.GetUserAsync(p.ApplicationUserId.Value);
                    if (user != null)
                    {
                        user.CurrentGames.Add(game);
                    }
                }
            }
            await _gameStorage.SaveGameAsync(game);
            return game;
        }
        /// <summary>
        /// TODO: Fix
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public async Task<(bool, string)> ValidatePlayerName(string playerName)
        {
            //Check if playername is acceptable. No racism, bigotry, etc. Not longer than "n" characters, no invalid characters
            return await new Task<(bool,string)>(() => {
                return (true, string.Empty);
            });
        }
    }
}
