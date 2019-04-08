using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto user)
        {
        
            user.Username=user.Username.ToLower();
            var userExists= await _authRepository.UserExists(user.Username);
            if(userExists)
                return BadRequest("Username already exists");
            
            var userToCreate=new User
            {
                UserName=user.Username
            };
            var result=_authRepository.Register(userToCreate,user.Password);
            return StatusCode(201);
        }

    }
}