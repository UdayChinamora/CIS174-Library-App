using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Library.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

    }
}
