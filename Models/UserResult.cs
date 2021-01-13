using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class UserResult
    {
        public ApplicationUser User { get; set; }
        public bool Succeded { get { return User != null; } }
        public string Error { get; set; }
    }
}
