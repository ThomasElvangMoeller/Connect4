using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Connect4.Hubs
{
    public class UsernameIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            if (connection.User != null)
            {
                var claim = connection.User.FindFirst(ClaimTypes.NameIdentifier);
                if(claim != null)
                {
                    return claim.Value;

                }
            }
            return null;
        }
    }
}
