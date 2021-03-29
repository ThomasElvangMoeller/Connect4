using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Connect4.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly GameService _gameService;

        public PlayerController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("validateplayername")]
        public async Task<IActionResult> ValidatePlayerName(string playerName)
        {
            var (isValid, message) = await _gameService.ValidatePlayerName(playerName);
            if (isValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest(message);
            }
        }
    }
}
