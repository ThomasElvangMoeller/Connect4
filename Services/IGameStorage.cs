using Connect4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Services
{
    public interface IGameStorage
    {
        Task SaveGameAsync(Game game);

        Task<Game> GetGameAsync(Guid id);
    }
}
