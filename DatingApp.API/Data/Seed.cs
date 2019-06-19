using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userMananger;
        
        private readonly RoleManager<Role> _roleManager;

       public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userMananger = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            if (!_userMananger.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>
                {
                    new Role{Name = "Member"},
                    new Role{Name = "Admin"},
                    new Role{Name = "Moderator"},
                    new Role{Name = "VIP"},
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {
                    //byte[] passwordHash, passwordSalt;
                    //CreatePasswordHash("12341234", out passwordHash, out passwordSalt);
                     //user.Photos.SingleOrDefault().IsApproved = true;
                    _userMananger.CreateAsync(user, "password").Wait();
                    _userMananger.AddToRoleAsync(user, "Member").Wait();
                }
                 var adminUser = new User
                {
                    UserName = "Admin"
                };

                IdentityResult result = _userMananger.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = _userMananger.FindByNameAsync("Admin").Result;
                    _userMananger.AddToRolesAsync(admin, new[] {"Admin", "Moderator"}).Wait();
                }
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}