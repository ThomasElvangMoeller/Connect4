using Connect4.Models;
using Connect4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Data
{
    public class GameStorage : IGameStorage
    {

        private readonly ApplicationDbContext _context;

        public GameStorage(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Game> GetGameAsync(Guid id)
        {
            return Task.Run(() => _context.Games.FirstOrDefault(g => g.Id == id));
        }

        public Task SaveGameAsync(Game game)
        {
            return Task.Run(() =>
            {
                //TODO actually save game
            });
        }
    }
}
