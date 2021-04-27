using Connect4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Services
{
    public interface ILobbyStorage
    {
        /// <param name="id"></param>
        /// <returns>Lobby with the given Guid, otherwise null</returns>
        public Task<Lobby> GetLobbyAsync(Guid id);
        /// <summary>
        /// Saves the given Lobby to storage, if the lobby does not already exist, creates a new one
        /// </summary>
        /// <param name="lobby"></param>
        public Task SaveLobbyAsync(Lobby lobby);
    }
}
