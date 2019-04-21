using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper ;

        public AuthController(IAuthRepository authRepository, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _authRepository = authRepository;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto user)
        {

            user.Username = user.Username.ToLower();
            var userExists = await _authRepository.UserExists(user.Username);
            if (userExists)
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                UserName = user.Username
            };
            var result = _authRepository.Register(userToCreate, user.Password);
            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDto userLogin)
        {
            var userExist = await _authRepository.Login(userLogin.Username.ToLower(), userLogin.Password);
            if (userExist == null)
                return Unauthorized();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userExist.Id.ToString()),
                new Claim(ClaimTypes.Name,userExist.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            var user = _mapper.Map<UserListDto>(userExist);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });
        }

    }
}