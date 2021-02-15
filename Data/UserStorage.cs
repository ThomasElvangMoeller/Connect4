using Connect4.Models;
using Connect4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Data
{
    public class UserStorage : IUserStorage
    {

        private readonly ApplicationDbContext _context;

        public UserStorage(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ApplicationUser> Users => _context.Users.ToList();

        public Task<ApplicationUser> GetUserAsync(Guid id)
        {
            return Task.Run<ApplicationUser>(() =>
            {
                return _context.Users.FirstOrDefault(usr => usr.Id == id);
            });
        }
    }
}
