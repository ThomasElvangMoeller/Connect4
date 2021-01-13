using Connect4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Connect4.Hubs
{
    [Authorize]
    public class ChatHub: Hub
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        public ChatHub(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// <paramref name="gameId"/> is used as the group name
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string gameId, string message)
        {
            
                await Clients.Group(gameId).SendAsync("recieveMessage", new {Sender = Context.User.Identity.Name, Message = message });
            
        }
    }
}
