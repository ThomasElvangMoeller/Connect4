﻿using Connect4.Data;
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
        private readonly ApplicationDbContext _context;


        public async Task<Game> CreateGame(GameSettings settings, Dictionary<string, PlayerColor> playersAndColors)
        {
            Game game = new Game(settings, playersAndColors);

            await _context.Games.AddAsync(game);

            foreach (KeyValuePair<string, PlayerColor> item in playersAndColors)
            {
                // We check the keys to see if they match any of the users in the database. If user is null the current player is a guest without an user
                if (Guid.TryParse(item.Key, out Guid result))
                {
                    ApplicationUser user = _context.Users.FirstOrDefault(q => q.Id == result);
                    if (user != null)
                    {
                        user.CurrentGames.Add(game);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return game;
        }
    }
}
