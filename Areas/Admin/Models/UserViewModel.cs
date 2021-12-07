using System.Collections.Generic;
using BoardGames_FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace Areas.Admin.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
