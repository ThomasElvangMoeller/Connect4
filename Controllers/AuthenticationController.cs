using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Connect4.Models;
using Connect4.Models.ApiRequest;
using Connect4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Connect4.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private UserService _userService;

        //TODO: Add produces thing from swagger
        [HttpPost("/user/new")]
        public async Task<IActionResult> CreateUser(UserRequest request)
        {
            UserResult result = await _userService.CreateUserAsync(request.Username, request.Password);
            if(result.Succeded)
            {
                return Ok(result.User);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
    }
}
