using Connect4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Services
{
    public interface IUserStorage
    {
        public Task<ApplicationUser> GetUserAsync(Guid id);
        
        public List<ApplicationUser> Users { get; }
    }
}
