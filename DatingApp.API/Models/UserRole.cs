using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.API.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        

       public Role Role { get; set; }
        public User User { get; set; }
    }
}