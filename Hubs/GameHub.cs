using Connect4.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Hubs
{
    public class GameHub: Hub
    {
        private readonly GameService _gameService;
    }
}
