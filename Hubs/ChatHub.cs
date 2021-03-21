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
    public class ChatHub: Hub
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        public ChatHub(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// <paramref name="gameId"/> is used as the group name
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string gameId, string message)
        {
            //Context.User.Identity.Name can be null, find alternative
            await Clients.Group(gameId).SendAsync("recieveMessage", new {Sender = Context.User.Identity.Name, Message = message });
            
        }
    }
}
